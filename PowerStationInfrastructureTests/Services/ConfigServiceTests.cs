using System;
using System.IO;
using System.Reflection;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using PowerStationInfrastructure.Services;
using Xunit;

namespace PowerStationInfrastructureTests.Services
{
    public class ConfigServiceTests:IDisposable
    {
       
        private readonly string _fullPath;

        public ConfigServiceTests()
        {
            _fullPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? string.Empty, "ConfigPower.xml");
            File.Delete(_fullPath);
        }
        [Fact]
        public void SaveDefaultTest()
        {
            var testDefault = AdConfiguration.Default;
            var configService = new ConfigService(_fullPath, new NullLogger<ConfigService>());

            configService.Save(testDefault);
            File.Exists(_fullPath).Should().BeTrue();

        }

        [Fact]
        public void SaveLoadDefaultTest()
        {
            var testDefault = AdConfiguration.Default;
            var configService = new ConfigService(_fullPath, new NullLogger<ConfigService>());

            configService.Save(testDefault);
            File.Exists(_fullPath).Should().BeTrue();

            var loaded=configService.Load();
            loaded.Should().BeEquivalentTo(testDefault);
        }

        [Fact]
        public void SaveTest()
        {
            var testDefault = new AdConfiguration("localhost","dn","PowerStation");
            var configService = new ConfigService(_fullPath, new NullLogger<ConfigService>());

            configService.Save(testDefault);
            File.Exists(_fullPath).Should().BeTrue();

        }

        [Fact]
        public void SaveLoadTest()
        {
            var testDefault = new AdConfiguration("localhost", "dn", "PowerStation");
            var configService = new ConfigService(_fullPath, new NullLogger<ConfigService>());

            configService.Save(testDefault);
            File.Exists(_fullPath).Should().BeTrue();

            var loaded = configService.Load();
            loaded.Should().BeEquivalentTo(testDefault);
        }

        [Fact]
        public void SaveBadTest()
        {
            var testDefault = new AdConfiguration("", "dn", "");
            var configService = new ConfigService(_fullPath, new NullLogger<ConfigService>());

            configService.Save(testDefault).Should().BeFalse();
            File.Exists(_fullPath).Should().BeFalse();

         
        }

        public void Dispose()
        {
            File.Delete(_fullPath);
        }
    }
}