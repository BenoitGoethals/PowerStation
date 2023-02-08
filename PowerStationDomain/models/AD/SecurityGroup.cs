using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using PowerStationDomain.Interfaces;

namespace PowerStationDomain.models.AD
{
    public class SecurityGroup : AdObject, ISecurityGroup, IEquatable<SecurityGroup?>
    {
        public SecurityGroup(string? groupName, GroupScope scope)
        {
            GroupName = groupName;
            Scope = scope;
            Name = groupName;
        }

        public string? GroupName { get; set; }
        public GroupScope Scope { get; set; }

        public SecurityGroup()
        {
        }


        public bool? IsSecurityGroup { get; set; } = true;



        public override bool Equals(object? obj)
        {
            return Equals(obj as SecurityGroup);
        }

        public bool Equals(SecurityGroup? other)
        {
            return other is not null &&
                   ObjectGuid.Equals(other.ObjectGuid);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(ObjectGuid);
        }

        public SecurityGroup? GetMember(string groupName)
        {

            return AllMembers().OfType<SecurityGroup>().FirstOrDefault(g => g.GroupName != null && g.GroupName.Equals(groupName));
        }

        public static bool operator ==(SecurityGroup? left, SecurityGroup? right)
        {
            return EqualityComparer<SecurityGroup>.Default.Equals(left, right);
        }

        public static bool operator !=(SecurityGroup? left, SecurityGroup? right)
        {
            return !(left == right);
        }
    }
}
