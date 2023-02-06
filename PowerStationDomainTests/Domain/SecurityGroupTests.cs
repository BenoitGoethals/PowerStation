using FluentAssertions;
using PowerStationDomain.models;
using System;
using System.Text.RegularExpressions;
using Xunit;

namespace PowerStationDomainTests.Domain
{
    public class SecurityGroupTests
    {
        private User user;
        private User user2;

        public SecurityGroupTests()
        {
           //  user ;//= User.CreateInstance("Benoit", "Benoit", "Benoit", true, "benoit", "", AdUserType.Basic, "");
            // user2 ;= User.CreateInstance("Benoit", "Benoit", "Benoit", true, "benoit", "", AdUserType.Basic,"");

        }
        [Fact()]
        public void SecurityGroupTest()
        {
            SecurityGroup group = new SecurityGroup("tst",GroupScope.Local);
         
            group.AddMember(new SecurityGroup("tst2", GroupScope.Local));
            group.AddMember(new SecurityGroup("tst20", GroupScope.Local));

            group.AllMembers().Should().HaveCount(2);
        }


       
     //   [Fact()]
        public void SecurityGroupUsersTest()
        {
            SecurityGroup group = new SecurityGroup("tst", GroupScope.Local);
            
            group.AddMember(user);
            group.AddMember(user2);

            group.AllMembers().Should().HaveCount(1);
        }

        [Fact()]
        public void SecurityGroupUsers2Test()
        {
            SecurityGroup group = new SecurityGroup("tst", GroupScope.Local);
            SecurityGroup? group2 = new SecurityGroup("tst20", GroupScope.Local);
            group.AddMember(new SecurityGroup("tst2", GroupScope.Local));
         
            group2.AddMember(user);
            group2.AddMember(user2);
            group.AddMember(group2);
            group.AllMembers().Should().HaveCount(2);
        }

        [Fact()]
        public void SecurityGroupAddNullUserTest()
        {
            SecurityGroup group = new SecurityGroup("tst", GroupScope.Local);
         
            group.AddMember(null);
        
            group.AllMembers().Should().HaveCount(0);
        }

     //   [Fact()]
        public void SecurityGroupGetAGroupByGuidTest()
        {
            SecurityGroup group = new SecurityGroup("tst", GroupScope.Local);
            SecurityGroup? group2 = new SecurityGroup("tst20", GroupScope.Local);
            group.AddMember(new SecurityGroup("tst2", GroupScope.Local));

            group2.AddMember(user);
            group2.AddMember(user2);
            group.AddMember(group2);
            group.AllMembers().Should().HaveCount(2);
           
            group.GetMember(user2.ObjectGuid)?.ObjectGuid.Should().Be(user2.ObjectGuid);
            group.GetMember(user.ObjectGuid)?.ObjectGuid.Should().Be(user.ObjectGuid);
            group.GetMember(user.ObjectGuid)?.ObjectGuid.Should().Be(user.ObjectGuid);
        }

        [Fact()]
        public void SecurityGroupGetAGroupByGroupNameTest()
        {
            SecurityGroup group = new SecurityGroup("tst", GroupScope.Local);
            SecurityGroup? group2 = new SecurityGroup("tst20", GroupScope.Local);
            group.AddMember(new SecurityGroup("tst2", GroupScope.Local));

            group2.AddMember(user);
            group2.AddMember(user2);
            group.AddMember(group2);
            if (group2.GroupName != null) group.GetMember(group2.GroupName)?.ObjectGuid.Should().Be(group2.ObjectGuid);
            group.AllMembers().Should().HaveCount(2);
        }


        [Fact()]
        public void SecurityGroupGetAllUserFromAGroupTest()
        {
            SecurityGroup group = new SecurityGroup("tst", GroupScope.Local);
            SecurityGroup? group2 = new SecurityGroup("tst20", GroupScope.Local);
            group.AddMember(new SecurityGroup("tst2", GroupScope.Local));

            group2.AddMember(user);
            group2.AddMember(user2);
            group.AddMember(group2);
            group.AllMembers().Should().HaveCount(2);
        }

        [Fact()]
        public void SecurityGroupGetAllUserEmpty()
        {
            SecurityGroup group = new SecurityGroup("tst", GroupScope.Local);
            group.AllMembers().Should().HaveCount(0);
        }

        [Fact()]
        public void SecurityGroupGetAllUserEmptywithname()
        {
            SecurityGroup group = new SecurityGroup("tst", GroupScope.Local);
            group.GetMember("nono").Should().BeNull();
            group.AllMembers().Should().HaveCount(0);
        }

        [Fact()]
        public void SecurityGroupRemoveUser()
        {
            SecurityGroup group = new SecurityGroup("tst", GroupScope.Local);
            SecurityGroup? group2 = new SecurityGroup("tst20", GroupScope.Local);
            group.AddMember(new SecurityGroup("tst2", GroupScope.Local));

            group2.AddMember(user);
            group2.AddMember(user2);
            group.AddMember(group2);
            group.AllMembers().Should().HaveCount(2);
            group2.RemoveMember(user);
            group2.AllMembers().Should().HaveCount(0);
            group.RemoveMember(group2);
            group.AllMembers().Should().HaveCount(1);
          
        }

        [Fact()]
        public void SecurityGroupRemoveUserAll()
        {
            SecurityGroup group = new SecurityGroup("tst", GroupScope.Local);
            SecurityGroup? group2 = new SecurityGroup("tst20", GroupScope.Local);
            group.AddMember(new SecurityGroup("tst2", GroupScope.Local));

             group2.AddMember(user);
            group2.AddMember(user2);
            group.AddMember(group2);
            group.AllMembers().Should().HaveCount(2);
            
            group.RemoveMembers();
            group.AllMembers().Should().HaveCount(0);
        }


    }
}