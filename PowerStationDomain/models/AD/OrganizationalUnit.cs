using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace PowerStationDomain.models.AD
{
    public class OrganizationalUnit
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public Guid objectGUID { get; set; }

        public OrganizationalUnit parent { get; set; }

        public List<OrganizationalUnit> Organizations = new List<OrganizationalUnit>();

    }
}
