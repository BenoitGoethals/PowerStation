using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using PowerStationDomain.models.Orginisation;
using PowerStationDomain.models.Orginization;
using PowerStationInfrastructure.Services.AD;
using PowerStationInfrastructure.Services.External;
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
            var departmentTop = new Department();

            var departments = new List<Department>()
            {
                new Department()
                {
                    Id = 1,
                    Description = "IT",
                    Name = "IT",
                    ParentDepartment = departmentTop

                },

                new Department()
                {
                    Id = 2,
                    Description = "HR",
                    Name = "HR",
                    ParentDepartment = departmentTop
                },

                new Department()
                {
                    Id = 3,
                    Description = "SALES",
                    Name = "SALES",
                    ParentDepartment = departmentTop
                },

                new Department()
                {
                    Id = 4,
                    Description = "MANAGEMENT",
                    Name = "MANAGEMENT",
                    ParentDepartment = departmentTop
                },
                new Department()
                {
                    Id = 5,
                    Description = "ADMIN",
                    Name = "ADMIN",
                    ParentDepartment = departmentTop
                },
            };
            departments.ForEach(d => departmentTop.AddDepartment(d));
           
            testDefault.Department = departmentTop;
            var configService = new ConfigService(_fullPath, new NullLogger<ConfigService>());

            configService.Save(testDefault);
            File.Exists(_fullPath).Should().BeTrue();
            var loaded = configService.Load();
            loaded.Should().BeEquivalentTo(testDefault);

        }

      
        public void Dispose()
        {
            File.Delete(_fullPath);
        }
    }
}