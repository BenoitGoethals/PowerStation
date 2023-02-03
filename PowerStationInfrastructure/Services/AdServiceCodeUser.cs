using Microsoft.Extensions.Logging;
using PowerStationDomain.models;
using System.ComponentModel;
using System.DirectoryServices.AccountManagement;
using System.DirectoryServices.ActiveDirectory;
using System.Reflection.PortableExecutable;
using AutoMapper;
using System.Xml.Linq;

namespace PowerStationInfrastructure.Services;

public class AdServiceCodeUser : IAdService<User>
{
    private readonly Configuration _configuration;
    private readonly ILogger<AdServiceCodeUser> _logger;
    private readonly IMapper _mapper;

    public AdServiceCodeUser(Configuration configuration, ILogger<AdServiceCodeUser> logger, IMapper mapper)
    {
        this._configuration = configuration;
        _logger = logger;
        _mapper = mapper;
    }

    public bool Add(User user)
    {
        try
        {
            var userPrincePrincipal = _mapper.Map<UserPrincipal>(user);
            userPrincePrincipal.Save();
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
            using var context = new PrincipalContext(ContextType.Domain, Environment.UserDomainName);
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
            using var context = new PrincipalContext(ContextType.Domain, Environment.UserDomainName);
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
            using var context = new PrincipalContext(ContextType.Domain, Environment.UserDomainName);
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




    public User? Get(string username)
    {
        try
        {
            using var context = new PrincipalContext(ContextType.Domain, Environment.UserDomainName);
            {
                using var principal = new UserPrincipal(context)
                {
                    SamAccountName = username
                };
                using var searcher = new PrincipalSearcher(principal);
                PrincipalSearchResult<Principal> result = searcher.FindAll();
                UserPrincipal? user = (UserPrincipal)result.First();
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


    public void Dispose()
    {

    }
}