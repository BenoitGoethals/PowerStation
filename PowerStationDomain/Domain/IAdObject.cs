namespace PowerStationDomain.Domain;

public interface IAdObject
{
    void AddMember(AdObject? adObject);
    void RemoveMember(AdObject? adObject);
    List<AdObject?> AllMembers();

    List<AdObject?> AllMembers(Predicate<AdObject?> predicate);
}