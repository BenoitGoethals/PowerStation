using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerStationDomain.models
{
    public class User : AdObject
    {
        private User(string name, string userPrincipalName, string givenName, bool enabled, string samAccountName,
            string distinguishedName, AdUserType userType, string password)
        {
            Name = name;
            UserPrincipalName = userPrincipalName;
            GivenName = givenName;
            Enabled = enabled;
            SamAccountName = samAccountName;
            DistinguishedName = distinguishedName;
            UserType = userType;
            Password = password;
        }

        public static User CreateInstance(string name, string userPrincipalName, string givenName, bool enabled, string samAccountName, string distinguishedName, AdUserType userType, string password)
        {
            return new User(name, userPrincipalName, givenName, enabled, samAccountName, distinguishedName, userType, password);
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
        public string Password { get; set; }

        public User(string? description, string distinguishedName, string? emailAddress, string? employeeId,
            bool enabled, string? firstName, string givenName, string? jobTitle, string? lastName, string name,
            string? officePhone, string? organizationalUnit, string password, string samAccountName, string? type,
            string userPrincipalName, AdUserType userType)
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

    }



}

