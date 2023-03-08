using AutoMapper;
using FluentAssertions;
using Microsoft.Extensions.Logging.Abstractions;
using PowerStationDomain.models.AD;
using PowerStationInfrastructure.helpers;
using PowerStationInfrastructure.mapper;
using PowerStationInfrastructure.Services;
using PowerStationInfrastructure.Services.AD;
using Xunit;

namespace PowerStationInfrastructureTests.Services
{
    public class AdComputerServiceTests
    {
        private readonly IAdComputerService? _adService;


        public AdComputerServiceTests()
        {
            var adConfiguration = new AdConfiguration("localhost");
            var configuration = new Configuration(adConfiguration);
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new SecurityGroupMapper());
                cfg.AddProfile(new UserMapper());
                cfg.AddProfile(new ComputerMapper());
                //     cfg.DisableConstructorMapping();
            });
            var mapper = mockMapper.CreateMapper();

            _adService = new AdComputerService(configuration, new NullLogger<AdComputerService>(), mapper);
        }

        [Fact()]
        public void AddTest()
        {
            Computer? computer = new Computer()
            {
                Description = "testcomputer",
                SamAccountName = "testComputer",
                DisplayName = "testComputer",
                Name = "testComputer",
                Enabled = true,
                UserPrincipalName = "testcomputer"

            };
            _adService?.Add(computer).Should().BeTrue();
            _adService?.Remove("testcomputer").Should().BeTrue();
        }



        [Fact()]
        public void RemoveTest1()
        {
            Computer? computer = new Computer()
            {
                Description = "testcomputer2",
                SamAccountName = "testComputer2",
                DisplayName = "testComputer2",
                Name = "testComputer2",
                Enabled = true,
                UserPrincipalName = "testcomputer2"

            };
            _adService?.Add(computer).Should().BeTrue();
            _adService?.Remove(computer).Should().BeTrue();
        }

        [Fact()]
        public void AllTest()
        {
            Computer? computer = new Computer()
            {
                Description = "testcomputer2",
                SamAccountName = "testComputer2",
                DisplayName = "testComputer2",
                Name = "testComputer2",
                Enabled = true,
                UserPrincipalName = "testcomputer2"

            };
            Computer? computer2 = new Computer()
            {
                Description = "testcomputer2f",
                SamAccountName = "testComputer2f",
                DisplayName = "testComputer2f",
                Name = "testComputer2f",
                Enabled = true,
                UserPrincipalName = "testcomputer2f"

            };
            _adService?.Add(computer).Should().BeTrue();
            _adService?.Add(computer2).Should().BeTrue();
            _adService?.All().Should().HaveCountGreaterOrEqualTo(2);
            _adService?.Remove(computer).Should().BeTrue();
            _adService?.Remove(computer2).Should().BeTrue();
        }

        [Fact()]
        public void GetTest()
        {
            Computer? computer = new Computer()
            {
                Description = "testcomputer2a",
                SamAccountName = "testComputer2a",
                DisplayName = "testComputer2a",
                Name = "testComputer2a",
                Enabled = true,
                UserPrincipalName = "testcomputer2a"

            };
            _adService?.Add(computer).Should().BeTrue();
            _adService?.Get("testcomputer2a")?.SamAccountName.Should().BeEquivalentTo(computer.SamAccountName);
            _adService?.Remove(computer).Should().BeTrue();
        }

        [Fact()]
        public void GetComputersInGroupTest()
        {
            _adService?.GetComputersInGroup("Domain Computers").Should().NotBeNull();
        }

       
    }
}