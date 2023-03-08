using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.DirectoryServices.AccountManagement;
using System.Drawing;
using System.Linq;
using System.Resources;
using FluentAssertions;
using PowerStationInfrastructure.helpers;
using PowerStationInfrastructureTests.Properties;
using Xunit;
using System.IO;

namespace PowerStationInfrastructure.helpers.Tests
{
    [TestClass()]
    public class AdHelperTests
    {
        [Fact()]
        public void DomainExistsNoForestsTest()
        {
            AdHelper.IsMember("Administrator", "Administrators");
        }

        [Fact()]
        public void GetUsedAttributesTest()
        {
            AdHelper.GetUsedAttributes("Administrator").Should().NotBeNull();
        }

        [Fact()]
        public void GetMembersTest()
        {
            AdHelper.GetMembers("Domain Admins").Should().NotBeNull();
        }

        [Fact()]
        public void GetPropertyTestPrincipqle()
        {
            var grp = AdHelper.GetMembers("Administrators").First(u => u.SamAccountName.Equals("Administrator"));
            var property = AdHelper.GetProperty(grp, "name");
            property.Should().NotBeNull();
        }

        [Fact()]
        public void GetPropertyTestString()
        {
            var property = AdHelper.GetProperty("Administrator", "name");
            property.Should().NotBeNull();
        }

        [Fact()]
        public void GetRandomPasswordTest()
        {
            AdHelper.GetRandomPassword().Should().HaveLength(12);
            AdHelper.GetRandomPassword(12).Should().HaveLength(12);
            AdHelper.GetRandomPassword(20).Should().HaveLength(20);
            AdHelper.GetRandomPassword(5).Should().BeNull();
        }

        [Fact()]
        public void GetRootContainerTest()
        {
            var container = AdHelper.GetRootContainer();
            container.Should().Contain("DC");
        }

        [Fact()]
        public void GetSamAccountNameTest()
        {
            AdHelper.GetSamAccountName("Administrator").Should().BeEquivalentTo("Administrator");
        }

        [Fact()]
        public void SetPropertyTest()
        {
            var grp = AdHelper.GetMembers("Administrators").First(u => u.SamAccountName.Equals("Administrator"));
            var property = AdHelper.GetProperty(grp, "name");
            property.Should().NotBeNull();
            AdHelper.SetProperty(grp, "description", "test").Should().BeTrue();
            var grp2 = AdHelper.GetMembers("Administrators").First(u => u.SamAccountName.Equals("Administrator"));
            AdHelper.GetProperty(grp2, "description").Should().BeEquivalentTo("test");
        }

        [Fact()]
        public void SetPropertyStringTest()
        {
            var grp = AdHelper.GetMembers("Administrators").First(u => u.SamAccountName.Equals("Administrator"));
            var property = AdHelper.GetProperty(grp, "name");
            property.Should().NotBeNull();
            AdHelper.SetProperty("Administrator", "description", "test").Should().BeTrue();
            var grp2 = AdHelper.GetMembers("Administrators").First(u => u.SamAccountName.Equals("Administrator"));
            AdHelper.GetProperty(grp2, "description").Should().BeEquivalentTo("test");
        }

        // [Fact()]
        public void UnlockTest()
        {
            UserPrincipal principal =
                new UserPrincipal(new PrincipalContext(ContextType.Domain, Environment.UserDomainName))
                {
                    SamAccountName = "lockuser",
                    Name = "lockuser",
                    Enabled = true,
                    DisplayName = "lockuser"
                };
            principal.Save();
            AdHelper.IsLocked(principal).Should().BeFalse();

            AdHelper.Unlock("lockuser").Should().BeTrue();
            principal.Delete();
        }

        [Fact()]
        public void ValidateCredentialsTest()
        {
            var principal =
                new UserPrincipal(new PrincipalContext(ContextType.Domain, Environment.UserDomainName))
                {
                    SamAccountName = "Testlockuser",
                    Name = "Testlockuser",
                    Enabled = true,
                    DisplayName = "Testlockuser"
                };
            var pass = AdHelper.GetRandomPassword();
            principal.Save();
            AdHelper.SetPassword("Testlockuser", pass);
            AdHelper.ValidateCredentials("Testlockuser", pass);
            principal.Delete();
        }

