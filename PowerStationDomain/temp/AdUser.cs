using System.DirectoryServices.AccountManagement;
using System.Runtime.Serialization;

namespace PowerStationDomain.temp
{

    [DataContract]
    public class AdUser
    {

        #region constructors

        /// <summary>
        /// Default constructor that initializes attributes with empty values.
        /// </summary>
        public AdUser()
        {
            AccountExpirationData = string.Empty;
            AccountLockoutTime = string.Empty;
            AllowReversiblePasswordEncryption = false;
            BadLogonCount = 0;
            Company = string.Empty;
            DelegationPermitted = false;
            Department = string.Empty;
            Description = string.Empty;
            DisplayName = string.Empty;
            DistinguishedName = string.Empty;
            EmailAddress = string.Empty;
            EmployeeId = string.Empty;
            Enabled = false;
            GivenName = string.Empty;
            Guid = string.Empty;
            HomeDirectory = string.Empty;
            HomeDrive = string.Empty;
            LastBadPasswordAttempt = string.Empty;
            LastLogon = string.Empty;
            LastPasswordSet = string.Empty;
            Manager = string.Empty;
            MemberOf = null;
            MiddleName = string.Empty;
            Mobile = string.Empty;
            Name = string.Empty;
            PasswordNeverExpires = false;
            PasswordNotRequired = false;
            ProfilePath = string.Empty;
            SamAccountName = string.Empty;
            ScriptPath = string.Empty;
            Sid = string.Empty;
            SmartCardLogonRequired = false;
            Surname = string.Empty;
            UserCannotChangePassword = false;
            UserPrincipalName = string.Empty;
            VoiceTelephoneNumber = string.Empty;
        }

        /// <summary>
        /// Constructor that instantiates an AdUser object with the values of the specified
        /// UserPrincipal's attributes.
        /// </summary>
        /// <param name="user"></param>
        public AdUser(UserPrincipal user)
        {
            AccountExpirationData = user.AccountExpirationDate.ToString();
            AccountLockoutTime = user.AccountLockoutTime.ToString();
            AllowReversiblePasswordEncryption = user.AllowReversiblePasswordEncryption;
            BadLogonCount = user.BadLogonCount;
            //Company = GetCompany(user);
            //DelegationPermitted = user.DelegationPermitted;
            //Department = GetDepartment(user);
            Description = user.Description;
            DisplayName = user.DisplayName;
            DistinguishedName = user.DistinguishedName;
            EmailAddress = user.EmailAddress;
            EmployeeId = user.EmployeeId;
            Enabled = user.Enabled;
            GivenName = user.GivenName;
            Guid = user.Guid.ToString();
            HomeDirectory = user.HomeDirectory;
            HomeDrive = user.HomeDrive;
            LastBadPasswordAttempt = user.LastBadPasswordAttempt.ToString();
            LastLogon = user.LastLogon.ToString();
            LastPasswordSet = user.LastPasswordSet.ToString();
            //Manager = GetManager(user);
            //MemberOf = AdGroup.GetAdGroupSamAccountNames(user.GetGroups());
            //MiddleName = user.MiddleName;
            //Mobile = GetMobile(user);
            //Name = user.Name;
            //PasswordNeverExpires = user.PasswordNeverExpires;
            //PasswordNotRequired = user.PasswordNotRequired;
            //ProfilePath = AdToolkit.GetProperty(user, "profilePath");
            SamAccountName = user.SamAccountName;
            ScriptPath = user.ScriptPath;
            Sid = user.Sid.Value;
            SmartCardLogonRequired = user.SmartcardLogonRequired;
            Surname = user.Surname;
            UserCannotChangePassword = user.UserCannotChangePassword;
            UserPrincipalName = user.UserPrincipalName;
            VoiceTelephoneNumber = user.VoiceTelephoneNumber;

            //user.GetGroups
        }

        #endregion constructors

        #region DataMembers

        [DataMember]
        public string AccountExpirationData { get; set; }

        [DataMember]
        public string AccountLockoutTime { get; set; }

        [DataMember]
        public bool AllowReversiblePasswordEncryption { get; set; }

        [DataMember]
        public int BadLogonCount { get; set; }

