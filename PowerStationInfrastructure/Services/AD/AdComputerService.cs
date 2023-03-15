using AutoMapper;
using Microsoft.Extensions.Logging;
using PowerStationDomain.models.AD;
using PowerStationDomain.temp;
using PowerStationInfrastructure.helpers;
using System.DirectoryServices.AccountManagement;
using System.Security.Principal;
using System.Xml.Linq;

namespace PowerStationInfrastructure.Services.AD;

public class AdComputerService : IAdComputerService
{
    private readonly AdConfiguration _configuration;
    private readonly ILogger<AdComputerService> _logger;
    private readonly IMapper _mapper;

    public AdComputerService(AdConfiguration configuration, ILogger<AdComputerService> logger, IMapper mapper)
    {
        _configuration = configuration;
        _logger = logger;
        _mapper = mapper;
    }
    public void Dispose()
    {

    }

    public bool Add(Computer identity)
    {
        try
        {
            using var context = new PrincipalContext(ContextType.Domain, Environment.UserDomainName, _configuration.OuUnit?.Wks);
            {
                if (identity.Name != null)
                {
                    ComputerPrincipal newComputer = new ComputerPrincipal(context, identity.Name, "First@1401!", true);
                    newComputer.Description = identity.Description;
                    newComputer.DisplayName = identity.DisplayName;
                    newComputer.Name = identity.Name;
                    newComputer.Save();
                    context.Dispose();
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

    public bool Remove(Computer identity)
    {
        try
        {
            if (identity.SamAccountName != null && RemoveComputer(identity.SamAccountName)) return true;
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return false;
        }

        return false;
    }

    public bool Remove(string identity)
    {
        try
        {
            if (RemoveComputer(identity)) return true;
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return false;
        }

        return false;
    }

    private bool RemoveComputer(string identity)
    {
        using var context = new PrincipalContext(ContextType.Domain, Environment.UserDomainName, _configuration.OuUnit?.Wks);
        {
            ComputerPrincipal? computerPrincipal = ComputerPrincipal.FindByIdentity(context, identity);
            if (computerPrincipal != null)
            {
                computerPrincipal.Delete();
                return true;
            }

            context.Dispose();
        }
        return false;
    }

    public List<Computer>? All()
    {
        try
        {
            using var context = new PrincipalContext(ContextType.Domain, Environment.UserDomainName);
            {
                using var searcher = new PrincipalSearcher(new ComputerPrincipal(context));
                List<ComputerPrincipal> computers = searcher.FindAll().Select(u => (ComputerPrincipal)u).ToList();
                var computersList = computers.Select(principal => _mapper.Map<Computer>(principal)).ToList();
                context.Dispose();
                return computersList;
            }
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return null;
        }
    }

    public Computer? Get(string identity, bool topLevel)
    {
        try
        {
            using var context = new PrincipalContext(ContextType.Domain, Environment.UserDomainName, _configuration.OuUnit?.Wks);
            {
                using var principal = new ComputerPrincipal(context)
                {
                    SamAccountName = identity
                };
                using var searcher = new PrincipalSearcher(principal);
                PrincipalSearchResult<Principal> result = searcher.FindAll();
                ComputerPrincipal? computer = (ComputerPrincipal)result.First();
                var newComputer = _mapper.Map<Computer>(computer);
                context.Dispose();
                return newComputer;
            }
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return null;
        }
    }

    /// <summary>
    /// Returns a list of computers that are members of the specified group.
    /// </summary>
    /// <param name="group">The unique identifier of the group.</param>
    /// <returns>A list of ComputerPrincipal objects.</returns>
    public List<ComputerPrincipal>? GetComputersInGroup(string group)
    {
        try
        {
            using var context = new PrincipalContext(ContextType.Domain, Environment.UserDomainName);
            {
                List<ComputerPrincipal>? results = new List<ComputerPrincipal>();
                GroupPrincipal g = GroupPrincipal.FindByIdentity(context, group);
                PrincipalCollection members = g.Members;
                foreach (Principal member in members)
                {
                    if (member is ComputerPrincipal)
                    {
                        results.Add(GetComputer(member.SamAccountName, context));
                    }
                }
                return results;
            }
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);

        }
        return null;
    }
    private ComputerPrincipal GetComputer(string name, PrincipalContext principalContext)
    {
        ComputerPrincipal cp = ComputerPrincipal.FindByIdentity(principalContext, name)!;
        return cp;
    }
}

public interface IAdComputerService : IAdService<Computer>
{
    public List<ComputerPrincipal>? GetComputersInGroup(string group);
}