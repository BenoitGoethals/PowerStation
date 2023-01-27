using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerStationDomain.Domain
{
    public class User:AdObject
    {
        public User(string sid, string objectClass, Guid objectGuid, string name, string userPrincipalName, string givenName, bool enabled, string samAccountName, int distinguishedName) : base(sid, objectClass, objectGuid)
        {
            Name = name;
            UserPrincipalName = userPrincipalName;
            GivenName = givenName;
            Enabled = enabled;
            SamAccountName = samAccountName;
            DistinguishedName = distinguishedName;
        }

        public string Name { get; set; }
        public string UserPrincipalName { get; set; }

        public string GivenName { get; set; }

        public bool Enabled { get; set; }
        public string SamAccountName { get; set; }

        public int DistinguishedName { get; set; }


        protected bool Equals(User other)
        {
            return Sid == other.Sid;
        }
    


        //        Surname           : David
        //Name              : Chew David
        //UserPrincipalName :
        //GivenName         : David
        //Enabled           : False
        //SamAccountName    : ChewDavid
        //ObjectClass       : user
        //SID               : S-1-5-21-2889043008-4136710315-2444824263-3544
        //ObjectGUID        : e1418d64-096c-4cb0-b903-ebb66562d99d
        //DistinguishedName : CN=Chew David, OU = NorthAmerica, OU = Sales, OU = UserAccounts, DC = FABRIKAM, DC = COM
    }
}
