using PowerStationDomain.models;

namespace PowerStationInfrastructure.Services;

public interface IAdService<in T>:IDisposable where T : AdObject
{
    void Add(T user);
    void Remove(T adUser);
    List<User> All();

    User? GetUser(string username);
}