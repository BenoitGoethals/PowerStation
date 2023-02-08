using PowerStationDomain.models.AD;

namespace PowerStationInfrastructure.Services.AD;

public interface IAdServiceOrganizationalUnit
{
    bool Add(OrganizationalUnit identity);
    bool Remove(OrganizationalUnit identity);
    bool Remove(string identity);
    List<OrganizationalUnit>? All();

    List<OrganizationalUnit>? All(string? level);
    OrganizationalUnit? Get(string identity);
}