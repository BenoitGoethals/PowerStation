using PowerStationDomain.models;

namespace PowerStationInfrastructure.Services;

public interface IAdService<T>:IDisposable where T : AdObject
{
    bool Add(T identity);
    bool Remove(T identity);

    bool Remove(string identity);
    List<T>? All();

    T? Get(string username);
}