        [DataMember]
        public string Company { get; set; }

        [DataMember]
        public bool DelegationPermitted { get; set; }

        [DataMember]
        public string Department { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public string DisplayName { get; set; }

        [DataMember]
        public string DistinguishedName { get; set; }

        [DataMember]
        public string EmailAddress { get; set; }

        [DataMember]
        public string EmployeeId { get; set; }

        [DataMember]
        public bool? Enabled { get; set; }

        [DataMember]
        public string GivenName { get; set; }

        [DataMember]
        public string Guid { get; set; }

        [DataMember]
        public string HomeDirectory { get; set; }

        [DataMember]
        public string HomeDrive { get; set; }

        [DataMember]
        public string LastBadPasswordAttempt { get; set; }

        [DataMember]
        public string LastLogon { get; set; }

        [DataMember]
        public string LastPasswordSet { get; set; }

        [DataMember]
        public string Manager { get; set; }

        [DataMember]
        public List<string> MemberOf { get; set; }

        [DataMember]
        public string MiddleName { get; set; }

        [DataMember]
        public string Mobile { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public bool PasswordNeverExpires { get; set; }

        [DataMember]
        public bool PasswordNotRequired { get; set; }

        [DataMember]
        public string ProfilePath { get; set; }

        [DataMember]
        public string SamAccountName { get; set; }

        [DataMember]
        public string ScriptPath { get; set; }

        [DataMember]
        public string Sid { get; set; }

        [DataMember]
        public bool SmartCardLogonRequired { get; set; }

        [DataMember]
        public string Surname { get; set; }

        [DataMember]
        public bool UserCannotChangePassword { get; set; }

        [DataMember]
        public string UserPrincipalName { get; set; }

        [DataMember]
        public string VoiceTelephoneNumber { get; set; }

        #endregion DataMembers

        #region methods

        /// <summary>
        /// The method returns a list of AdUser objects that is compiled from a list
        /// of UserPrincipal objects.
        /// </summary>
        /// <param name="users"></param>
        /// <returns>A list of AdUser objects.</returns>
        public static List<AdUser> GetAdUsers(List<UserPrincipal> users)
        {
            List<AdUser> results = new List<AdUser>();

            foreach (UserPrincipal u in users)
            {
                results.Add(new AdUser(u));
            }

            return results;
        }

        /// <summary>
        /// This method returns a list of user name that is compilied from a list of
        /// UserPrincipal objects. The names returned are the values of the UserPrincipal's
        /// Name attribute.
        /// </summary>
        /// <param name="users"></param>
        /// <returns>A list of strings that are user names.</returns>
        public static List<string> GetAdUserNames(List<UserPrincipal> users)
        {
            return GetAdUsers(users).Select(x => x.Name).OrderBy(x => x).ToList();
        }

        /// <summary>
        /// Thie method returns a list of user SamAccountNames that is compiled from a list of
        /// UserPrincipal objects. The SamAccountNames are the values of the UserPrincipal's
        /// SamAccountName attribute.
        /// </summary>
        /// <param name="users"></param>
        /// <returns>A list of strings that are SamAccountNames of users.</returns>
        public static List<string> GetAdUserSamAccountNames(List<UserPrincipal> users)
        {
            return GetAdUsers(users).Select(x => x.SamAccountName).OrderBy(x => x).ToList();
        }

        //public static string GetCompany(Principal user)
        //{
        //    return AdToolkit.GetProperty(user, "company");
        //}

        //public static string GetDepartment(Principal user)
        //{
        //    return AdToolkit.GetProperty(user, "department");
        //}

        //public static string GetManager(Principal user)
        //{
        //    string managerDn = AdToolkit.GetProperty(user, "manager");
        //    return AdToolkit.GetSamAccountName(managerDn);
        //}

        //public static string GetMobile(Principal user)
        //{
        //    return AdToolkit.GetProperty(user, "mobile");
        //}

        /// <summary>
        /// To string override function.
        /// </summary>
        /// <returns>A string representing this AdUser object.</returns>
        public new string ToString()
        {
            return string.Format("{0}\n{1}\n{2}\n{3}", SamAccountName, DisplayName, DistinguishedName, Description);
        }

        #endregion methods
    }
}