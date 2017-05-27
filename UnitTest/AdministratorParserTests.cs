using NUnit.Framework;
using ForumDEG.Helpers;
using ForumDEG.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace UnitTest {
    class AdministratorParserTests {
        public static string content = "{\"email\":\"finn@thehuman.ooo\",\"name\":\"Finn, The Human\",\"password\":\"PB12345\",\"registration\":\"123456789\"}";
        public static string contentList = "[" + content + ",{\"email\":\"finn@thehuman.ooo\",\"name\":\"Finn, The Human\",\"password\":\"PB12345\",\"registration\":\"12345678\"}" + "]";

        public static string name = "Finn, The Human";
        public static string email = "finn@thehuman.ooo";
        public static string password = "PB12345";
        public static string registration = "123456789";

        public static string newName = "New name";
        public static string newEmail = "New email";
        public static string newPassword = "New password";
        public static string newRegistration = "New registration";

        [Test()]
        public void GetAdministratorParser_WhenCalled_CreateAdminWithCorrectProperties() {
            Administrator administrator = AdministratorParser.GetAdministratorParser(content, Constants.Registration);

            Assert.AreEqual(name, administrator.Name);
            Assert.AreEqual(email, administrator.Email);
            Assert.AreEqual(password, administrator.Password);
            Assert.AreEqual(registration, administrator.Registration);
        }

        [Test()]
        public void GetAdministratorsParser_WhenCalled_CreateListWithCorrectAmountOfAdministrators() {
            List<Administrator> administrators = AdministratorParser.GetAdministratorsParser(contentList);

            var listSize = administrators.Count;

            Assert.AreEqual(2, listSize);
        }

        [Test()]
        public void PostAdministratorBuilder_WhenCalled_ShouldCreateObjectWithCorrectData() {
            Administrator administrator = new Administrator {
                Name = name,
                Email = email,
                Password = password,
                Registration = registration
            };

            JObject obj = AdministratorParser.PostAdministratorBuilder(administrator);

            var adminBody = obj["administrator"];

            var adminName = adminBody["name"].ToString();
            var adminEmail = adminBody["email"].ToString();
            var adminPass = adminBody["password"].ToString();
            var adminRegist = adminBody["registration"].ToString();

            Assert.AreEqual(name, adminName);
            Assert.AreEqual(email, adminEmail);
            Assert.AreEqual(password, adminPass);
            Assert.AreEqual(registration, adminRegist);
        }

        [Test()]
        public void PutAdministratorBuilder_AllFieldsChanged() {
            Administrator oldAdministrator = new Administrator {
                Name = name,
                Email = email,
                Password = password,
                Registration = registration
            };

            Administrator newAdministrator = new Administrator {
                Name = newName,
                Email = newEmail,
                Password = newPassword,
                Registration = newRegistration
            };

            JObject obj = AdministratorParser.PutAdministratorBuilder(oldAdministrator, newAdministrator);

            var adminBody = obj["administrator"];

            var adminName = adminBody["name"].ToString();
            var adminEmail = adminBody["email"].ToString();
            var adminPass = adminBody["password"].ToString();
            var adminRegist = adminBody["registration"].ToString();

            Assert.AreEqual(newName, adminName);
            Assert.AreEqual(newEmail, adminEmail);
            Assert.AreEqual(newPassword, adminPass);
            Assert.AreEqual(newRegistration, adminRegist);
        }

        [Test()]
        public void PutAdministratorBuilder_OneFieldChanged() {
            Administrator oldAdministrator = new Administrator {
                Name = name,
                Email = email,
                Password = password,
                Registration = registration
            };

            Administrator newAdministrator = new Administrator {
                Name = newName,
            };

            JObject obj = AdministratorParser.PutAdministratorBuilder(oldAdministrator, newAdministrator);

            var adminBody = obj["administrator"];
            var children = adminBody.Count();

            var adminName = adminBody["name"].ToString();

            Assert.AreEqual(newName, adminName);
            Assert.AreEqual(1, children);
        }

        [Test()]
        public void PutAdministratorBuilder_SomeFieldsChanged() {
            Administrator oldAdministrator = new Administrator {
                Name = name,
                Email = email,
                Password = password,
                Registration = registration
            };

            Administrator newAdministrator = new Administrator {
                Name = newName,
                Email = newEmail,
            };

            JObject obj = AdministratorParser.PutAdministratorBuilder(oldAdministrator, newAdministrator);

            var adminBody = obj["administrator"];
            var children = adminBody.Count();

            var adminName = adminBody["name"].ToString();
            var adminEmail = adminBody["email"].ToString();;

            Assert.AreEqual(newName, adminName);
            Assert.AreEqual(newEmail, adminEmail);
            Assert.AreEqual(2, children);
        }

        [Test()]
        public void PutAdministratorBuilder_NoFieldsChanged() {
            Administrator oldAdministrator = new Administrator {
                Name = name,
                Email = email,
                Password = password,
                Registration = registration
            };

            Administrator newAdministrator = new Administrator();

            JObject obj = AdministratorParser.PutAdministratorBuilder(oldAdministrator, newAdministrator);

            var adminBody = obj["administrator"];
            var children = adminBody.Count();

            Assert.AreEqual(0, children);
        }
    }
}
