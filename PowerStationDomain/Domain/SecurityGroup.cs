using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerStationDomain.Domain
{
    public class SecurityGroup:AdObject,ISecurityGroup
    {
      
        public string? GroupName { get; set; }
        public GroupScope Scope { get; set; }

        private List<AdObject> _adObjects =new List<AdObject>();

        public SecurityGroup(string sid, string objectClass, Guid objectGuid) : base(sid, objectClass, objectGuid)
        {
        }


        public void Add(AdObject adObject)
        {
            throw new NotImplementedException();
        }

        public void Remove(AdObject adObject)
        {
            throw new NotImplementedException();
        }

        public List<AdObject> AllAdObjects()
        {
            throw new NotImplementedException();
        }

        public List<AdObject> AllAdObjects(Predicate<AdObject> predicate)
        {
            throw new NotImplementedException();
        }
    }
}
