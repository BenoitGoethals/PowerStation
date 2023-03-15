namespace PowerStationInfrastructure.Services.AD;

public record OrganizationalUnitLocation
{
    public OrganizationalUnitLocation(string? domain, string? distName)
    {
        Groups = BuildLdapPath("groups", distName, domain);
        Users = BuildLdapPath("users", distName, domain);
        Shares = BuildLdapPath("shares", distName, domain);
        Wks = BuildLdapPath("wks", distName, domain);
        Root = $"OU={domain.ToLower()},{distName}";
    }

    public OrganizationalUnitLocation()
    {
    }

    private string BuildLdapPath(string ou, string distName, string domain)
    {
        return $"OU={ou},OU={domain.ToLower()},{distName}";
    }

    public string? Users { get; }

    public string? Groups { get; }

    public string? Servers { get; }

    public string? Shares { get; }

    public string? Wks { get; }
    public string? Root { get; set; }
}