using System;
using AutoMapper;
using FluentAssertions;
using Microsoft.Extensions.Logging.Abstractions;
using PowerStationApplicationCore.mappers;
using PowerStationDomain.models;
using PowerStationInfrastructure;
using PowerStationInfrastructure.mapper;
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
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new UserMapper());
           //     cfg.DisableConstructorMapping();
            });
            var mapper = mockMapper.CreateMapper();

            _adService = new AdServiceCodeUser(configuration, new NullLogger<AdServiceCodeUser>(), mapper);
        }
        [Fact()]
        public void AllTest()
        {
            _adService?.All().Should().NotBeNull();

        }

        [Fact()]
        public void MakeUserTest()
        {
            User user = new User()
            {
                SamAccountName = "user0100",
                Name = "user0100",
                GivenName = "user0100",
                Company = "user0100",
                Department = "dept",
                DistinguishedName = "ops",
                Surname = "benoit",
                EmailAddress = "benoit.goethals@gmail.com",
                Description = "test",
                Enabled = true


            };
            _adService?.Add(user).Should().BeTrue();

        }



        [Fact()]
        public void GetUserTest()
        {
            _adService?.Get("Benoit").Should().NotBeNull();

        }



        [Fact()]
        public void RemoveUserTest()
        {

            User user = new User()
            {
                SamAccountName = "user010000",
                Name = "user010000",
                GivenName = "user010000",
                Company = "ops",
                Department = "dept",
                DistinguishedName = "ops",
                Surname = "benoit",
                EmailAddress = "benoit.goethals@gmail.com",
                Description = "test",
                Enabled = true


            };
            _adService?.Add(user).Should().BeTrue();
            _adService?.Remove(user).Should().BeTrue();

        }

    }


}