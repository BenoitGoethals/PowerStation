namespace PowerStationDomain.Domain;

public abstract class AdObject : IAdObject
{
    private readonly List<AdObject?> _adObjects = new();

    public string? Sid { get; } = default;
    public string? ObjectClass { get; set; } = default;
    public Guid ObjectGuid { get; } = Guid.NewGuid();
    public void AddMember(AdObject? adObject)
    {
        if (adObject == null || _adObjects.Exists(c => c.ObjectGuid == adObject.ObjectGuid)) return;
        if (adObject is User user)
        {
            if (_adObjects.OfType<User>().Any(u => u.SamAccountName == user.SamAccountName))
            {
                return;
            }
        }
        _adObjects.Add(adObject);
    }

    public void RemoveMember(AdObject? adObject)
    {
        _adObjects.Remove(adObject);
    }

    public List<AdObject?> AllMembers()
    {
        return _adObjects;
    }

    public List<AdObject?> AllMembers(Predicate<AdObject?> predicate)
    {
        return _adObjects.FindAll(predicate);
    }

    public AdObject? GetMember(Guid guid)
    {
        if (this.ObjectGuid.Equals(guid))
        {
            return this;
        }

        return _adObjects.FirstOrDefault(adObject => adObject != null && adObject.ObjectGuid.Equals(guid));
    }

    public void RemoveMembers()
    {

       _adObjects.Clear();

    }
}