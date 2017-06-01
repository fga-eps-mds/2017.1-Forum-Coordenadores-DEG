using NUnit.Framework;
using ForumDEG.Helpers;
using ForumDEG.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using System.Diagnostics;

namespace Tests {
    class CoordinatorParserTests {
        public static string content = "{\"course\":\"Engenharia de Software\",\"email\":\"finn@thehuman.ooo\",\"name\":\"Finn, The Human\",\"password\":\"PB12345\",\"registration\":\"123456789\"}";
        public static string contentList = "[" + content + ",{\"course\":\"Engenharia de Software\",\"email\":\"finn@thehuman.ooo\",\"name\":\"Finn, The Human\",\"password\":\"PB12345\",\"registration\":\"12345678\"}" + "]";


        public static string name = "Finn, The Human";
        public static string course = "Engenharia de Software";
        public static string email = "finn@thehuman.ooo";
        public static string password = "PB12345";
        public static string registration = "123456789";

        public static string newName = "New name";
        public static string newEmail = "New email";
        public static string newCourse = "New course";
        public static string newPassword = "New password";
        public static string newRegistration = "New registration";


        [Test()]
        public void GetCoordinatorParser_WhenCalled_CreateCoordinatorWithCorrectProperties() {
            Coordinator coordinator = CoordinatorParser.GetCoordinatorParser(content, Constants.Registration);

            Assert.AreEqual(name, coordinator.Name);
            Assert.AreEqual(course, coordinator.Course);
            Assert.AreEqual(email, coordinator.Email);
            Assert.AreEqual(password, coordinator.Password);
            Assert.AreEqual(registration, coordinator.Registration);
        }

        [Test()]
        public void GetCoordinatorsParser_WhenCalled_CreateListWithCorrectAmountOfCoordinators() {
            List<Coordinator> coordinators = CoordinatorParser.GetCoordinatorsParser(contentList);

            var listSize = coordinators.Count;

            Assert.AreEqual(2, listSize);
        }

        [Test()]
        public void PostCoordinatorBuilder_WhenCalled_ShouldCreateObjectWithCorrectData() {
            Coordinator coordinator = new Coordinator {
                Name = name,
                Email = email,
                Course = course,
                Password = password,
                Registration = registration
            };

            JObject obj = CoordinatorParser.PostCoordinatorBuilder(coordinator);

            var coordBody = obj["coordinator"];

            var coordName = coordBody["name"].ToString();
            var coordEmail = coordBody["email"].ToString();
            var coordCourse = coordBody["course"].ToString();
            var coordPass = coordBody["password"].ToString();
            var coordRegist = coordBody["registration"].ToString();

            Assert.AreEqual(name, coordName);
            Assert.AreEqual(email, coordEmail);
            Assert.AreEqual(course, coordCourse);
            Assert.AreEqual(password, coordPass);
            Assert.AreEqual(registration, coordRegist);
        }

        [Test()]
        public void PutCoordinatorBuilder_AllFieldsChanged() {
            Coordinator oldCoordinator = new Coordinator {
                Name = name,
                Email = email,
                Course = course,
                Password = password,
                Registration = registration
            };

            Coordinator newCoordinator = new Coordinator {
                Name = newName,
                Email = newEmail,
                Course = newCourse,
                Password = newPassword,
                Registration = newRegistration
            };

            JObject obj = CoordinatorParser.PutCoordinatorBuilder(oldCoordinator, newCoordinator);

            var coordBody = obj["coordinator"];

            var coordName = coordBody["name"].ToString();
            var coordEmail = coordBody["email"].ToString();
            var coordCourse = coordBody["course"].ToString();
            var coordPass = coordBody["password"].ToString();
            var coordRegist = coordBody["registration"].ToString();

            Assert.AreEqual(newName, coordName);
            Assert.AreEqual(newEmail, coordEmail);
            Assert.AreEqual(newCourse, coordCourse);
            Assert.AreEqual(newPassword, coordPass);
            Assert.AreEqual(newRegistration, coordRegist);
        }

        [Test()]
        public void PutCoordinatorBuilder_OneFieldChanged() {
            Coordinator oldCoordinator = new Coordinator {
                Name = name,
                Email = email,
                Course = course,
                Password = password,
                Registration = registration
            };

            Coordinator newCoordinator = new Coordinator {
                Name = newName,
            };

            JObject obj = CoordinatorParser.PutCoordinatorBuilder(oldCoordinator, newCoordinator);

            var coordBody = obj["coordinator"];
            var children = coordBody.Count();

            var coordName = coordBody["name"].ToString();

            Assert.AreEqual(newName, coordName);
            Assert.AreEqual(1, children);
        }

        [Test()]
        public void PutCoordinatorBuilder_SomeFieldsChanged() {
            Coordinator oldCoordinator = new Coordinator {
                Name = name,
                Email = email,
                Course = course,
                Password = password,
                Registration = registration
            };

            Coordinator newCoordinator = new Coordinator {
                Name = newName,
                Email = newEmail,
                Course = newCourse
            };

            JObject obj = CoordinatorParser.PutCoordinatorBuilder(oldCoordinator, newCoordinator);

            var coordBody = obj["coordinator"];
            var children = coordBody.Count();

            var coordName = coordBody["name"].ToString();
            var coordEmail = coordBody["email"].ToString();
            var coordCourse = coordBody["course"].ToString();

            Assert.AreEqual(newName, coordName);
            Assert.AreEqual(newEmail, coordEmail);
            Assert.AreEqual(newCourse, coordCourse);
            Assert.AreEqual(3, children);
        }

        [Test()]
        public void PutCoordinatorBuilder_NoFieldsChanged() {
            Coordinator oldCoordinator = new Coordinator {
                Name = name,
                Email = email,
                Course = course,
                Password = password,
                Registration = registration
            };

            Coordinator newCoordinator = new Coordinator();

            JObject obj = CoordinatorParser.PutCoordinatorBuilder(oldCoordinator, newCoordinator);

            var coordBody = obj["coordinator"];
            var children = coordBody.Count();

            Assert.AreEqual(0, children);
        }
    }
}
