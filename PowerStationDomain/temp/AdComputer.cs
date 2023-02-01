using System.DirectoryServices.AccountManagement;
using System.Runtime.Serialization;

namespace PowerStationDomain.temp
{
    [DataContract]
    public class AdComputer
    {

        #region constructors

        /// <summary>
        /// Default constructor that initializes attributes with empty values.
        /// </summary>
        public AdComputer()
        {
            AccountExpirationDate = string.Empty;
            AccountLockouttime = string.Empty;
            BadLogonCount = 0;
            DelegationPermitted = false;
            Description = string.Empty;
            DisplayName = string.Empty;
            DistinguishedName = string.Empty;
            Enabled = false;
            Guid = string.Empty;
            HomeDirectory = string.Empty;
            HomeDrive = string.Empty;
            LastBadPasswordAttempt = string.Empty;
            Name = string.Empty;
            PasswordNeverExpires = false;
            PasswordNotRequired = false;
            SamAccountName = string.Empty;
            ScriptPath = string.Empty;
            ServicePrincipalNames = null;
            Sid = string.Empty;
            SmartcardLogonRequired = false;
            UserCannotChangePassword = false;
            UserPrincipalName = string.Empty;
        }

        /// <summary>
        /// Constructor that instantiates an AdComputer object with values of the attributes of the
        /// specified ComputerPrincipal object.
        /// </summary>
        /// <param name="computer"></param>
        public AdComputer(ComputerPrincipal computer)
        {
            AccountExpirationDate = computer.AccountExpirationDate.ToString();
            AccountLockouttime = computer.AccountLockoutTime.ToString();
            BadLogonCount = computer.BadLogonCount;
            DelegationPermitted = computer.DelegationPermitted;
            Description = computer.Description;
            DisplayName = computer.DisplayName;
            DistinguishedName = computer.DistinguishedName;
            Enabled = computer.Enabled;
            Guid = computer.Guid.ToString();
            HomeDirectory = computer.HomeDirectory;
            HomeDrive = computer.HomeDrive;
            LastBadPasswordAttempt = computer.LastBadPasswordAttempt.ToString();
            Name = computer.Name;
            PasswordNeverExpires = computer.PasswordNeverExpires;
            PasswordNotRequired = computer.PasswordNotRequired;
            SamAccountName = computer.SamAccountName;
            ScriptPath = computer.ScriptPath;

            List<string> svcPNs = new List<string>();
            foreach (var spn in computer.ServicePrincipalNames)
            {
                svcPNs.Add(spn);
            }
            ServicePrincipalNames = svcPNs;

            Sid = computer.Sid.Value;
            SmartcardLogonRequired = computer.SmartcardLogonRequired;
            UserCannotChangePassword = computer.UserCannotChangePassword;
            UserPrincipalName = computer.UserPrincipalName;
        }

        #endregion constructors

        #region DataMembers

        [DataMember]
        public string AccountExpirationDate { get; set; }

        [DataMember]
        public string AccountLockouttime { get; set; }

        [DataMember]
        public int BadLogonCount { get; set; }

        [DataMember]
        public bool DelegationPermitted { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public string DisplayName { get; set; }

        [DataMember]
        public string DistinguishedName { get; set; }

        [DataMember]
        public bool? Enabled { get; set; }

        [DataMember]
        public string Guid { get; set; }

        [DataMember]
        public string HomeDirectory { get; set; }

        [DataMember]
        public string HomeDrive { get; set; }

        [DataMember]
        public string LastBadPasswordAttempt { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public bool PasswordNeverExpires { get; set; }

        [DataMember]
        public bool PasswordNotRequired { get; set; }

        [DataMember]
        public string SamAccountName { get; set; }

        [DataMember]
        public string ScriptPath { get; set; }

        [DataMember]
        public List<string> ServicePrincipalNames { get; set; }

        [DataMember]
        public string Sid { get; set; }

        [DataMember]
        public bool SmartcardLogonRequired { get; set; }

        [DataMember]
        public bool UserCannotChangePassword { get; set; }

        [DataMember]
        public string UserPrincipalName { get; set; }

        #endregion DataMembers

        #region methods

        /// <summary>
        /// This method returns a list of AdComputer objects that are compiled from the specified
        /// list of ComputerPrincipal objects.
        /// </summary>
        /// <param name="computers"></param>
        /// <returns>A list of AdComputerObjects.</returns>
        public static List<AdComputer> GetAdComputers(List<ComputerPrincipal> computers)
        {
            List<AdComputer> results = new List<AdComputer>();

            foreach (ComputerPrincipal c in computers)
            {
                results.Add(new AdComputer(c));
            }

            return results;
        }

        /// <summary>
        /// Thie method returns a list of user SamAccountNames that is compiled from a list of
        /// ComputerPrincipal objects. The SamAccountNames are the values of the ComputerPrincipals'
        /// SamAccountName attribute.
        /// </summary>
        /// <param name="computers"></param>
        /// <returns>A list of strings that are the SamAccountNames of computers.</returns>
        public static List<string> GetAdComputerSamAccountNames(List<ComputerPrincipal> computers)
        {
            return GetAdComputers(computers).Select(x => x.SamAccountName).OrderBy(x => x).ToList();
        }

        /// <summary>
        /// This method returns a list of strings that are the names of the specified ComputerPrincipals.
        /// </summary>
        /// <param name="computers"></param>
        /// <returns>A list of strings that are computer names.</returns>
        public static List<string> GetAdComputerNames(List<ComputerPrincipal> computers)
        {
            return GetAdComputers(computers).Select(x => x.Name).OrderBy(x => x).ToList();
        }

        /// <summary>
        /// Override to string method.
        /// </summary>
        /// <returns>A string that represents this AdComputer object.</returns>
        public new string ToString()
        {
            return string.Format("{0}\n{1}\n{2}\n{3}", SamAccountName, DisplayName, DistinguishedName, Description);
        }

        #endregion methods

    }
}