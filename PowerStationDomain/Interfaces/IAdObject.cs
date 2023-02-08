using PowerStationDomain.models.AD;

namespace PowerStationDomain.Interfaces;

public interface IAdObject
{
    void AddMember(AdObject? adObject);
    void RemoveMember(AdObject? adObject);
    List<AdObject?> AllMembers();

    List<AdObject?> AllMembers(Predicate<AdObject?> predicate);
}