using System.DirectoryServices.AccountManagement;
using System.DirectoryServices;
using System.DirectoryServices.ActiveDirectory;
using System.Security.Policy;
using AdApiService;
using System.Text;
using System.Xml.Linq;
using System.Collections;
using System.Drawing;

namespace PowerStationInfrastructure.helpers;

public static class AdHelper
{
    public static bool DomainExistsNoForests(string domain, string server)
    {
        try
        {
            var directoryContext = new DirectoryContext(DirectoryContextType.DirectoryServer, server);
            var d = Domain.GetDomain(directoryContext);
            return d.Name.Trim().Equals(domain.Trim(), StringComparison.CurrentCultureIgnoreCase);
        }
        catch (Exception e)
        {
            return false;
        }
    }
    public static bool DomainExist(string domain)
    {
        try
        {
            HashSet<string> domains = new HashSet<string>();
            foreach (Domain d in Forest.GetCurrentForest().Domains)
            {
                domains.Add(d.Name.ToLower());
            }

            return domains.Contains(domain.ToLower());
        }
        catch (Exception e)
        {
            return false;
        }
    

    }



   

   public static void InsertPicture(string userName, byte[]? data)
   {
       using DirectorySearcher dsSearcher = new DirectorySearcher();
       dsSearcher.Filter = "(&(objectClass=user) (cn=" + userName + "))";
       SearchResult result = dsSearcher.FindOne();

       using DirectoryEntry user = new DirectoryEntry(result.Path);
       user.Properties["jpegPhoto"].Clear();
       user.Properties["jpegPhoto"].Add(data);
       user.CommitChanges();
   }

    public static Image GetUserPicture(string userName)
    {
        using DirectorySearcher dsSearcher = new DirectorySearcher();
        dsSearcher.Filter = "(&(objectClass=user) (cn=" + userName + "))";
        SearchResult? result = dsSearcher.FindOne();

        using DirectoryEntry user = new DirectoryEntry(result.Path);
        byte[] data = user.Properties["jpegPhoto"].Value as byte[];

        if (data == null) return null;
        using MemoryStream s = new MemoryStream(data);
        return Bitmap.FromStream(s);

    }


    /// <summary>
    /// This method returns a list of attributes that have values of the specified object identified by the name parameter.
    /// </summary>
    /// <param name="name">The unique identifier of the Principal whose attributes should be returned.</param>
    /// <returns>A dictionary of string keys and values where the keys are the names of the attributes and the
    /// values are, of course, the values of the attributes.</returns>
    public static SortedDictionary<string, string?> GetUsedAttributes(string name)
    {
        Principal p = Principal.FindByIdentity(GetPrincipalContext(), name);
        String dn = p.DistinguishedName;
        DirectoryEntry objRootDSE = new DirectoryEntry("LDAP://" + dn);
        SortedDictionary<string, string?> props = new SortedDictionary<string, string?>();
        foreach (PropertyValueCollection prop in objRootDSE.Properties)
        {
            string? value = String.Empty;
            if (prop.Value is Object[])
            {
                foreach (var o in prop.Value as Object[])
                {
                    value += o.ToString() + " ";
                }
            }
            else if (prop.Value is String)
            {
                value = prop.Value.ToString();
            }
            else
            {
                value = prop.Value?.ToString();
            }
            props.Add(prop.PropertyName, value);
        }
        return props;
    }
    /// <summary>
    /// Returns a PrincipalCollection that is the members of the specified groups.
    /// </summary>
    /// <param name="group">The unique identifier of the group.</param>
    /// <returns>A PrincipalCollection.</returns>
    public static PrincipalCollection GetMembers(string group)
    {
        GroupPrincipal g = GroupPrincipal.FindByIdentity(GetPrincipalContext(), group);
        return g.Members;
    }

    /// <summary>
    /// Gets the base principal context
    /// </summary>
    /// <returns>Retruns the PrincipalContext object</returns>
    private static PrincipalContext GetPrincipalContext()
    {
        return new PrincipalContext(ContextType.Domain, GetCurrentDomain());
    }

    /// <summary>
    /// This method returns a PrincipalContext targeted to the specified OU.
    /// </summary>
    /// <param name="location">The distinguished name of the container this context will be attached to.</param>
    /// <returns>A PrincipalContext object.</returns>
    private static PrincipalContext GetPrincipalContext(string location)
    {
        return new PrincipalContext(ContextType.Domain, GetCurrentDomain(), location);
    }