        [Fact()]
        public void MoveTest()
        {
            var principal =
                new UserPrincipal(new PrincipalContext(ContextType.Domain, Environment.UserDomainName))
                {
                    SamAccountName = "MoveUser",
                    Name = "MoveUser",
                    Enabled = true,
                    DisplayName = "MoveUser"
                };

            principal.Save();
            AdHelper.Move("MoveUser", "Temp");
            AdHelper.Exists("MoveUser").Should().BeTrue();
        }

        [Fact()]
        public void ExistsTest()
        {
            var principal =
                new UserPrincipal(new PrincipalContext(ContextType.Domain, Environment.UserDomainName))
                {
                    SamAccountName = "ExistsTest",
                    Name = "ExistsTest",
                    Enabled = true,
                    DisplayName = "ExistsTest"
                };
            principal.Save();
            AdHelper.Exists("ExistsTest");
            principal.Delete();
        }


        [Fact()]
        public void NotExistsTest()
        {
            AdHelper.Exists("NotExistsTest").Should().BeFalse();
        }

        //    [Fact()]
        public void RemoveMemberTest()
        {

        }

        [Fact()]
        public void IsMemberTest()
        {
            AdHelper.IsMember("Administrator", "Administrators").Should().BeTrue();
        }


        [Fact()]
        public void GetDistinguishedNameTest()
        {
            var dis = AdHelper.GetDistinguishedName("Administrator");
            dis.Should().Contain("CN");
        }
        [Fact()]
        public void GetDistinguishedNameFalseTest()
        {
            AdHelper.GetDistinguishedName("Adm").Should().BeEmpty();
        }

        [Fact()]
        public void GetCurrentDomainTest()
        {
            AdHelper.GetCurrentDomain().Should().NotBeEmpty();
        }

        [Fact()]
        public void GetDomainControllersTest()
        {
            AdHelper.GetDomainControllers().Should().NotBeNull();
        }

        [Fact()]
        public void GetForestDomainsTest()
        {
            AdHelper.GetForestDomains().Should().NotBeNull();
        }

        [Fact()]
        public void GetFullDomainNameTest()
        {
            var forest = AdHelper.GetFullDomainName(AdHelper.GetForestDomains()[0]);
            forest.Should().NotBeNull();
        }

        [Fact()]
        public void GetGlobalCatalogServersTest()
        {
            AdHelper.GetGlobalCatalogServers().Should().HaveCountGreaterOrEqualTo(1);
        }
        [Fact()]
        public void DomainExistTest()
        {
            AdHelper.DomainExist("OPSLABO.BE").Should().BeTrue();
        }
        [Fact()]
        public void DomainExistFalseTest()
        {
            AdHelper.DomainExist("OPSLO.BE").Should().BeFalse();
        }

        [Fact()]
        public void EnumerateOuTest()
        {
            AdHelper.EnumerateOu(AdHelper.GetDistinguishedName("Administrator")).Should().NotBeNull();
        }

        [Fact()]
        public void InsertPictureTest()
        {
            Bitmap picture=Resources._20150725_203401;
            var principal =
                new UserPrincipal(new PrincipalContext(ContextType.Domain, Environment.UserDomainName))
                {
                    SamAccountName = "TestlockuserPicture2",
                    Name = "TestlockuserPicture2",
                    Enabled = true,
                    DisplayName = "TestlockuserPicture2"
                };
            var pass = AdHelper.GetRandomPassword();
            principal.Save();
            AdHelper.SetPassword("TestlockuserPicture", pass);
            AdHelper.ValidateCredentials("TestlockuserPicture2", pass);

            AdHelper.InsertPicture("TestlockuserPicture2", ImageToByte2(picture));
            AdHelper.GetUserPicture("TestlockuserPicture2").Should().NotBeNull();
            principal.Delete();
        }


        private  byte[] ImageToByte2(Image img)
        {
            using var stream = new MemoryStream();
            img.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
            return stream.ToArray();
        }

    }
}
