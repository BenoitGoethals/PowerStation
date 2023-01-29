using System;
using FluentAssertions;
using Microsoft.Extensions.Logging.Abstractions;
using PowerStationDomain.models;
using PowerStationInfrastructure;
using PowerStationInfrastructure.Services;
using Xunit;

namespace PowerStationInfrastructureTests.Services
{
    public class AdServiceCodeUserTests : IDisposable
    {
        private IAdService<User>? _adService;

        void IDisposable.Dispose() => _adService = null;


        public AdServiceCodeUserTests()
        {
            var adConfiguration = new AdConfiguration("localhost", "OU=Users,OU=opslab,DC=opslab,DC=be", "opslabo");
            var configuration = new Configuration(adConfiguration);
            _adService = new AdServiceCodeUser(configuration, new NullLogger<AdServiceCodeUser>());
        }
        [Fact()]
        public void AllTest()
        {
            _adService?.All().Should().NotBeNull();

        }

        [Fact()]
        public void MakeUserTest()
        {
           // _adService?.Add(new User());

        }



        [Fact()]
        public void GetUserTest()
        {
            _adService?.GetUser("Benoit").Should().NotBeNull();

        }



        [Fact()]
        public void RemoveUserTest()
        {
         //   _adService?.Remove();

        }

    }


}