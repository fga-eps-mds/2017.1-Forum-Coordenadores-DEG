using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumDEG.Helpers {
    public class CoordinatorParser {
        public static Models.Coordinator GetCoordinatorParser(string content, string registration) {
            var obj = JObject.Parse(content);

            string name = obj["name"].ToString();
            string email = obj["email"].ToString();
            string course = obj["course"].ToString();
            string password = obj["password"].ToString();

            Debug.WriteLine("[Coordinator API]: Coord name: " + name);
            Debug.WriteLine("[Coordinator API]: Coord email: " + email);
            Debug.WriteLine("[Coordinator API]: Coord course: " + course);
            Debug.WriteLine("[Coordinator API]: Coord password: " + password);

            Models.Coordinator coordinator = new Models.Coordinator {
                Name = name,
                Email = email,
                Course = course,
                Password = password,
                Registration = registration
            };

            return coordinator;
        }

        public static List<Models.Coordinator> GetCoordinatorsParser(string content) {
            List<Models.Coordinator> coordinators = new List<Models.Coordinator>();
            var objArray = JArray.Parse(content);

            foreach (JObject obj in objArray) {
                string name = obj["name"].ToString();
                string email = obj["email"].ToString();
                string course = obj["course"].ToString();
                string password = obj["password"].ToString();
                string registration = obj["registration"].ToString();

                Debug.WriteLine("[Coordinator API]: Coord name: " + name);
                Debug.WriteLine("[Coordinator API]: Coord email: " + email);
                Debug.WriteLine("[Coordinator API]: Coord course: " + course);
                Debug.WriteLine("[Coordinator API]: Coord password: " + password);
                Debug.WriteLine("[Coordinator API]: Coord registration: " + registration);

                coordinators.Add(new Models.Coordinator {
                    Name = name,
                    Email = email,
                    Course = course,
                    Password = password,
                    Registration = registration
                });
            }

            return coordinators;
        }

        public static JObject PostCoordinatorBuilder(Models.Coordinator coordinator) {
            var name = coordinator.Name;
            var email = coordinator.Email;
            var password = coordinator.Password;
            var registration = coordinator.Registration;
            var course = coordinator.Course;

            var coordinatorData = new JObject();
            coordinatorData.Add("name", name);
            coordinatorData.Add("email", email);
            coordinatorData.Add("password", password);
            coordinatorData.Add("registration", registration);
            coordinatorData.Add("course", course);

            var body = new JObject();
            body.Add("coordinator", coordinatorData);

            return body;
        }

        public static JObject PutCoordinatorBuilder(Models.Coordinator oldCoordinator, Models.Coordinator coordinator) {
            var coordinatorData = new JObject();

            if (oldCoordinator.Name != coordinator.Name && !String.IsNullOrEmpty(coordinator.Name)) {
                var name = coordinator.Name;
                coordinatorData.Add("name", name);
            }

            if (oldCoordinator.Email != coordinator.Email && !String.IsNullOrEmpty(coordinator.Email)) {
                var email = coordinator.Email;
                coordinatorData.Add("email", email);
            }

            if (oldCoordinator.Password != coordinator.Password && !String.IsNullOrEmpty(coordinator.Password)) {
                var password = coordinator.Password;
                coordinatorData.Add("password", password);
            }

            if (oldCoordinator.Registration != coordinator.Registration && !String.IsNullOrEmpty(coordinator.Registration)) {
                var newRegistration = coordinator.Registration;
                coordinatorData.Add("registration", newRegistration);
            }

            if (oldCoordinator.Course != coordinator.Course && !String.IsNullOrEmpty(coordinator.Course)) {
                var course = coordinator.Course;
                coordinatorData.Add("course", course);
            }

            var body = new JObject();
            body.Add("coordinator", coordinatorData);

            return body;
        }
    }
}
