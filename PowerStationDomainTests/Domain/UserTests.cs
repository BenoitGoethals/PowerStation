using System;
using FluentAssertions;
using PowerStationDomain.models;
using PowerStationInfrastructure.Services;
using Xunit;

namespace PowerStationDomainTests.Domain
{
    public class UserTests
    {
        [Fact()]
        public void UserTest()
        {
           
            User user = User.CreateInstance("Benoit", "Benoit", "Benoit", true, "benoit", "",AdUserType.Basic,"");
            user.SamAccountName.Should().NotBeEmpty();
            user.GivenName.Should().NotBeEmpty();
            user.UserPrincipalName.Should().NotBeEmpty();
            user.Name.Should().NotBeEmpty();
        }

    
    }
}