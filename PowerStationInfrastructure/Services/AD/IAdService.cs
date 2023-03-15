using PowerStationDomain.models.AD;

namespace PowerStationInfrastructure.Services.AD;

public interface IAdService<T> : IDisposable where T : AdObject
{
    bool Add(T identity);
    bool Remove(T identity);

    bool Remove(string identity);
    List<T>? All();

    T? Get(string identity,bool topLevel);
   
}