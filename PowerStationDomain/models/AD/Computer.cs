using System.Runtime.Serialization;

namespace PowerStationDomain.models.AD;

public class Computer : AdObject
{

    public string? AccountExpirationDate { get; set; }
    public string? AccountLockouttime { get; set; }
    public int? BadLogonCount { get; set; }
    public bool? DelegationPermitted { get; set; }

    public bool? Enabled { get; set; }

    public string? HomeDirectory { get; set; }
    public string? HomeDrive { get; set; }
    public string? LastBadPasswordAttempt { get; set; }
    public string? Name { get; set; }
    public bool? PasswordNeverExpires { get; set; }
    public bool? PasswordNotRequired { get; set; }

    public string? ScriptPath { get; set; }
    public List<string>? ServicePrincipalNames { get; set; }
    public string? Sid { get; set; }
    public bool? SmartcardLogonRequired { get; set; }
    public bool? UserCannotChangePassword { get; set; }



}