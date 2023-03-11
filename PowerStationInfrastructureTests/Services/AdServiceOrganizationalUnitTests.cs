using AutoMapper;
using FluentAssertions;
using Microsoft.Extensions.Logging.Abstractions;
using PowerStationDomain.models;
using PowerStationInfrastructure.helpers;
using PowerStationInfrastructure.mapper;
using System.Xml.Linq;
using PowerStationDomain.models.AD;
using Xunit;
using PowerStationInfrastructure.Services.AD;

namespace PowerStationInfrastructureTests.Services
{
    public class AdServiceOrganizationalUnitTests
    {
        private readonly AdServiceOrganizationalUnit _adService;
        private readonly AdConfiguration _configuration;
        public AdServiceOrganizationalUnitTests()
        {
            var _configuration = new AdConfiguration("localhost");
           
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new UserMapper());
                //     cfg.DisableConstructorMapping();
            });
            var mapper = mockMapper.CreateMapper();

            _adService = new AdServiceOrganizationalUnit(_configuration, new NullLogger<AdServiceUser>(), mapper);
        }

        

        [Fact()]
        public void RemoveTest()
        {
            var ou = new OrganizationalUnit()
            {
                Description = "TestOU",
                Name = "TestOU",

            };
            _adService.Add(ou).Should().BeTrue();

            _adService.Remove(ou).Should().BeTrue(); ;
        }

       

        [Fact()]
        public void AllTest()
        {
            _adService.All().Should().HaveCountGreaterOrEqualTo(1);
        }

        [Fact()]
        public void AllTestOU()
        {
            _adService.All(_configuration.OuUnit?.Root).Should().HaveCountGreaterOrEqualTo(1);
        }

        [Fact()]
        public void GetTest()
        {
            Assert.True(false, "This test needs an implementation");
        }
    }
}