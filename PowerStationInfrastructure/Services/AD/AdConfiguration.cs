using System.Xml.Serialization;
using PowerStationDomain.models.organisation;

using PowerStationInfrastructure.helpers;

namespace PowerStationInfrastructure.Services.AD;

[Serializable]
public class AdConfiguration : IDisposable
{
    public void Dispose()
    {
    }

    public Guid IdGuid { get; set; }


    public AdConfiguration(string? adServerHostname)
    {

        try
        {
            Load(adServerHostname);
            IsLoaded=true;
        }
        catch (Exception e)
        {
            IsLoaded=false;
        }

    }

    public bool IsLoaded { get; set; }

    private void Load(string? adServerHostname)
    {
        IdGuid = Guid.NewGuid();
        AdServerHostname = adServerHostname;
        DistinguishedName = AdHelper.GetRootContainer();
        DomainName = Environment.UserDomainName;
        OuUnit = new OrganizationalUnitLocation(DomainName, DistinguishedName);
    }

    public AdConfiguration()
    {

    }



    public OrganizationalUnitLocation? OuUnit { get; set; }

  
    public Department Department { get; set; }

    public string? AdServerHostname { get; set; }

    public string? DistinguishedName { get; set; }

    public string? DomainName { get; set; }
    public static AdConfiguration Default => new()
    {
        IsLoaded = false,
        Department = new Department(),
        AdServerHostname = "localhost",
        DistinguishedName = "default",
        DomainName = "default",
        IdGuid = new Guid(),
        OuUnit = new OrganizationalUnitLocation("default", "default")
    };

    protected bool Equals(AdConfiguration other)

    {
        return IdGuid.Equals(other.IdGuid);
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((AdConfiguration)obj);
    }

    public override int GetHashCode()
    {
        return IdGuid.GetHashCode();
    }

    public override string ToString()
    {
        return $"{nameof(IdGuid)}: {IdGuid}, {nameof(AdServerHostname)}: {AdServerHostname}, {nameof(DistinguishedName)}: {DistinguishedName}, {nameof(DomainName)}: {DomainName}";
    }

    public void Init(AdConfiguration ad)
    {
        this.DomainName = ad.DomainName;
        this.OuUnit = ad.OuUnit;
        this.Department = ad.Department;
        this.DistinguishedName = ad.DistinguishedName;
        this.AdServerHostname = ad.AdServerHostname;
        this.IdGuid= ad.IdGuid;
        this.IsLoaded = true;
    }
}