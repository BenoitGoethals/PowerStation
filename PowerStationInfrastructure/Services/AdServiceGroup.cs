using System.Collections.Specialized;
using PowerStationDomain.Interfaces;
using PowerStationDomain.models;
using static System.Formats.Asn1.AsnWriter;
using System.DirectoryServices.AccountManagement;
using System.Xml.Linq;
using AutoMapper;
using Microsoft.Extensions.Logging;
using PowerStationInfrastructure.helpers;
using System.Security.Principal;
using System.Text.RegularExpressions;
using GroupScope = PowerStationDomain.models.GroupScope;
using AutoMapper.Execution;

namespace PowerStationInfrastructure.Services;

public class AdServiceGroup : IAdServiceSecurity
{

    private readonly Configuration _configuration;
    private readonly ILogger<AdServiceUser> _logger;
    private readonly IMapper _mapper;

    public AdServiceGroup(Configuration configuration, ILogger<AdServiceUser> logger, IMapper mapper)
    {
        this._configuration = configuration;
        _logger = logger;
        _mapper = mapper;
    }
    public void Dispose()
    {

    }

    public bool Add(SecurityGroup identity)
    {
        try
        {
            using var context = new PrincipalContext(ContextType.Domain, Environment.UserDomainName, _configuration.AdSetting.OuUnit?.Groups);
            {
                if (identity.GroupName == null) return false;
                using GroupPrincipal? newGroup = new GroupPrincipal(context, identity.GroupName);
                newGroup.GroupScope = identity.Scope.ConvertToGroupPrincipal();
                newGroup.IsSecurityGroup = identity.IsSecurityGroup;
                newGroup.Name = identity.Name;
                newGroup.DisplayName = identity.DisplayName;
                newGroup.Description = identity.Description;
                newGroup.Save();

                return true;
            }
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);

        }
        return false;
    }

    public bool Remove(SecurityGroup? identity)
    {
        try
        {
            using var context = new PrincipalContext(ContextType.Domain, Environment.UserDomainName, _configuration.AdSetting.OuUnit?.Groups);
            {
                GroupPrincipal? groupPrincipal = GroupPrincipal.FindByIdentity(context, identity.GroupName);
                if (groupPrincipal != null)
                {
                    groupPrincipal.Delete();
                    return true;

                }

            }
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);

        }
        return false;
    }




    /// <summary>
    /// This method returns true if the specified principal is a member of the specified group.
    /// </summary>
    /// <param name="principal">The unique identifier for the principal.</param>
    /// <param name="group">The unique identifier for the group.</param>
    /// <returns>True if the Principal is a member of the group, false otherwise.</returns>
    public bool IsMember(string? principal, string? group)
    {
        try
        {
            using var context = new PrincipalContext(ContextType.Domain, Environment.UserDomainName);
            {
                if (group == null || principal == null) return false;
                using GroupPrincipal? g = GroupPrincipal.FindByIdentity(context, group);
                using Principal? u = Principal.FindByIdentity(context, principal);
                if (u != null && g != null)
                {
                    // loop through the members of the group and compare samaccountnames to the user's.
                    // have to do it this way rather than using UserPrincipal.IsMemberOf or GroupPrincipal.
                    // Members.Contains because those have a known issue of returning false for Domain Users
                    // and Domain Computers. Thanks, Microsoft.
                    return g.Members.Any(p => p.SamAccountName == u.SamAccountName);
                }

                return false;
            }
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return false;

        }

    }

    public List<SecurityGroup>? GetGroups(string userName)
    {
        try
        {
            using var context = new PrincipalContext(ContextType.Domain, Environment.UserDomainName);
            {
                using var searcher = new PrincipalSearcher(new GroupPrincipal(context));
                var groups = new List<GroupPrincipal>();

                foreach (var u in searcher.FindAll().Select(u => (GroupPrincipal)u).ToList())
                {
                    foreach (Principal principal in u.Members.Where(t=>t is UserPrincipal))
                    {
                        if (principal.SamAccountName.Equals(userName))
                        {
                            groups.Add((GroupPrincipal)u);
                        }
                    }
                }
                var groupPrincipals = groups.Select(principal => _mapper.Map<SecurityGroup>(principal)).ToList();
                context.Dispose();
                return groupPrincipals;

            }
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);

        }

        return null;
    }

    public List<User> GetAllUsers(string groupName)
    {
        try
        {
            using var context = new PrincipalContext(ContextType.Domain, Environment.UserDomainName);
            {
                List<User> principals = new List<User>();
                GroupPrincipal oGroupPrincipal = GroupPrincipal.FindByIdentity(context, groupName)!;
                foreach (var member in oGroupPrincipal.Members)
                {
                    if (member is UserPrincipal principal)
                    {
                        principals.Add(_mapper.Map<User>(principal));
                    }
                }
              
                context.Dispose();
                return principals;
            }
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);

        }

        return null;
    }

    public bool Remove(string? identity)
    {
        if (identity == null) return false;
        try
        {
            using var context = new PrincipalContext(ContextType.Domain, Environment.UserDomainName);
            {
                GroupPrincipal groupPrincipal = GroupPrincipal.FindByIdentity(context, identity)!;
                groupPrincipal.Delete();
                return true;
            }
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);

        }
        return false;
    }

    public List<SecurityGroup>? All()
    {
        try
        {
            using var context = new PrincipalContext(ContextType.Domain, Environment.UserDomainName);
            {
                using var searcher = new PrincipalSearcher(new GroupPrincipal(context));
                List<GroupPrincipal> users = searcher.FindAll().Select(u => (GroupPrincipal)u).ToList();
                var groups = users.Select(principal => _mapper.Map<SecurityGroup>(principal)).ToList();
                context.Dispose();
                return groups;

            }
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);

        }

        return null;
    }

    public SecurityGroup? Get(string sGroupName)
    {
        try
        {
            using var context = new PrincipalContext(ContextType.Domain, Environment.UserDomainName);
            {
                GroupPrincipal oGroupPrincipal = GroupPrincipal.FindByIdentity(context, sGroupName)!;
                var group = _mapper.Map<SecurityGroup>(oGroupPrincipal);
                context.Dispose();
                return group;
            }
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
        }
        return null;
    }
    /// <summary>
    /// Returns a list of groups that are members of the specified group.
    /// </summary>
    /// <param name="group">The unique identifier of the group.</param>
    /// <returns>A list of GroupPrincipal objects.</returns>
    public  List<SecurityGroup?> GetGroupsInGroup(string group)
    {
        try
        {
            using var context = new PrincipalContext(ContextType.Domain, Environment.UserDomainName);
            {
                List<SecurityGroup?> results = new List<SecurityGroup?>();
                GroupPrincipal g = GroupPrincipal.FindByIdentity(context, group);
                PrincipalCollection members = g.Members;
                foreach (Principal member in members)
                {
                    if (member is GroupPrincipal)
                    {
                        results.Add(Get(member.SamAccountName));
                    }
                }
                context.Dispose();
                return results;
            }
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
        }
        return null;
        
    }
}