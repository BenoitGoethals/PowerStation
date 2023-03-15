using Microsoft.Extensions.Logging;
using System.ComponentModel;
using System.DirectoryServices.AccountManagement;
using System.DirectoryServices.ActiveDirectory;
using System.Reflection.PortableExecutable;
using AutoMapper;
using System.Xml.Linq;
using System.DirectoryServices;
using DirectoryEntry = System.DirectoryServices.DirectoryEntry;
using System.Configuration;
using Microsoft.Extensions.Configuration;
using PowerStationInfrastructure.helpers;

using PowerStationDomain.models.AD;
using System.Security.Principal;

namespace PowerStationInfrastructure.Services.AD;

public class AdServiceUser : IAdServiceUser
{
    private readonly AdConfiguration _configuration;
    private readonly ILogger<AdServiceUser> _logger;
    private readonly IMapper _mapper;

    public AdServiceUser(AdConfiguration configuration, ILogger<AdServiceUser> logger, IMapper mapper)
    {
        _configuration = configuration;
        _logger = logger;
        _mapper = mapper;
    }


    private PrincipalContext? GetPrincipalContext(bool topLevel)
    {
        return topLevel ? new PrincipalContext(ContextType.Domain, Environment.UserDomainName) : new PrincipalContext(ContextType.Domain, Environment.UserDomainName, _configuration.OuUnit?.Users);
    }


    public bool Add(User user)
    {
        try
        {
            using var context = new PrincipalContext(ContextType.Domain, Environment.UserDomainName,
                _configuration.OuUnit?.Users);
            {
                using var contextGrp = new PrincipalContext(ContextType.Domain, Environment.UserDomainName);
                {
                    var userPrincePrincipal = _mapper.Map<UserPrincipal>(user);

                    userPrincePrincipal.Save(context);

                    var group = user.AllMembers().FirstOrDefault();
                    if (group != null)
                    {
                        if (group.Name != null)
                        {
                            GroupPrincipal? groupPrincipal = GroupPrincipal.FindByIdentity(contextGrp, group.Name);
                            if (groupPrincipal != null)
                            {
                                groupPrincipal.Members.Add(userPrincePrincipal);
                                groupPrincipal.Save(contextGrp);
                            }
                        }

                        contextGrp.Dispose();
                        context.Dispose();

                    }
                }
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            return false;
        }

        return true;
    }

    public bool Remove(User adUser)
    {
        try
        {
            using var context = new PrincipalContext(ContextType.Domain, Environment.UserDomainName, _configuration.OuUnit?.Users);
            {
                using var principal = new UserPrincipal(context)
                {
                    SamAccountName = adUser.SamAccountName
                };
                using var searcher = new PrincipalSearcher(principal);
                PrincipalSearchResult<Principal> result = searcher.FindAll();
                UserPrincipal? user = (UserPrincipal)result.First();
                if (user.SamAccountName == null) return false;
                UserPrincipal? u = UserPrincipal.FindByIdentity(context, user.SamAccountName);
                u?.Delete();
                context.Dispose();
            }
            return true;
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return false;
        }
    }

    public bool Remove(string adUser)
    {
        try
        {
            using var context = new PrincipalContext(ContextType.Domain, Environment.UserDomainName, _configuration.OuUnit?.Users);
            {
                using var principal = new UserPrincipal(context)
                {
                    SamAccountName = adUser
                };
                using var searcher = new PrincipalSearcher(principal);
                PrincipalSearchResult<Principal> result = searcher.FindAll();
                UserPrincipal? user = (UserPrincipal)result.First();
                if (user.SamAccountName != null)
                {
                    UserPrincipal? u = UserPrincipal.FindByIdentity(context, user.SamAccountName);
                    u?.Delete();
                }
                context.Dispose();
            }
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return false;
        }

        return true;
    }

    public List<User>? All()
    {
        try
        {
            using var context = new PrincipalContext(ContextType.Domain, Environment.UserDomainName, _configuration.OuUnit?.Users);
            {
                using var searcher = new PrincipalSearcher(new UserPrincipal(context));
                List<UserPrincipal> users = searcher.FindAll().Select(u => (UserPrincipal)u).ToList();
                var usersAll = users.Select(principal => _mapper.Map<User>(principal)).ToList();
                context.Dispose();
                return usersAll;
            }
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return null;
        }
    }




    public User? Get(string username,bool topLevel)
    {
        try
        {
            using var context = GetPrincipalContext(topLevel);
            {
                using var principal = new UserPrincipal(context)
                {
                    SamAccountName = username
                };
                using var searcher = new PrincipalSearcher(principal);
                PrincipalSearchResult<Principal> result = searcher.FindAll();
                if (!result.Any())
                {
                    context.Dispose();
                    return null;
                }
                var user = (UserPrincipal)result.First();
                var newUser = _mapper.Map<User>(user);
                context.Dispose();
                return newUser;
            }
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return null;
        }



    }


    /// <summary>
    /// Sets the enabled boolean to false on the specified user account.
    /// </summary>
    /// <param name="name">The unique identifier of the user.</param>
    /// <returns>True if the account was disabled.</returns>
    public bool DisableUser(string name)
    {
        try
        {
            using var context = new PrincipalContext(ContextType.Domain, Environment.UserDomainName);
            {
                UserPrincipal u = UserPrincipal.FindByIdentity(context, name);

                u.Enabled = false;
                u.Save();
                context.Dispose();
                return true;
            }
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return false;
        }

    }

    /// <summary>
    /// Sets the enabled boolean to true on the specified user account.
    /// </summary>
    /// <param name="name">The unique identifier of the user.</param>
    /// <returns>True if the account was enabled, false otherwise.</returns>
    public bool EnableUser(string name)
    {
        try
        {
            using var context = new PrincipalContext(ContextType.Domain, Environment.UserDomainName);
            {
                UserPrincipal u = UserPrincipal.FindByIdentity(context, name);

                u.Enabled = true;
                u.Save();
                context.Dispose();
                return true;
            }
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return false;
        }

    }

    /// <summary>
    /// This method searches for a user with the specified email, and if found, returns the UserPrincipal object
    /// associated with it.
    /// </summary>
    /// <param name="email">The email address that identifies the user.</param>
    /// <returns>A UserPrincipal object.</returns>
    public User? GetUserByEmail(string email)
    {
        User? result = null;
        DirectorySearcher searcher = new DirectorySearcher();
        searcher.Filter = $"(&(objectCategory=person)(objectClass=user)(mail={email}))";
        foreach (SearchResult r in searcher.FindAll())
        {
            DirectoryEntry de = r.GetDirectoryEntry();
            string sam = de.Properties["SamAccountName"].Value.ToString();
            string dn = de.Properties["distinguishedName"].Value.ToString();
            if (dn.Contains(",OU=Users,"))
            {
                return _mapper.Map<User>(Get(sam,true));
            }
        }
        return result;
    }

    public void Dispose()
    {

    }
}