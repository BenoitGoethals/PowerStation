using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerStationDomain.Domain
{
    public class Administrator:User
    {
        public Administrator(string sid, string objectClass, Guid objectGuid, string name, string userPrincipalName, string givenName, bool enabled, string samAccountName, int distinguishedName) : base(sid, objectClass, objectGuid, name, userPrincipalName, givenName, enabled, samAccountName, distinguishedName)
        {
        }
    }
}
