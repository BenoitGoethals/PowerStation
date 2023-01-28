using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using PowerStationApplicationCore.Interfaces;
using PowerStationApplicationCore.Validators;
using PowerStationDomain.Domain;
using PowerStationInfrastructure.Services;

namespace PowerStationApplicationCore.Services
{
    public class UserService:IUserService
    {
        private readonly ILogger<AdService> _logger;
        private readonly IAdService _adService;
        private readonly UserValidator _userValidator = new();
        public UserService(IAdService adService, ILogger<AdService> logger)
        {
            _adService = adService;
            _logger = logger;
        }

        public void AddAdUser(User adUser)
        {
            var val=_userValidator.Validate(adUser);
            if (val.IsValid)
            {
                _adService.Add(adUser);
            }
            else
            {

                foreach (var failure in val.Errors)
                {
                    _logger.LogError(failure.ErrorMessage);
                }
            }
           
        }

        public void Remove(User adUser)
        {
            _adService.Remove(adUser);
        }

        public List<User> AllUsers()
        {
            return _adService.AllUsers();
        }
    }
}
