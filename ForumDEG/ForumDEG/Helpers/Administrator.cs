using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ForumDEG.Helpers {
    class Administrator {
        private HttpClient _client;

        public Administrator() {
            _client = new HttpClient();
            _client.MaxResponseContentBufferSize = 256000;
        }

        public async Task<Models.Administrator> GetAdministratorAsync(string registration) {
            var uri = new Uri(string.Format(Constants.RestUrl, "administrators/" + registration));

            try {
                var response = await _client.GetAsync(uri);

                if (response.IsSuccessStatusCode) {
                    var content = await response.Content.ReadAsStringAsync();

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

                return null;
            } catch (Exception ex) {
                Debug.WriteLine("[Administrator API exception]: " + ex.Message);
                return null;
            }
        }

        public async Task<List<Models.Administrator>> GetAdministratorsAsync() {
            var uri = new Uri(string.Format(Constants.RestUrl, "administrators"));

            try {
                var response = await _client.GetAsync(uri);
                List<Models.Administrator> administrators = new List<Models.Administrator>();

                if (response.IsSuccessStatusCode) {
                    var content = await response.Content.ReadAsStringAsync();
                    Debug.WriteLine("[Administrator API] - Administrators: " + content);

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
                }

                return administrators;
            } catch (Exception ex) {
                Debug.WriteLine("[Administrator API exception]: " + ex.Message);
                return null;
            }
        }

        public async /*Task<bool>*/ void PostAdministratorAsync(Models.Administrator administrator) {
            var uri = new Uri(string.Format(Constants.RestUrl, "administrators"));

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
            body.Add("user", administratorData);

            var content = new StringContent(body.ToString(), Encoding.UTF8, "application/json");
            var contentString = await content.ReadAsStringAsync();

            try {
                var response = await _client.PostAsync(uri, content);
                if (response.IsSuccessStatusCode) {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    Debug.WriteLine("[Administrator API] - Post result: " + responseContent);
                    //return true;
                } else {
                    var failedContent = await response.Content.ReadAsStringAsync();
                    Debug.WriteLine("[Administrator API] - Post response unsuccessful " + failedContent);
                    //return false;
                }
            } catch (Exception ex) {
                Debug.WriteLine("[Administrator API exception]:" + ex.Message);
                //return false;
            }
        }

        public async Task<bool> PutAdministratorAsync(string registration, Models.Administrator admin) {
            var uri = new Uri(string.Format(Constants.RestUrl, "administrators/" + registration));
            var oldAdmin = await GetAdministratorAsync(registration);

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

            var content = new StringContent(body.ToString(), Encoding.UTF8, "application/json");
            var contentString = await content.ReadAsStringAsync();

            try {
                var response = await _client.PutAsync(uri, content);
                if (response.IsSuccessStatusCode) {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    Debug.WriteLine("[Administrator API] - Put result: " + responseContent);
                    return true;
                } else {
                    var failedContent = await response.Content.ReadAsStringAsync();
                    Debug.WriteLine("[Administrator API] - Put response unsuccessful " + failedContent);
                    return false;
                }
            } catch (Exception ex) {
                Debug.WriteLine("[Administrator API exception]:" + ex.Message);
                return false;
            }
        }

        public async Task<bool> DeleteAdministratorAsync(string registration) {
            var uri = new Uri(string.Format(Constants.RestUrl, "administrators/" + registration));
            var emptyBody = new StringContent(""); // for some reason can't send request without body

            try {
                var response = await _client.DeleteAsync(uri);

                if (response.IsSuccessStatusCode) {
                    var content = await response.Content.ReadAsStringAsync();
                    Debug.WriteLine("[Administrator API] - Delete result: " + content);
                    return true;
                } else {
                    var failedContent = await response.Content.ReadAsStringAsync();
                    Debug.WriteLine("[Administrator API] - Delete response unsuccessful " + failedContent);
                    return false;
                }
            } catch (Exception ex) {
                Debug.WriteLine("[Administrator API exception]:" + ex.Message);
                return false;
            }
        }
    }
}
