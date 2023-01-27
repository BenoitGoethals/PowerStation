using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Microsoft.Extensions.Logging;
using PowerStationDomain.Domain;

namespace PowerStationInfrastructure.Services
{
    public class AdService: IAdService
    {
        public void Add(User user)
        {
            throw new NotImplementedException();
        }

        public void Remove(User adUser)
        {
            throw new NotImplementedException();
        }

        public List<User> AllUsers()
        {
            throw new NotImplementedException();
        }
    }

    public interface IAdService
    {
        void Add(User user);
        void Remove(User adUser);
        List<User> AllUsers();
    }
}
