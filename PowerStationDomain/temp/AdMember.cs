using System.DirectoryServices.AccountManagement;
using System.Runtime.Serialization;

namespace PowerStationDomain.temp
{
    [DataContract]
    public class AdMember
    {
        #region constructors

        /// <summary>
        /// Default constructor that initializes attributes with empty values.
        /// </summary>
        public AdMember()
        {
            Description = string.Empty;
            DisplayName = string.Empty;
            DistinguishedName = string.Empty;
            Guid = string.Empty;
            Name = string.Empty;
            SamAccountName = string.Empty;
            Sid = string.Empty;
            UserPrincipalName = string.Empty;
        }

        /// <summary>
        /// Constructor that instantiates an AdMember object with the attribute values of
        /// the specified Principal.
        /// </summary>
        /// <param name="member"></param>
        public AdMember(Principal member)
        {
            Description = member.Description;
            DisplayName = member.DisplayName;
            DistinguishedName = member.DistinguishedName;
            Guid = member.Guid.ToString();
            Name = member.Name;
            SamAccountName = member.SamAccountName;
            Sid = member.Sid.Value;
            UserPrincipalName = member.UserPrincipalName;
        }

        #endregion constructors

        #region DataMembers

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public string DisplayName { get; set; }

        [DataMember]
        public string DistinguishedName { get; set; }

        [DataMember]
        public string Guid { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string SamAccountName { get; set; }

        [DataMember]
        public string Sid { get; set; }

        [DataMember]
        public string UserPrincipalName { get; set; }

        #endregion DataMembers

        #region methods

        /// <summary>
        /// This method returns a list of AdMember objects compilied from the specified
        /// PrincipalCollection.
        /// </summary>
        /// <param name="members"></param>
        /// <returns>A list of AdMember objects.</returns>
        public static List<AdMember> GetAdMembers(PrincipalCollection members)
        {
            List<AdMember> results = new List<AdMember>();

            foreach (Principal m in members)
            {
                AdMember member = new AdMember();

                member.Description = m.Description;
                member.DisplayName = m.DisplayName;
                member.DistinguishedName = m.DistinguishedName;
                member.Guid = m.Guid.ToString();
                member.Name = m.Name;
                member.SamAccountName = m.SamAccountName;
                member.Sid = m.Sid.Value;
                member.UserPrincipalName = m.UserPrincipalName;

                results.Add(member);
            }

            return results.OrderBy(x => x.SamAccountName).ToList();
        }

        /// <summary>
        /// This method returns a list of strings that are the SamAccountNames of the Principals in the
        /// specified PrincipalCollection.
        /// </summary>
        /// <param name="members"></param>
        /// <returns>A list of strings that are SamAccountNames.</returns>
        public static List<string> GetAdMemberSamAccountNames(PrincipalCollection members)
        {
            return GetAdMembers(members).Select(x => x.SamAccountName).OrderBy(x => x).ToList();
        }

        /// <summary>
        /// Override to string method.
        /// </summary>
        /// <returns>A string representing this AdMember object.</returns>
        public new string ToString()
        {
            return string.Format("{0}\n{1}\n{2}\n{3}", SamAccountName, DisplayName, DistinguishedName, Description);
        }

        #endregion methods
    }
}