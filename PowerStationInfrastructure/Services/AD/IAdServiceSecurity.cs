using PowerStationDomain.models.AD;

namespace PowerStationInfrastructure.Services.AD;

public interface IAdServiceSecurity : IAdService<SecurityGroup>
{
    public bool IsMember(string? principal, string? group);

    public List<SecurityGroup>? GetGroups(string userName);


    public List<User> GetAllUsers(string groupName);

    public List<SecurityGroup?> GetGroupsInGroup(string group);
}