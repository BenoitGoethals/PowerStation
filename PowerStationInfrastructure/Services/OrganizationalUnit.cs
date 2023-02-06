namespace PowerStationInfrastructure.Services;

public record OrganizationalUnit
{
    public OrganizationalUnit(string? domain,string? distName)
    {
        Groups = buildLdapPath("groups",distName,domain);
        Users = buildLdapPath("users", distName, domain);
        Shares = buildLdapPath("shares", distName, domain);
        Wks = buildLdapPath("wks", distName, domain);
    }

    private string buildLdapPath(string ou, string distName, string domain)
    {
        return $"OU={ou},OU={domain.ToLower()},{distName}";
    }

    public string? Users { get;  }

    public string? Groups { get;  }

    public string? Servers { get;  }

    public string? Shares { get;  }

    public string? Wks { get;  }
}