    /// <summary>
    /// This method will return the value of the specified property if it exists in the specified Principal's
    /// underlying DirectoryEntry object.
    /// </summary>
    /// <param name="principal">The unique identifier of a Principal.</param>
    /// <param name="property">The name of the Property whose value should be returned.</param>
    /// <returns></returns>
    public static String GetProperty(Principal principal, string property)
    {
        DirectoryEntry? de = principal.GetUnderlyingObject() as DirectoryEntry;
        // property is case senstive, use PropertyLookup
        string targetProperty = PropertyLookup.Instance.get(property);
        if (de.Properties.Contains(targetProperty))
            return de.Properties[targetProperty].Value.ToString();
        else
            return String.Empty;
    }

    /// <summary>
    /// Overload for GetProperty(Principal, String)
    /// </summary>
    /// <param name="principal"></param>
    /// <param name="property"></param>
    /// <returns></returns>
    public static String GetProperty(string principal, string property)
    {
        Principal p = Principal.FindByIdentity(GetPrincipalContext(), principal);
        return GetProperty(p, property);
    }

    /// <summary>
    /// Returns a 12 character password that has at least 2 special characters.
    /// </summary>
    /// <returns>
    /// A string that represents a random password 12 characters long that has at least
    /// 2 special characters.
    /// </returns>
    public static string? GetRandomPassword(int size=12)
    {
        return RandomNumberGenerator.RandomPassword(size);
    }

    private static class RandomNumberGenerator
    {
        // Generate a random number between two numbers    
        private static int RandomNumber(int min, int max)
        {
            Random random = new Random();
            return random.Next(min, max);
        }

        // Generate a random string with a given size and case.   
        // If second parameter is true, the return string is lowercase  
        private static string RandomString(int size, bool lowerCase)
        {
            StringBuilder builder = new StringBuilder();
            Random random = new Random();
            char ch;
            for (int i = 0; i < size; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(ch);
            }
            if (lowerCase)
                return builder.ToString().ToLower();
            return builder.ToString();
        }

        // Generate a random password of a given length (optional)  
        public static string? RandomPassword(int size = 12)
        {
            if (size < 12) return null;
            StringBuilder builder = new StringBuilder();
            builder.Append(RandomString(size-8, true));
            builder.Append(RandomNumber(1000, 9999));
            builder.Append(RandomString(4, false));
            return builder.ToString();
        }
    }
    /// <summary>
    /// This method returns the full domain name as the top level ldap path. For example, if 
    /// the domain is blah.meh.com this method will return DC=blah,DC=meh,DC=com.
    /// </summary>
    /// <returns>A string representing the top level LDAP container of the current domain.</returns>
    public static string GetRootContainer()
    {
        string domain = GetCurrentDomain();
        string rootCn = String.Empty;
        string[] domainParts = domain.Split('.');
        foreach (string part in domainParts)
        {
            rootCn += string.Format("DC={0},", part);
        }
        return rootCn.TrimEnd(new char[] { ',' });
    }

    /// <summary>
    /// This method returns the SamAccountName associated with the principal identified by the specified name.
    /// </summary>
    /// <param name="name">The unique identifier of the Principal.</param>
    /// <returns>A string that is the SamAccountName of the Principal identified by the specified name.</returns>
    public static string GetSamAccountName(string name)
    {
        if (!string.IsNullOrEmpty(name))
        {
            Principal p = Principal.FindByIdentity(GetPrincipalContext(), name);
            if (p != null)
            {
                return p.SamAccountName;
            }
        }
        return String.Empty;
    }

  

    /// <summary>
    /// Sets the specified property to the specified value on the Principal specified by name.
    /// </summary>
    /// <param name="name">The unique identifier of the Principal.</param>
    /// <param name="property">The name of the property.</param>
    /// <param name="value">The value to assign to the property.</param>
    /// <returns>True if success, false otherwise.</returns>
    public static Boolean SetProperty(string name, string property, string value)
    {
        Principal p = Principal.FindByIdentity(GetPrincipalContext(), name);
        return SetProperty(p, property, value);
    }

    /// <summary>
    /// Sets the specified property to the specified value on the specified Principal.
    /// </summary>
    /// <param name="principal">The Principal object.</param>
    /// <param name="property">The name of the property.</param>
    /// <param name="value">The value to assign to the property.</param>
    /// <returns>True if success, false otherwise.</returns>
    public static Boolean SetProperty(Principal principal, string property, string value)
    {
        DirectoryEntry? de = principal.GetUnderlyingObject() as DirectoryEntry;
        // Use PropertyLookup to get the property name in the correct format.
        string targetProperty = PropertyLookup.Instance.get(property);

        // if the property is Manager the value should be a distinguishedName but those
        // are hard to pass around in URLs if they have errant commas and escaping backslashes.
        // Instead get the dn from the value which should be a samaccountname.
        if (targetProperty == PropertyLookup.Instance.get("manager"))
        {
            value = GetDistinguishedName(value);
        }
        if (de.Properties.Contains(targetProperty))
        {
            try
            {
                de.Properties[targetProperty][0] = value;
                de.CommitChanges();
            }
            catch
            {
                return false;
            }
        }
        else
        {
            try
            {
                de.Properties[targetProperty].Add(value);
                de.CommitChanges();
            }
            catch
            {
                return false;
            }
        }
        return true;
    }

