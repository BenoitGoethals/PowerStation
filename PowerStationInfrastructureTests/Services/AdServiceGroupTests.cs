using Xunit;
using PowerStationInfrastructure.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using FluentAssertions;
using Microsoft.Extensions.Logging.Abstractions;
using PowerStationDomain.models;
using PowerStationInfrastructure.mapper;

namespace PowerStationInfrastructure.Services.Tests
{
    public class AdServiceGroupTests
    {

        private readonly IAdServiceSecurity _adService;

        public AdServiceGroupTests()
        {
            var adConfiguration = new AdConfiguration("localhost", "OU=Users,OU=opslab,DC=opslab,DC=be", "opslabo");
            var configuration = new Configuration(adConfiguration);
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new SecurityGroupMapper());
                cfg.AddProfile(new UserMapper());
                //     cfg.DisableConstructorMapping();
            });
            var mapper = mockMapper.CreateMapper();

            _adService = new AdServiceGroup(configuration, new NullLogger<AdServiceCodeUser>(), mapper);
        }

        [Fact()]
        public void AddTest()
        {
            SecurityGroup group = new SecurityGroup("testGroup", GroupScope.Local) { Description = "test", DisplayName = "test" };
            _adService.Add(group).Should().BeTrue();
            _adService.Remove("testGroup").Should().BeTrue();
        }

        [Fact()]
        public void RemoveTest()
        {
            SecurityGroup group = new SecurityGroup("testGroup", GroupScope.Local) { Description = "test", DisplayName = "test" };
            _adService.Add(group).Should().BeTrue();
            _adService.Remove("testGroup").Should().BeTrue();
        }

        [Fact()]
        public void RemoveTest1()
        {
            SecurityGroup group = new SecurityGroup("testGroup", GroupScope.Local) { Description = "test", DisplayName = "test" };
            _adService.Add(group).Should().BeTrue();
            _adService.Remove(group).Should().BeTrue();
        }

        [Fact()]
        public void AllTest()
        {
            _adService.All().Should().HaveCountGreaterThan(2);
        }

        [Fact()]
        public void GetTest()
        {
            _adService.Get("Administrators").Should().NotBeNull();
        }

        [Fact()]
        public void GetAllUsersTest()
        {
            _adService.GetAllUsers("Administrators").Should().NotBeNull();
        }

        [Fact()]
        public void GetGroupsTest()
        {
            _adService.GetGroups("Administrator").Should().HaveCountGreaterThan(1);
        }
    }
}