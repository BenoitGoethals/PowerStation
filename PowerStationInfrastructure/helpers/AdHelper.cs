using System.DirectoryServices.ActiveDirectory;
using System.Security.Policy;

namespace PowerStationInfrastructure.helpers;

public static class AdHelper
{
    public static bool DomainExistsNoForests(string domain, string server)
    {
        try
        {
            var directoryContext = new DirectoryContext(DirectoryContextType.DirectoryServer, server);
            var d = Domain.GetDomain(directoryContext);
            return d.Name.Trim().Equals(domain.Trim(), StringComparison.CurrentCultureIgnoreCase);
        }
        catch (Exception e)
        {
            return false;
        }
    }
    public static bool DomainExist(string domain)
    {
        HashSet<string> domains = new HashSet<string>();
        foreach (Domain d in Forest.GetCurrentForest().Domains)
        {
            domains.Add(d.Name.ToLower());
        }

        return domains.Contains(domain.ToLower());
    }

}