    /// <summary>
    /// Unlocks the specified principal, which can be either a user or computer.
    /// </summary>
    /// <param name="principal">A unique identifier of a user or computer.</param>
    /// <returns>True if success, false otherwise.</returns>
    public static Boolean Unlock(string principal)
    {
        Principal p = Principal.FindByIdentity(GetPrincipalContext(), principal);
        try
        {
            if (p is UserPrincipal)
            {
                UserPrincipal? u = p as UserPrincipal;
                u.UnlockAccount();
                u.Save();
            }
            else if (p is ComputerPrincipal)
            {
                ComputerPrincipal c = p as ComputerPrincipal;
                c.UnlockAccount();
                c.Save();
            }
            return true;
        }
        catch
        {
            return false;
        }
    }
    
    /// <summary>
    /// Validates the username and password of a given user
    /// </summary>
    /// <param name="username">The username to validate</param>
    /// <param name="password">The password of the username to validate</param>
    /// <returns>Returns True of user is valid</returns>
    public static bool ValidateCredentials(string username, string password)
    {
        return GetPrincipalContext().ValidateCredentials(username, password);
    }
    /// <summary>
    /// This method will move an item to the specified OU identified by its distinguishedName.
    /// </summary>
    /// <param name="name">The unique identifier of the Principal to move.</param>
    /// <param name="location">The path, i.e., distinguished name of the OU to move the Principal to.</param>
    /// <returns>A Boolean. True if the object was moved, false otherwise.</returns>
    public static Boolean Move(string name, string location)
    {
        Boolean moved = false;

        if (Exists(name) && Exists(location))
        {
            DirectoryEntry source = Principal.FindByIdentity(GetPrincipalContext(), name).GetUnderlyingObject() as DirectoryEntry;
            DirectoryEntry target = new DirectoryEntry(("LDAP://" + location));

            source.MoveTo(target);
            moved = true;
        }

        return moved;
    }

    public static List<string> EnumerateOu(string OuDn)
    {
        List<string> alObjects = new List<string>();
        try
        {
            DirectoryEntry directoryObject = new DirectoryEntry("LDAP://" + OuDn);
            foreach (DirectoryEntry child in directoryObject.Children)
            {
                string childPath = child.Path.ToString();
                alObjects.Add(childPath.Remove(0, 7));
                //remove the LDAP prefix from the path

                child.Close();
                child.Dispose();
            }
            directoryObject.Close();
            directoryObject.Dispose();
        }
        catch (DirectoryServicesCOMException e)
        {
            Console.WriteLine("An Error Occurred: " + e.Message.ToString());
        }
        return alObjects;
    }

    /// <summary>
    /// Tests if the specified item exists. The item is either a user, computer, group,
    /// or OU.
    /// </summary>
    /// <param name="item">The unique identifier of the item.</param>
    /// <returns>True if the item exists, false otherwise.</returns>
    public static bool Exists(string item)
    {
        try
        {
            return (Principal.FindByIdentity(GetPrincipalContext(), item) != null) || (DirectoryEntry.Exists("LDAP://" + item));
        }
        catch
        {
            return false;
        }
    }

    /// <summary>
    /// Removes the specified member from the specified group.
    /// </summary>
    /// <param name="member">The member to remove.</param>
    /// <param name="group">The group from which the member should be removed.</param>
    /// <returns>True if the member was removed, false otherwise.</returns>
    public static Boolean RemoveMember(string member, string group)
    {
        Boolean done = false;

        if (IsMember(member, group))
        {
            GroupPrincipal g = GroupPrincipal.FindByIdentity(GetPrincipalContext(), group);
            Principal p = Principal.FindByIdentity(GetPrincipalContext(), member);
            g.Members.Remove(p);
            g.Save();
            done = !IsMember(member, group);
        }
        return done;
    }

