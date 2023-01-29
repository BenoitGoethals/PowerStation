using FluentAssertions;
using PowerStationInfrastructure.helpers;
using Xunit;

namespace PowerStationInfrastructureTests.helpers
{
    public class AdHelperTests
    {
        [Fact()]
        public void DomainExistTest()
        {
            AdHelper.DomainExist("OPSLABO.BE").Should().BeTrue();
        }
    }
}