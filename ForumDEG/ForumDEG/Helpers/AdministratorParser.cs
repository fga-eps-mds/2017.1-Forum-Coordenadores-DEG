using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumDEG.Helpers {
    public class AdministratorParser {
        public static Models.Administrator GetAdministratorParser(string content, string registration) {
            var obj = JObject.Parse(content);

            string name = obj["name"].ToString();
            string email = obj["email"].ToString();
            string password = obj["password"].ToString();

            Debug.WriteLine("[Administrator API]: Coord name: " + name);
            Debug.WriteLine("[Administrator API]: Coord email: " + email);
            Debug.WriteLine("[Administrator API]: Coord password: " + password);

            Models.Administrator administrator = new Models.Administrator {
                Name = name,
                Email = email,
                Password = password,
                Registration = registration
            };

            return administrator;
        }

        public static List<Models.Administrator> GetAdministratorsParser(string content) {
            List<Models.Administrator> administrators = new List<Models.Administrator>();
            var objArray = JArray.Parse(content);

            foreach (JObject obj in objArray) {
                string name = obj["name"].ToString();
                string email = obj["email"].ToString();
                string password = obj["password"].ToString();
                string registration = obj["registration"].ToString();

                Debug.WriteLine("[Administrator API]: Admin name: " + name);
                Debug.WriteLine("[Administrator API]: Admin email: " + email);
                Debug.WriteLine("[Administrator API]: Admin password: " + password);
                Debug.WriteLine("[Administrator API]: Admin registration: " + registration);

                administrators.Add(new Models.Administrator {
                    Name = name,
                    Email = email,
                    Password = password,
                    Registration = registration
                });
            }

            return administrators;
        }

        public static JObject PostAdministratorBuilder(Models.Administrator administrator) {
            var name = administrator.Name;
            var email = administrator.Email;
            var password = administrator.Password;
            var registration = administrator.Registration;

            var administratorData = new JObject();
            administratorData.Add("name", name);
            administratorData.Add("email", email);
            administratorData.Add("password", password);
            administratorData.Add("registration", registration);

            var body = new JObject();
            body.Add("administrator", administratorData);

            return body;
        }

        public static JObject PutAdministratorBuilder(Models.Administrator oldAdmin, Models.Administrator admin) {
            var adminData = new JObject();

            if (oldAdmin.Name != admin.Name && !String.IsNullOrEmpty(admin.Name)) {
                var name = admin.Name;
                adminData.Add("name", name);
            }

            if (oldAdmin.Email != admin.Email && !String.IsNullOrEmpty(admin.Email)) {
                var email = admin.Email;
                adminData.Add("email", email);
            }

            if (oldAdmin.Password != admin.Password && !String.IsNullOrEmpty(admin.Password)) {
                var password = admin.Password;
                adminData.Add("password", password);
            }

            if (oldAdmin.Registration != admin.Registration && !String.IsNullOrEmpty(admin.Registration)) {
                var newRegistration = admin.Registration;
                adminData.Add("registration", newRegistration);
            }

            var body = new JObject();
            body.Add("administrator", adminData);

            return body;
        }
    }
}
