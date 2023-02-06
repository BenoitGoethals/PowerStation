using System;
using System.Collections.Generic;
using AutoMapper;
using FluentAssertions;
using Microsoft.Extensions.Logging.Abstractions;
using PowerStationDomain.models;
using PowerStationInfrastructure;
using PowerStationInfrastructure.mapper;
using PowerStationInfrastructure.Services;
using Xunit;


namespace PowerStationInfrastructureTests.Services
{
    public class AdServiceUserTests : IDisposable
    {
        private IAdServiceUser? _adService;

        void IDisposable.Dispose() => _adService = null;


        [Fact()]
        public void DisableUserTest()
        {
            User user = new User()
            {
                SamAccountName = "ilse",
                Name = "ilse",
                GivenName = "ilse",
                Company = "ilse",
                Department = "dept",
                DistinguishedName = "ops",
                Surname = "ilse",
                EmailAddress = "ilse.goethals@gmail.com",
                Description = "test",
                Enabled = true


            };
            _adService?.Add(user).Should().BeTrue();
            _adService?.DisableUser("ilse").Should().BeTrue();
            _adService?.Get("ilse")?.Enabled.Should().BeFalse();
            _adService?.Remove(user).Should().BeTrue();

        }

        [Fact()]
        public void EnableUserTest()
        {
            User user = new User()
            {
                SamAccountName = "ilse",
                Name = "ilse",
                GivenName = "ilse",
                Company = "ilse",
                Department = "dept",
                DistinguishedName = "ops",
                Surname = "ilse",
                EmailAddress = "ilse.goethals@gmail.com",
                Description = "test",
                Enabled = true


            };
            _adService?.Add(user).Should().BeTrue();
            _adService?.DisableUser("ilse").Should().BeTrue();
            _adService?.Get("ilse").Enabled.Should().BeFalse();
            _adService?.EnableUser("ilse").Should().BeTrue();
            _adService?.Get("ilse").Enabled.Should().BeTrue();
            _adService?.Remove(user).Should().BeTrue();
        }

        [Fact()]
        public void GetUserByEmailTest()
        {
            User user = new User()
            {
                SamAccountName = "ilse",
                Name = "ilse",
                GivenName = "ilse",
                Company = "ilse",
                Department = "dept",
                DistinguishedName = "ops",
                Surname = "ilse",
                EmailAddress = "ilse.goethals@gmail.com",
                Description = "test",
                Enabled = true


            };
            _adService?.GetUserByEmail("ilse.goethals@gmail.com")
                ?.EmailAddress.Should()
                .BeEquivalentTo(user.EmailAddress);
        }

        public AdServiceUserTests()
        {
            var adConfiguration = new AdConfiguration("localhost");
            var configuration = new Configuration(adConfiguration);
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new UserMapper());
                //     cfg.DisableConstructorMapping();
            });
            var mapper = mockMapper.CreateMapper();

            _adService = new AdServiceUser(configuration, new NullLogger<AdServiceUser>(), mapper);
        }
        [Fact()]
        public void AllTest()
        {
            _adService?.All().Should().NotBeNull();

        }

        [Fact()]
        public void MakeUserRemoveTest()
        {
            User user = new User()
            {
                SamAccountName = "benuser0100",
                Name = "benuser0100",
                GivenName = "benuser0100",
                Company = "user0100",
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

        [Fact()]
        public void GetUserAdministratorTest()
        {
            var userRet = _adService?.Get("administrator");
            userRet.Should().NotBeNull();

        }

        [Fact()]
        public void GetUserTest()
        {
            User user = new User()
            {
                SamAccountName = "benoituser0100",
                Name = "benoituser0100",
                GivenName = "benoituser0100",
                Company = "benoituser0100",
                Department = "dept",
                DistinguishedName = "ops",
                Surname = "benoit",
                EmailAddress = "benoit.goethals@gmail.com",
                Description = "test",
                Enabled = true


            };
            _adService?.Add(user).Should().BeTrue();
            var userRet = _adService?.Get("benoituser0100");
            userRet.Should().NotBeNull();
            _adService?.Remove("benoituser0100").Should().BeTrue();
        }

        [Fact()]
        public void RemoveUserBystringTest()
        {
            var user = new User()
            {
                SamAccountName = "benoituser01001",
                Name = "benoituser01001",
                GivenName = "benoituser01001",
                Company = "benoituser01010",
                Department = "dept1",
                DistinguishedName = "ops",
                Surname = "benoit",
                EmailAddress = "benoit.goethals@gmail.com",
                Description = "test",
                Enabled = true


            };
            _adService?.Add(user).Should().BeTrue();

            _adService?.Remove("benoituser01001").Should().BeTrue();
        }

        [Fact()]
        public void StressTest()
        {
            var start = _adService?.All()?.Count;
            foreach (var user in GenerateUsersStressTest())
            {
                _adService?.Add(user).Should().BeTrue();
            }

            foreach (var user in GenerateUsersStressTest())
            {
                _adService?.Remove(user).Should().BeTrue();
            }
            var end = _adService?.All()?.Count;
            start.Should().Be(end);


        }



        private List<User> GenerateUsersStressTest()
        {
            List<User> users = new List<User>();

            for (int i = 0; i < 100; i++)
            {
                users.Add(new User()
                {
                    SamAccountName = "TestUser" + i,
                    Name = "TestUser" + i,
                    GivenName = "TestUser" + i,
                    Company = "testc",
                    Department = "testc",

                    Surname = "testbenoit" + i,
                    EmailAddress = $"benoit.goethals{i}@gmail.com",
                    Description = "test",
                    Enabled = true
                });
            }




            return users;

        }


    }


}