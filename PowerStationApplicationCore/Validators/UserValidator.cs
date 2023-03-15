using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PowerStationDomain.models.AD;

namespace PowerStationApplicationCore.Validators
{
    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator()
        {
           
            RuleFor(user => user.GivenName).NotEmpty().MaximumLength(25); ;
            RuleFor(user => user.Surname).NotEmpty().MaximumLength(25); ;
            RuleFor(user => user.Enabled).NotNull();
            RuleFor(user => user.SamAccountName).NotEmpty();
            RuleFor(user => user.Name).NotEmpty();
            RuleFor(user => user.UserPrincipalName).NotEmpty();
            RuleFor(user => user.Company).NotEmpty().MinimumLength(3).MaximumLength(25);
            RuleFor(user => user.Department).NotEmpty().MinimumLength(3).MaximumLength(25);
            RuleFor(user => user.EmailAddress).EmailAddress();
           // RuleFor(user => user.VoiceTelephoneNumber).NotEmpty();
            RuleFor(user => user.Department).NotEmpty();
          // RuleFor(user => user.Mobile).NotEmpty();
            RuleFor(user => user.MiddleName).NotEmpty().MinimumLength(3).MaximumLength(25);
            RuleFor(user => user.SamAccountName).MinimumLength(3).MaximumLength(20);
          


        }
    }
}
