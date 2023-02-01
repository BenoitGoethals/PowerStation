using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace PowerStationDomain.models
{
    public class User : AdObject
    {

        public AdUserType UserType { get; set; }
        public string? AccountExpirationData { get; set; }
        public string? AccountLockoutTime { get; set; }
        public bool? AllowReversiblePasswordEncryption { get; set; }
        public int? BadLogonCount { get; set; }
        public string? Company { get; set; }
        public Boolean? DelegationPermitted { get; set; }
        public string? Department { get; set; }
      
        public string? EmailAddress { get; set; }
        public string? EmployeeId { get; set; }
        public Boolean? Enabled { get; set; }
        public string? GivenName { get; set; }
       
        public string? HomeDirectory { get; set; }
        public string? HomeDrive { get; set; }
        public string? LastBadPasswordAttempt { get; set; }
        public string? LastLogon { get; set; }
        public string? LastPasswordSet { get; set; }
        public string? Manager { get; set; }
        public List<string>? MemberOf { get; set; }
        public string? MiddleName { get; set; }
        public string? Mobile { get; set; }
    
        public Boolean? PasswordNeverExpires { get; set; }
        public Boolean? PasswordNotRequired { get; set; }
        public string? ProfilePath { get; set; }
     
        public string? ScriptPath { get; set; }
      
        public Boolean? SmartCardLogonRequired { get; set; }
        public string? Surname { get; set; }
        public Boolean? UserCannotChangePassword { get; set; }
      
        public string? VoiceTelephoneNumber { get; set; }

      
    }



}

