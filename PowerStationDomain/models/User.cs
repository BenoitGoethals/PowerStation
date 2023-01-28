using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerStationDomain.models
{
    public class User : AdObject
    {
        public User(string sid, string objectClass, Guid objectGuid, string name, string userPrincipalName, string givenName, bool enabled, string samAccountName, string distinguishedName, AdUserType userType)
        {
            Name = name;
            UserPrincipalName = userPrincipalName;
            GivenName = givenName;
            Enabled = enabled;
            SamAccountName = samAccountName;
            DistinguishedName = distinguishedName;
            UserType = userType;
        }

        public AdUserType UserType { get; set; }
        public string Name { get; set; }
        public string UserPrincipalName { get; set; }
        public string GivenName { get; set; }
        public bool Enabled { get; set; }
        public string SamAccountName { get; set; }
        public string DistinguishedName { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? JobTitle { get; set; }
        public string? OfficePhone { get; set; }
        public string? EmployeeId { get; set; }
        public string? EmailAddress { get; set; }
        public string? Description { get; set; }
        public string? Type { get; set; }
        public string? OrganizationalUnit { get; set; }
        public string? Password { get; set; }

        public User(string? description, string distinguishedName, string? emailAddress, string? employeeId, bool enabled, string? firstName, string givenName, string? jobTitle, string? lastName, string name, string? officePhone, string? organizationalUnit, string? password, string samAccountName, string? type, string userPrincipalName, AdUserType userType)
        {
            Description = description;
            DistinguishedName = distinguishedName;
            EmailAddress = emailAddress;
            EmployeeId = employeeId;
            Enabled = enabled;
            FirstName = firstName;
            GivenName = givenName;
            JobTitle = jobTitle;
            LastName = lastName;
            Name = name;
            OfficePhone = officePhone;
            OrganizationalUnit = organizationalUnit;
            Password = password;
            SamAccountName = samAccountName;
            Type = type;
            UserPrincipalName = userPrincipalName;
            UserType = userType;
        }

        public override string ToString()
        {
            return $"{nameof(UserType)}: {UserType}, {nameof(Name)}: {Name}, {nameof(UserPrincipalName)}: {UserPrincipalName}, {nameof(GivenName)}: {GivenName}, {nameof(Enabled)}: {Enabled}, {nameof(SamAccountName)}: {SamAccountName}, {nameof(DistinguishedName)}: {DistinguishedName}, {nameof(FirstName)}: {FirstName}, {nameof(LastName)}: {LastName}, {nameof(JobTitle)}: {JobTitle}, {nameof(OfficePhone)}: {OfficePhone}, {nameof(EmployeeId)}: {EmployeeId}, {nameof(EmailAddress)}: {EmailAddress}, {nameof(Description)}: {Description}, {nameof(Type)}: {Type}, {nameof(OrganizationalUnit)}: {OrganizationalUnit}, {nameof(Password)}: {Password}";
        }
    }
}
