using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PowerStationDomain.models;

namespace PowerStationApplicationCore.Validators
{
    internal class UserValidator : AbstractValidator<User>
    {
        public UserValidator()
        {
           
            RuleFor(user => user.GivenName).NotEmpty();
            RuleFor(user => user.Enabled).NotNull();
            RuleFor(user => user.SamAccountName).NotEmpty();
            RuleFor(user => user.Name).NotEmpty();
            RuleFor(user => user.UserPrincipalName).NotEmpty();
            RuleFor(user => user.Company).NotEmpty();
            RuleFor(user => user.EmailAddress).EmailAddress();
            RuleFor(user => user.VoiceTelephoneNumber).NotEmpty();
            RuleFor(user => user.Department).NotEmpty();
            RuleFor(user => user.Mobile).NotEmpty();
            RuleFor(user => user.Surname).NotEmpty();
            RuleFor(user => user.SamAccountName).MinimumLength(3);
            RuleFor(user => user.UserPrincipalName).NotEmpty();


        }
    }
}
