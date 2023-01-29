using Microsoft.Extensions.Logging;
using PowerStationDomain.models;
using System.DirectoryServices.AccountManagement;
using System.Reflection.PortableExecutable;

namespace PowerStationInfrastructure.Services;

public class AdServiceCodeUser:IAdService<User>
{
    private Configuration _configuration;
    private readonly ILogger<AdServiceCodeUser> _logger;

    public AdServiceCodeUser(Configuration configuration, ILogger<AdServiceCodeUser> logger)
    {
        this._configuration = configuration;
        _logger = logger;
    }

    public void Add(User user)
    {       
        try
        {
            UserPrincipal? up = new(_configuration.OuContex)
            {
                SamAccountName = user.SamAccountName,
                Description= user.Description,                             
                DisplayName=user.UserPrincipalName,
                Enabled= user.Enabled,                
                GivenName= user.GivenName,
                Surname= user.GivenName,
                Name= user.Name,
                UserPrincipalName= user.UserPrincipalName,
                EmailAddress= user.EmailAddress,
                VoiceTelephoneNumber=user.OfficePhone
            };
           
            up.Save();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
        }
    }

    public void Remove(User adUser)
    {
        throw new NotImplementedException();
    }

    public List<User> All()
    {
        throw new NotImplementedException();
    }

    public User? GetUser(string username)
    {
     
        UserPrincipal? user = UserPrincipal.FindByIdentity(_configuration.OuContex, username);
        if (user != null)
        {
            var user1 = new User(user.Description, user.DistinguishedName, user.EmailAddress, user.EmployeeId,
                (bool)user.Enabled, user.GivenName, user.GivenName, "", user.MiddleName, user.Name, "", "", "",
                user.SamAccountName, "", user.UserPrincipalName, AdUserType.Basic)
            {
                Description = user.Description,
                DistinguishedName = user.DistinguishedName,
                EmailAddress = user.EmailAddress,
                EmployeeId = user.EmployeeId,
                Enabled = (bool)user.Enabled,
                FirstName = user.Surname,
                LastName = user.Name,
                Name = user.Name,
                GivenName = user.GivenName,
                SamAccountName = user.SamAccountName
            };

            return user1;
        }
        return null;
    }
}