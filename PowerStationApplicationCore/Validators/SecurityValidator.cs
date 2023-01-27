using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using PowerStationDomain.Domain;

namespace PowerStationApplicationCore.Validators
{
    public class SecurityValidator:AbstractValidator<SecurityGroup>
    {

        public SecurityValidator()
        {
            RuleFor(group => group.GroupName).NotEmpty();
            RuleFor(group => group.Scope).NotNull();
          

        }
    }
}
