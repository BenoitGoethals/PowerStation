using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Microsoft.Extensions.Logging;
using PowerStationDomain.models;

namespace PowerStationInfrastructure.Services
{
    public class AdServicePowerShellUser: IAdService<User>
    {
        public bool Add(User user)
        {
            throw new NotImplementedException();
        }

        public bool Remove(User adUser)
        {
            throw new NotImplementedException();
        }

        public List<User> All()
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public User? Get(string username)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
        }
    }
}
