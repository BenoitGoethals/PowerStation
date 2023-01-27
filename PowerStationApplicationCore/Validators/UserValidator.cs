using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PowerStationDomain.Domain;

namespace PowerStationApplicationCore.Validators
{
    internal class UserValidator : AbstractValidator<User>
    {
        public UserValidator()
        {
            RuleFor(user => user.DistinguishedName).NotEmpty();
            RuleFor(user => user.GivenName).NotEmpty();
            RuleFor(user => user.Enabled).NotNull();
            RuleFor(user => user.SamAccountName).NotEmpty();
            RuleFor(user => user.Name).NotEmpty();
            RuleFor(user => user.UserPrincipalName).NotEmpty();
            

        }
    }
}
