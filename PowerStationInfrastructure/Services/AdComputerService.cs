using AutoMapper;
using Microsoft.Extensions.Logging;
using PowerStationDomain.models;
using PowerStationDomain.temp;
using System.DirectoryServices.AccountManagement;
using System.Xml.Linq;

namespace PowerStationInfrastructure.Services;

public class AdComputerService:IAdComputerService
{
    private readonly Configuration _configuration;
    private readonly ILogger<AdComputerService> _logger;
    private readonly IMapper _mapper;

    public AdComputerService(Configuration configuration, ILogger<AdComputerService> logger, IMapper mapper)
    {
        this._configuration = configuration;
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
            using var context = new PrincipalContext(ContextType.Domain, Environment.UserDomainName);
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

    private static bool RemoveComputer(string identity)
    {
        using var context = new PrincipalContext(ContextType.Domain, Environment.UserDomainName);
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

    public Computer? Get(string identity)
    {
        try
        {
            using var context = new PrincipalContext(ContextType.Domain, Environment.UserDomainName);
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
}

public interface IAdComputerService:IAdService<Computer>
{
}