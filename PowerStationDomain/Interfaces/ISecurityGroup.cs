using PowerStationDomain.Domain;

namespace PowerStationDomain.Interfaces;

public interface ISecurityGroup
{
    SecurityGroup? GetMember(string groupName);
}