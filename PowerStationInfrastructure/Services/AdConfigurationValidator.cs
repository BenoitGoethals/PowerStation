using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace PowerStationInfrastructure.Services
{
    internal class AdConfigurationValidator : AbstractValidator<AdConfiguration>
    {
        public AdConfigurationValidator()
        {
            RuleFor(config => config.DistinguishedName).NotEmpty();
            RuleFor(config => config.DomainName).NotEmpty();
            RuleFor(config => config.AdServerHostname).NotEmpty();
      
        }


    }
}
