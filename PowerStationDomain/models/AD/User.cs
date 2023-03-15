using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace PowerStationDomain.models.AD
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
        [Required]
        [MaxLength(50, ErrorMessage = "The Company field should not contain more than 50 characters.")]
        [MinLength(3, ErrorMessage = "The Company field must contain at least 3 characters.")]
        public string? Company { get; set; }
        public bool? DelegationPermitted { get; set; }
        [Required]
        [MaxLength(50, ErrorMessage = "The Department field should not contain more than 50 characters.")]
        [MinLength(3, ErrorMessage = "The Department field must contain at least 3 characters.")]
        public string? Department { get; set; }
        [EmailAddress(ErrorMessage = "The EmailAddress field is required.")]
        public string? EmailAddress { get; set; }
        public string? EmployeeId { get; set; }
        public bool? Enabled { get; set; } = true;
        [Required]
        [MaxLength(50, ErrorMessage = "The Department field should not contain more than 50 characters.")]
        [MinLength(3, ErrorMessage = "The GivenName field must contain at least 3 characters.")]
        public string? GivenName { get; set; }
        public string? HomeDirectory { get; set; }
        public string? HomeDrive { get; set; }
        public string? LastBadPasswordAttempt { get; set; }
        public string? LastLogon { get; set; }
        public string? LastPasswordSet { get; set; }
        public string? Manager { get; set; }
        public List<string>? MemberOf { get; set; }
        [Required]
        [MaxLength(50, ErrorMessage = "The Department field should not contain more than 50 characters.")]
        [MinLength(3, ErrorMessage = "The MiddleName field must contain at least 3 characters.")]
        public string? MiddleName { get; set; }

        [DataType(DataType.PhoneNumber)]
        [Phone]
        public string? Mobile { get; set; }
        public bool? PasswordNeverExpires { get; set; }
        public bool? PasswordNotRequired { get; set; }
        public string? ProfilePath { get; set; }
        public string? ScriptPath { get; set; }
        public bool? SmartCardLogonRequired { get; set; }
        [Required]
        [MaxLength(50, ErrorMessage = "The Department field should not contain more than 50 characters.")]
        [MinLength(3, ErrorMessage = "The Surname field must contain at least 3 characters.")]
        public string? Surname { get; set; }
        public bool? UserCannotChangePassword { get; set; }
        [DataType(DataType.PhoneNumber)]
        [Phone]
        public string? VoiceTelephoneNumber { get; set; }

        public PasswordSet PasswordSet { get; } = new PasswordSet();

        public override void Populate()
        {
            UserPrincipalName = Surname + GivenName;
            Name = UserPrincipalName;
            DisplayName = $"{GivenName} {MiddleName} {Surname}";
            SamAccountName = RemoveSpecialChars(UserPrincipalName,20);
            Department = "dept";
          
        }

       
            public string RemoveSpecialChars(string input,int maxLength)
            {
                var temp = input?.Substring(0, Math.Min(input.Length, maxLength)).ToLowerInvariant().Trim();
                return Regex.Replace(temp?.Substring(0, Math.Min(temp.Length, maxLength)) ?? string.Empty, @"[^0-9a-zA-Z\._]", string.Empty);

            }
           
        
    }



}

