namespace PowerStationDomain.Domain;

public interface ISecurityGroup
{
    SecurityGroup? GetMember(string groupName);
}