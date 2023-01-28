using System.Collections.Generic;
using FluentAssertions;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.VisualStudio.TestPlatform.Utilities.Helpers;
using PowerStationInfrastructure.helpers;
using PowerStationInfrastructure.Services;
using Xunit;

namespace PowerStationInfrastructureTests.Services
{
    public class CsvServiceTests
    {
        [Fact()]
        public void LoadCsvTest()
        {
           CsvService service = new CsvService(new NullLogger<CsvService>());
           var result =service.LoadCsv("resources.NewUsers.csv");
           result.Should().NotBeNull();
        }
    }
}