    /// <summary>
    /// This method returns true if the specified principal is a member of the specified group.
    /// </summary>
    /// <param name="principal">The unique identifier for the principal.</param>
    /// <param name="group">The unique identifier for the group.</param>
    /// <returns>True if the Principal is a member of the group, false otherwise.</returns>
    public static bool IsMember(string principal, string group)
    {
        GroupPrincipal g = GroupPrincipal.FindByIdentity(GetPrincipalContext(), group);
        Principal u = Principal.FindByIdentity(GetPrincipalContext(), principal);
        if (u != null && g != null)
        {
            // loop through the members of the group and compare samaccountnames to the user's.
            // have to do it this way rather than using UserPrincipal.IsMemberOf or GroupPrincipal.
            // Members.Contains because those have a known issue of returning false for Domain Users
            // and Domain Computers. Thanks, Microsoft.
            foreach (Principal p in g.Members)
            {
                if (p.SamAccountName == u.SamAccountName)
                {
                    return true;
                }
            }
        }
        return false;
    }

    /// <summary>
    /// Sets the password of the specified user to the specified password.
    /// </summary>
    /// <param name="name">The unique identifier of the user.</param>
    /// <param name="password">The new password for the user.</param>
    /// <returns>True if success, false otherwise.</returns>
    public static Boolean SetPassword(string name, string password)
    {
        try
        {
            UserPrincipal u = UserPrincipal.FindByIdentity(GetPrincipalContext(), name);
            u.SetPassword(password);
            return true;
        }
        catch
        {
            return false;
        }
    }

    /// <summary>
    /// Returns the distinguished name associated with the Principal identified by the specified name.
    /// </summary>
    /// <param name="name">The unique identifier of the Principal.</param>
    /// <returns>A string that is the distinguished name of the specified Principal.</returns>
    public static string GetDistinguishedName(string name)
    {
        Principal p = Principal.FindByIdentity(GetPrincipalContext(), name);
        if (p != null)
        {
            return p.DistinguishedName;
        }
        return string.Empty;
    }
    /// <summary>
    /// This method returns the domain of the current context, i.e., the domain of the user that is running the code.
    /// </summary>
    /// <returns>A string that is the current domain name.</returns>
    public static string GetCurrentDomain()
    {
        return Domain.GetCurrentDomain().Name;

    }

    /// <summary>
    /// This method returns a list of Domain Controllers.
    /// </summary>
    /// <returns>A list of strings that are the Domain Controllers in the current Domain.</returns>
    public static List<string> GetDomainControllers()
    {
        List<string> results = new List<string>();
        Domain domain = Domain.GetCurrentDomain();
        foreach (DomainController dc in domain.DomainControllers)
        {
            results.Add(dc.Name);
        }
        return results;
    }

    /// <summary>
    /// This method returns a list of all Domains in the current Forest.
    /// </summary>
    /// <returns>A list of strings that are the names of the Domains in the current Forest.</returns>
    public static List<string> GetForestDomains()
    {
        List<string> alDomains = new List<string>();
        Forest currentForest = Forest.GetCurrentForest();
        DomainCollection myDomains = currentForest.Domains;

        foreach (Domain objDomain in myDomains)
        {
            alDomains.Add(objDomain.Name);
        }
        return alDomains;
    }

    /// <summary>
    /// This method returns the full domain name based on the friendly name. For example, if contoso is passed in
    /// contoso.com will be returned.
    /// </summary>
    /// <param name="friendlyDomainName">The friendly, or short domain name of the current domain.</param>
    /// <returns>A string that is the full domain name of the current domain.</returns>
    public static string GetFullDomainName(string friendlyDomainName)
    {
        string ldapPath = null;

        DirectoryContext objContext = new DirectoryContext(
            DirectoryContextType.Domain, friendlyDomainName);
        Domain objDomain = Domain.GetDomain(objContext);
        ldapPath = objDomain.Name;

        return ldapPath;
    }

    /// <summary>
    /// This method returns a list of Global Catalog servers in the current domain.
    /// </summary>
    /// <returns>A list of strings that are the names of the Global Catalog servers in the current domain.</returns>
    public static List<string> GetGlobalCatalogServers()
    {
        List<string> results = new List<string>();
        Forest currentForest = Forest.GetCurrentForest();
        foreach (GlobalCatalog gc in currentForest.GlobalCatalogs)
        {
            results.Add(gc.Name);
        }
        return results;
    }

    public static bool IsLocked(Principal principal)
    {
        bool locked = false;
        
        if (principal is UserPrincipal userPrincipal)
        {
            locked = userPrincipal.IsAccountLockedOut();
        }
        else if (principal is ComputerPrincipal computerPrincipal
                 
                 )
        {
            locked = computerPrincipal.IsAccountLockedOut();
        }
        return locked;
    }
}