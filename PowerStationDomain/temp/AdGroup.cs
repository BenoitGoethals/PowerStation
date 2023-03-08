using System.DirectoryServices.AccountManagement;
using System.Runtime.Serialization;

namespace PowerStationDomain.temp
{
    [DataContract]
    public class AdGroup
    {

        #region constructors

        public AdGroup()
        {
            Description = string.Empty;
            DisplayName = string.Empty;
            DistinguishedName = string.Empty;
            GroupScope = string.Empty;
            Guid = string.Empty;
            IsSecurityGroup = false;
            Members = null;
            Name = string.Empty;
            SamAccountName = string.Empty;
            Sid = string.Empty;
            UserPrincipalName = string.Empty;
        }

        public AdGroup(GroupPrincipal group)
        {
            Description = group.Description;
            DisplayName = group.DisplayName;
            DistinguishedName = group.DistinguishedName;
            GroupScope = group.GroupScope.ToString();
            Guid = group.Guid.ToString();
            IsSecurityGroup = group.IsSecurityGroup;
            Members = AdMember.GetAdMembers(group.Members);
            Name = group.Name;
            SamAccountName = group.SamAccountName;
            Sid = group.Sid.Value;
            UserPrincipalName = group.UserPrincipalName;
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
        public string GroupScope { get; set; }

        [DataMember]
        public string Guid { get; set; }

        [DataMember]
        public bool? IsSecurityGroup { get; set; }

        [DataMember]
        public List<AdMember> Members { get; set; }

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
        /// This method returns a list of AdGroup objects that are compiled from the specified list
        /// of GroupPrincipal objects.
        /// </summary>
        /// <param name="groups"></param>
        /// <returns>A list of AdGroup objects.</returns>
        public static List<AdGroup> GetAdGroups(List<GroupPrincipal> groups)
        {
            List<AdGroup> results = new List<AdGroup>();

            foreach (GroupPrincipal g in groups)
            {
                results.Add(new AdGroup(g));
            }

            return results;
        }

        /// <summary>
        /// This method returns a list of SamAccountNames compiled from the specified list
        /// of GroupPrincipal objects.
        /// </summary>
        /// <param name="groups"></param>
        /// <returns>A list of strings that are group SamAccountNames.</returns>
        public static List<string> GetAdGroupsSamAccountNames(List<GroupPrincipal> groups)
        {
            return GetAdGroups(groups).Select(x => x.SamAccountName).OrderBy(x => x).ToList();
        }

        public static List<string> GetAdGroupSamAccountNames(PrincipalSearchResult<Principal> groups)
        {
            List<string> results = new List<string>();

            foreach (Principal p in groups)
            {
                results.Add(p.SamAccountName);
            }

            return results;
        }

        /// <summary>
        /// Override to string method.
        /// </summary>
        /// <returns>A string representing this AdGroup object.</returns>
        public new string ToString()
        {
            return string.Format("{0}\n{1}\n{2}\n{3}", SamAccountName, DisplayName, DistinguishedName, Description);
        }

        #endregion methods
    }
}