using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using PowerStationDomain.models;

namespace PowerStationApplicationCore.Validators
{
    public class SecurityValidator:AbstractValidator<SecurityGroup>
    {

        public SecurityValidator()
        {
            RuleFor(group => group.GroupName).NotEmpty();
            RuleFor(group => group.Scope).NotNull();
            RuleFor(group => group.IsSecurityGroup).NotNull();
            RuleFor(group => group.Name).NotNull();
            RuleFor(group => group.SamAccountName).NotNull();
            RuleFor(group => group.Description).NotNull();
            RuleFor(group => group.DisplayName).NotNull();

        }
    }
}
