using AutoMapper;
using FluentAssertions;
using Microsoft.Extensions.Logging.Abstractions;
using PowerStationDomain.models.AD;
using PowerStationInfrastructure.mapper;
using PowerStationInfrastructure.Services.AD;
using Xunit;

namespace PowerStationInfrastructureTests.Services
{
    public class AdServiceGroupTests
    {

        private readonly IAdServiceSecurity _adService;

        public AdServiceGroupTests()
        {
            var configuration = new AdConfiguration("localhost");
            
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new SecurityGroupMapper());
                cfg.AddProfile(new UserMapper());
                //     cfg.DisableConstructorMapping();
            });
            var mapper = mockMapper.CreateMapper();

            _adService = new AdServiceGroup(configuration, new NullLogger<AdServiceUser>(), mapper);
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
            _adService.All().Should().HaveCountGreaterThan(1);
        }

        [Fact()]
        public void GetTest()
        {
            _adService.Get("Administrators",true).Should().NotBeNull();
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

        [Fact()]
        public void IsMemberTest()
        {
            _adService.IsMember("Administrator","Administrators");
        }

        

        [Fact()]
        public void GetGroupsInGroupTest()
        {
            _adService.GetGroupsInGroup("Administrators").Should().NotBeNull();
        }

        
    }
}