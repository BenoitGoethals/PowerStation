namespace PowerStationDomain.Domain;

public abstract class AdObject
{
    protected AdObject(string sid, string objectClass, Guid objectGuid)
    {
        Sid = sid;
        ObjectClass = objectClass;
        ObjectGuid = objectGuid;
    }

    public string Sid  { get; }
    public string ObjectClass { get; set; }
    public Guid ObjectGuid { get; set; }
}