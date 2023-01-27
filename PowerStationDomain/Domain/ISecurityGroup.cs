namespace PowerStationDomain.Domain;

public interface ISecurityGroup
{
    void Add(AdObject adObject);
    void Remove(AdObject adObject);
    List<AdObject> AllAdObjects();

    List<AdObject> AllAdObjects(Predicate<AdObject> predicate);
}