using PowerStationDomain.models;

namespace PowerStationDomain.Interfaces;

public interface ISecurityGroup
{
    SecurityGroup? GetMember(string groupName);
}