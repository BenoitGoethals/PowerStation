using System;
using FluentAssertions;
using PowerStationDomain.models.AD;
using PowerStationInfrastructure.Services;
using Xunit;

namespace PowerStationDomainTests.Domain
{
    public class UserTests
    {
        [Fact()]
        public void UserTest()
        {

            User user = new User();
            user.SamAccountName.Should().NotBeEmpty();
            user.GivenName.Should().NotBeEmpty();
            user.UserPrincipalName.Should().NotBeEmpty();
            user.Name.Should().NotBeEmpty();
        }

    
    }
}