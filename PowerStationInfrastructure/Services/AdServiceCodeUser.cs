using Microsoft.Extensions.Logging;
using PowerStationDomain.models;
using System.ComponentModel;
using System.DirectoryServices.AccountManagement;
using System.DirectoryServices.ActiveDirectory;
using System.Reflection.PortableExecutable;

namespace PowerStationInfrastructure.Services;

public class AdServiceCodeUser:IAdService<User>
{
    private readonly Configuration _configuration;
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
       // const string groupName = "Domain Users";

        var usersRet = new List<User>();


       // using var principalContext = new PrincipalContext(ContextType.Domain, name: _configuration.AdSetting.DomainName, _configuration.OuContex.Container);
        //using (var group = GroupPrincipal.FindByIdentity(context, windowsGroup.TrimEnd('*')))
        using var searcher = new PrincipalSearcher(new UserPrincipal(new PrincipalContext(ContextType.Domain, Environment.UserDomainName)));
        List<UserPrincipal> users = searcher.FindAll().Select(u => (UserPrincipal)u).ToList();
        foreach (var principal in users)
        {
            var user = (UserPrincipal)principal;

            var newUser = new User(user.Description, user.DistinguishedName, null, null,
                user.Enabled.GetValueOrDefault(), user.GivenName, user.GivenName, "", user.MiddleName, user.Name, user.VoiceTelephoneNumber, user.DistinguishedName, "",
                user.SamAccountName,"", user.UserPrincipalName, AdUserType.Basic);
            usersRet.Add(newUser);
        }

        return usersRet;
    }

    public User? GetUser(string username)
    {
       
        using var context = new PrincipalContext(ContextType.Domain, Environment.UserDomainName);
        {
            var principal = new UserPrincipal(context)
            {
                SamAccountName = username
            };
            using var searcher = new PrincipalSearcher(principal);
            PrincipalSearchResult<Principal> result = searcher.FindAll();
            UserPrincipal? user = (UserPrincipal)result.FirstOrDefault();


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
        }
        return null;
    }


    public void Dispose()
    {
    }
}