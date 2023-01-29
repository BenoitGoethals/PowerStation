using PowerStationInfrastructure.Services;
using System;
using System.Collections.Generic;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerStationInfrastructure
{
    public  class Configuration
    {
        public PrincipalContext OuContex { get;}

        public AdConfiguration AdSetting { get; }

        public Configuration(AdConfiguration adConfiguration)
        {
            this.AdSetting = adConfiguration;
#pragma warning disable CA1416 // Validate platform compatibility
            OuContex = new PrincipalContext(ContextType.Domain, adConfiguration.DomainName, adConfiguration.DistinguishedName);
#pragma warning restore CA1416 // Validate platform compatibility


        }
    }
}
