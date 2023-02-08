using PowerStationDomain.models.AD;

namespace PowerStationDomain.Interfaces;

public interface ISecurityGroup
{
    SecurityGroup? GetMember(string groupName);
}