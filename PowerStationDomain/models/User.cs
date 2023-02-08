using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using PowerStationDomain.models.AD;

namespace PowerStationDomain.models
{
    public class User : AdObject
    {
        public User()
        {
            
        }

        public AdUserType UserType { get; set; }
        public string? AccountExpirationData { get; set; }
        public string? AccountLockoutTime { get; set; }
        public bool? AllowReversiblePasswordEncryption { get; set; }
        public int? BadLogonCount { get; set; }
        [MaxLength(10, ErrorMessage = "The Company field should not contain more than 10 characters.")]
        [MinLength(3, ErrorMessage = "The Company field must contain at least 3 characters.")]
        public string? Company { get; set; }
        public Boolean? DelegationPermitted { get; set; }
        [MaxLength(10, ErrorMessage = "The Department field should not contain more than 10 characters.")]
        [MinLength(3, ErrorMessage = "The Department field must contain at least 3 characters.")]
        public string? Department { get; set; }
        [EmailAddress(ErrorMessage = "The EmailAddress field is required.")]
        public string? EmailAddress { get; set; }
        public string? EmployeeId { get; set; }
        public bool? Enabled { get; set; }
        [MaxLength(10, ErrorMessage = "The GivenName field should not contain more than 10 characters.")]
        [MinLength(3, ErrorMessage = "The GivenName field must contain at least 3 characters.")]
        public string? GivenName { get; set; }
       
        public string? HomeDirectory { get; set; }
        public string? HomeDrive { get; set; }
        public string? LastBadPasswordAttempt { get; set; }
        public string? LastLogon { get; set; }
        public string? LastPasswordSet { get; set; }
        public string? Manager { get; set; }
        public List<string>? MemberOf { get; set; }
        [MaxLength(10, ErrorMessage = "The MiddleName field should not contain more than 10 characters.")]
        [MinLength(3, ErrorMessage = "The MiddleName field must contain at least 3 characters.")]
        public string? MiddleName { get; set; }
        [MaxLength(10, ErrorMessage = "The Mobile field should not contain more than 10 characters.")]
        [MinLength(3, ErrorMessage = "The Mobile field must contain at least 3 characters.")]
        public string? Mobile { get; set; }
    
        public Boolean? PasswordNeverExpires { get; set; }
        public Boolean? PasswordNotRequired { get; set; }
        public string? ProfilePath { get; set; }
     
        public string? ScriptPath { get; set; }
      
        public Boolean? SmartCardLogonRequired { get; set; }
        [MaxLength(10, ErrorMessage = "The Surname field should not contain more than 10 characters.")]
        [MinLength(3, ErrorMessage = "The Surname field must contain at least 3 characters.")]
        public string? Surname { get; set; }
        public Boolean? UserCannotChangePassword { get; set; }
        [MaxLength(10, ErrorMessage = "The VoiceTelephoneNumber field should not contain more than 10 characters.")]
        [MinLength(0, ErrorMessage = "The VoiceTelephoneNumber field must contain at least 3 characters.")]
      
        public string? VoiceTelephoneNumber { get; set; }

      
    }



}

