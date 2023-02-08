using AutoMapper;
using Microsoft.Extensions.Logging;
using System.DirectoryServices;
using PowerStationInfrastructure.helpers;
using PowerStationDomain.models.AD;

namespace PowerStationInfrastructure.Services.AD;



public class AdServiceOrganizationalUnit : IAdServiceOrganizationalUnit
{

    private readonly Configuration _configuration;
    private readonly ILogger<AdServiceUser> _logger;
    private readonly IMapper _mapper;

    public AdServiceOrganizationalUnit(Configuration configuration, ILogger<AdServiceUser> logger, IMapper mapper)
    {
        _configuration = configuration;
        _logger = logger;
        _mapper = mapper;
    }

    public bool Add(OrganizationalUnit identity)
    {
        throw new NotImplementedException();
    }

    public bool Remove(OrganizationalUnit identity)
    {
        throw new NotImplementedException();
    }

    public bool Remove(string identity)
    {
        throw new NotImplementedException();
    }

    public List<OrganizationalUnit>? All()
    {
        List<OrganizationalUnit> organizationalUnits = new List<OrganizationalUnit>();
        // connect to "RootDSE" to find default naming context
        DirectoryEntry rootDSE = new DirectoryEntry("LDAP://RootDSE");

        string defaultContext = rootDSE.Properties["defaultNamingContext"][0].ToString();

        // bind to default naming context - if you *know* where you want to bind to - 
        // you can just use that information right away
        DirectoryEntry domainRoot = new DirectoryEntry("LDAP://" + defaultContext);

        // set up directory searcher based on default naming context entry
        DirectorySearcher ouSearcher = new DirectorySearcher(domainRoot);

        // SearchScope: OneLevel = only immediate subordinates (top-level OUs); 
        // subtree = all OU's in the whole domain (can take **LONG** time!)
        //ouSearcher.SearchScope = SearchScope.OneLevel;
        ouSearcher.SearchScope = SearchScope.Subtree;

        // define properties to load - here I just get the "OU" attribute, the name of the OU
        ouSearcher.PropertiesToLoad.Add("ou");

        // define filter - only select organizational units
        ouSearcher.Filter = "(objectCategory=organizationalUnit)";

        // do search and iterate over results
        foreach (SearchResult deResult in ouSearcher.FindAll())
        {
            organizationalUnits.Add(new OrganizationalUnit()
            {
                Name = deResult.Properties["ou"][0].ToString()
            });

        }

        return organizationalUnits;
    }



    public List<OrganizationalUnit>? All(string? level)
    {
        List<OrganizationalUnit> organizationalUnits = new List<OrganizationalUnit>();

        DirectoryEntry domainRoot = new DirectoryEntry("LDAP://" + level);

        // set up directory searcher based on default naming context entry
        DirectorySearcher ouSearcher = new DirectorySearcher(domainRoot);

        // SearchScope: OneLevel = only immediate subordinates (top-level OUs); 
        // subtree = all OU's in the whole domain (can take **LONG** time!)
        //ouSearcher.SearchScope = SearchScope.OneLevel;
        ouSearcher.SearchScope = SearchScope.OneLevel;

        // define properties to load - here I just get the "OU" attribute, the name of the OU
        ouSearcher.PropertiesToLoad.Add("ou");

        // define filter - only select organizational units
        ouSearcher.Filter = "(objectCategory=organizationalUnit)";

        // do search and iterate over results
        foreach (SearchResult deResult in ouSearcher.FindAll())
        {
            organizationalUnits.Add(new OrganizationalUnit()
            {
                Name = deResult.Properties["ou"][0].ToString()
            });

        }

        return organizationalUnits;
    }

    public OrganizationalUnit? Get(string identity)
    {
        throw new NotImplementedException();
    }
}