namespace PowerStationInfrastructure.Services;

[Serializable]
public class AdConfiguration
{
    public Guid IdGuid { get; set; } 
    public AdConfiguration(string? adServerHostname, string? distinguishedName, string? domainName)
    {
        IdGuid=Guid.NewGuid();
        AdServerHostname = adServerHostname;
        DistinguishedName = distinguishedName;
        DomainName = domainName;
    }

    public AdConfiguration()
    {
        
    }

    public string? AdServerHostname { get; set; }

    public string? DistinguishedName { get; set; }

    public string? DomainName  { get; set; }
    public static AdConfiguration Default => new("HostName", "PowerPlane", "PowerPlane");

    protected bool Equals(AdConfiguration other)

    {
        return IdGuid.Equals(other.IdGuid);
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((AdConfiguration) obj);
    }

    public override int GetHashCode()
    {
        return IdGuid.GetHashCode();
    }

    public override string ToString()
    {
        return $"{nameof(IdGuid)}: {IdGuid}, {nameof(AdServerHostname)}: {AdServerHostname}, {nameof(DistinguishedName)}: {DistinguishedName}, {nameof(DomainName)}: {DomainName}";
    }
}