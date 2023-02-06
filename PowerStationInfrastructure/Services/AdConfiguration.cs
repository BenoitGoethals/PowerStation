using PowerStationInfrastructure.helpers;

namespace PowerStationInfrastructure.Services;

[Serializable]
public class AdConfiguration : IDisposable
{
    public void Dispose()
    {
    }

    public Guid IdGuid { get; set; } 
    

    public AdConfiguration(string? adServerHostname)
    {
        IdGuid = Guid.NewGuid();
        AdServerHostname = adServerHostname;
        DistinguishedName = AdHelper.GetRootContainer();
        DomainName = Environment.UserDomainName;
        OuUnit = new OrganizationalUnit(DomainName,DistinguishedName);
      
    }

    public AdConfiguration()
    {
        
    }

    public OrganizationalUnit? OuUnit { get; }



    public string? AdServerHostname { get; set; }

    public string? DistinguishedName { get; set; }

    public string? DomainName  { get; set; }
    public static AdConfiguration Default => new("HostName");

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