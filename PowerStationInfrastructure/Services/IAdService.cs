using PowerStationDomain.models;

namespace PowerStationInfrastructure.Services;

public interface IAdService<in T>:IDisposable where T : AdObject
{
    bool Add(T user);
    bool Remove(T adUser);
    List<User> All();

    User? Get(string username);
}