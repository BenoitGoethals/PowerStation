using Microsoft.Extensions.Logging;
using PowerStationDomain.models;
using System.ComponentModel;
using System.DirectoryServices.AccountManagement;
using System.DirectoryServices.ActiveDirectory;
using System.Reflection.PortableExecutable;
using AutoMapper;
using System.Xml.Linq;

namespace PowerStationInfrastructure.Services;

public class AdServiceCodeUser : IAdService<User>
{
    private readonly Configuration _configuration;
    private readonly ILogger<AdServiceCodeUser> _logger;
    private readonly IMapper _mapper;

    public AdServiceCodeUser(Configuration configuration, ILogger<AdServiceCodeUser> logger, IMapper mapper)
    {
        this._configuration = configuration;
        _logger = logger;
        _mapper = mapper;
    }

    public bool Add(User user)
    {
        try
        {
            var userPrincePrincipal = _mapper.Map<UserPrincipal>(user);
          
            userPrincePrincipal.Save();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            return false;
        }

        return true;
    }

    public bool Remove(User adUser)
    {
        using var context = _configuration.OuContex;
        {
            var principal = new UserPrincipal(context)
            {
                SamAccountName = adUser.SamAccountName
            };
            using var searcher = new PrincipalSearcher(principal);
            PrincipalSearchResult<Principal> result = searcher.FindAll();
            UserPrincipal? user = (UserPrincipal)result.First();
            if (adUser.SamAccountName != null)
            {
                UserPrincipal? u = UserPrincipal.FindByIdentity(context, adUser.SamAccountName);
                u?.Delete();
              
            }

            return true;

        }
        return false;
    }

    public List<User> All()
    {
        using var searcher = new PrincipalSearcher(new UserPrincipal(_configuration.OuContex));
        List<UserPrincipal> users = searcher.FindAll().Select(u => (UserPrincipal)u).ToList();
        return users.Select(principal => _mapper.Map<User>(principal)).ToList();
    }

    public User? Get(string username)
    {
        using var context = _configuration.OuContex;
        {
            var principal = new UserPrincipal(context)
            {
                SamAccountName = username
            };
            using var searcher = new PrincipalSearcher(principal);
            PrincipalSearchResult<Principal> result = searcher.FindAll();
            UserPrincipal? user = (UserPrincipal)result.First();
            var newUser = _mapper.Map<User>(principal);
            return newUser;

        }
        return null;
    }


    public void Dispose()
    {
    }
}