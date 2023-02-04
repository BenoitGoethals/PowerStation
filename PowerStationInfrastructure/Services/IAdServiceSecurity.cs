using PowerStationDomain.models;

namespace PowerStationInfrastructure.Services;

public interface IAdServiceSecurity:IAdService<SecurityGroup>
{
    public bool IsMember(string? principal, string? group);

    public List<SecurityGroup>? GetGroups(string userName);


    public List<User> GetAllUsers(string groupName);
}