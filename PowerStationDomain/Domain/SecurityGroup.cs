using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PowerStationDomain.Interfaces;

namespace PowerStationDomain.Domain
{
    public class SecurityGroup:AdObject,ISecurityGroup
    {
        public SecurityGroup(string? groupName, GroupScope scope)
        {
            GroupName = groupName;
            Scope = scope;
        }

        public string? GroupName { get; set; }
        public GroupScope Scope { get; set; }


        public SecurityGroup? GetMember(string groupName)
        {

            return base.AllMembers().OfType<SecurityGroup>().FirstOrDefault(g => g.GroupName != null && g.GroupName.Equals(groupName));
        }

    }
}
