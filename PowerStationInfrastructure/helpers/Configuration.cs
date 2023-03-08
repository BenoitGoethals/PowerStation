using PowerStationInfrastructure.Services.AD;
using System;
using System.Collections.Generic;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerStationInfrastructure.helpers
{
    public class Configuration : IDisposable
    {
        public void Dispose()
        {
            OuContex.Dispose();
        }

        public PrincipalContext OuContex { get; }

        public AdConfiguration AdSetting { get; }

        public Configuration(AdConfiguration adConfiguration)
        {
            AdSetting = adConfiguration;
#pragma warning disable CA1416 // Validate platform compatibility
            OuContex = new PrincipalContext(ContextType.Domain, Environment.UserDomainName);
#pragma warning restore CA1416 // Validate platform compatibility


        }
    }
}
