using PowerStationDomain.models;

namespace PowerStationInfrastructure.Services;

public interface IAdService<T> where T : AdObject
{
    void Add(T user);
    void Remove(T adUser);
    List<User> All();
}