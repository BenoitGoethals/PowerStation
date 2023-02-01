using System.Runtime.Serialization;

namespace PowerStationDomain.models;

public class Computer:AdObject
{
   
    public string? AccountExpirationDate { get; set; }
    public string? AccountLockouttime { get; set; }
    public int? BadLogonCount { get; set; }
    public Boolean? DelegationPermitted { get; set; }
   
    public Boolean? Enabled { get; set; }
  
    public string? HomeDirectory { get; set; }
    public string? HomeDrive { get; set; }
    public string? LastBadPasswordAttempt { get; set; }
    public string? Name { get; set; }
    public Boolean? PasswordNeverExpires { get; set; }
    public Boolean? PasswordNotRequired { get; set; }
   
    public string? ScriptPath { get; set; }
    public List<string>? ServicePrincipalNames { get; set; }
    public string? Sid { get; set; }
    public Boolean? SmartcardLogonRequired { get; set; }
    public Boolean? UserCannotChangePassword { get; set; }
  


}