using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ForumDEG.Helpers {
    class Coordinator {
        private HttpClient _client;

        public Coordinator() {
            _client = new HttpClient();
            _client.MaxResponseContentBufferSize = 256000;
        }

        public async Task<Models.Coordinator> GetCoordinatorAsync(string registration) {
            var uri = new Uri(string.Format(Constants.RestUrl, "coordinators/" + registration));

            try {
                var response = await _client.GetAsync(uri);

                if (response.IsSuccessStatusCode) {
                    var content = await response.Content.ReadAsStringAsync();

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

                return null;
            } catch (Exception ex) {
                Debug.WriteLine("[Coordinator API exception]: " + ex.Message);
                return null;
            }
        }

        public async Task<List<Models.Coordinator>> GetCoordinatorsAsync() {
            var uri = new Uri(string.Format(Constants.RestUrl, "coordinators"));

            try {
                var response = await _client.GetAsync(uri);
                List<Models.Coordinator> coordinators = new List<Models.Coordinator>();

                if (response.IsSuccessStatusCode) {
                    var content = await response.Content.ReadAsStringAsync();
                    Debug.WriteLine("[Coordinator API] - Coordinators: " + content);

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
                }

                return coordinators;
            } catch (Exception ex) {
                Debug.WriteLine("[Coordinator API exception]:" + ex.Message);
                return null;
            }
        }

        public async Task<bool> PostCoordinatorAsync(Models.Coordinator coordinator) {
            var uri = new Uri(string.Format(Constants.RestUrl, "coordinators"));

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

            var content = new StringContent(body.ToString(), Encoding.UTF8, "application/json");
            var contentString = await content.ReadAsStringAsync();

            try {
                var response = await _client.PostAsync(uri, content);
                if (response.IsSuccessStatusCode) {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    Debug.WriteLine("[Coordinator API] - Post result: " + responseContent);
                    return true;
                } else {
                    var failedContent = await response.Content.ReadAsStringAsync();
                    Debug.WriteLine("[Coordinator API] - Post response unsuccessful " + failedContent);
                    return false;
                }
            } catch (Exception ex) {
                Debug.WriteLine("[Coordinator API exception]:" + ex.Message);
                return false;
            }
        }

        public async Task<bool> PutCoordinatorAsync(string registration, Models.Coordinator coordinator) {
            var uri = new Uri(string.Format(Constants.RestUrl, "coordinators/" + registration));
            var oldCoordinator = await GetCoordinatorAsync(registration);

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

            var content = new StringContent(body.ToString(), Encoding.UTF8, "application/json");
            var contentString = await content.ReadAsStringAsync();

            try {
                var response = await _client.PutAsync(uri, content);
                if (response.IsSuccessStatusCode) {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    Debug.WriteLine("[Coordinator API] - Put result: " + responseContent);
                    return true;
                } else {
                    var failedContent = await response.Content.ReadAsStringAsync();
                    Debug.WriteLine("[Coordinator API] - Put response unsuccessful " + failedContent);
                    return false;
                }
            } catch (Exception ex) {
                Debug.WriteLine("[Coordinator API exception]:" + ex.Message);
                return false;
            }
        }

        public async Task<bool> DeleteCoordinatorAsync(string registration) {
            var uri = new Uri(string.Format(Constants.RestUrl, "coordinators/" + registration));
            var emptyBody = new StringContent(""); // for some reason can't send request without body
            
            try {
                var response = await _client.DeleteAsync(uri);

                if (response.IsSuccessStatusCode) {
                    var content = await response.Content.ReadAsStringAsync();
                    Debug.WriteLine("[Coordinator API] - Delete result: " + content);
                    return true;
                } else {
                    var failedContent = await response.Content.ReadAsStringAsync();
                    Debug.WriteLine("[Coordinator API] - Delete response unsuccessful " + failedContent);
                    return false;
                }
            } catch (Exception ex) {
                Debug.WriteLine("[Coordinator API exception]:" + ex.Message);
                return false;
            }
        }

        public async Task<bool> GetConfirmationStatusAsync(string registration, string forumId) {
            string confirmationRoute = "coordinators/" + registration + "/forum/" + forumId;
            var uri = new Uri(string.Format(Constants.RestUrl, confirmationRoute));

            try {
                var response = await _client.GetAsync(uri);

                if (response.IsSuccessStatusCode) {
                    var content = await response.Content.ReadAsStringAsync();

                    var obj = JObject.Parse(content);

                    var result = obj["success"].ToObject<bool>();
                    Debug.WriteLine("[Coordinator API] - Confirmation status: " + result.ToString());

                    return result;
                }
                return false;
            } catch (Exception ex) {
                Debug.WriteLine("[Coordinator API exception]:" + ex.Message);
                return false;
            }
        }

        public async void PostConfirmationStatusAsync(string registration, string forumId) {
            string confirmationRoute = "coordinators/" + registration + "/forum/" + forumId;
            var uri = new Uri(string.Format(Constants.RestUrl, confirmationRoute));
            var emptyBody = new StringContent("");

            try {
                await _client.PostAsync(uri, emptyBody);
            } catch (Exception ex) {
                Debug.WriteLine("[Coordinator API exception]:" + ex.Message);
            }
        }

        public async void DeleteConfirmationAsync(string registration, string forumId) {
            string confirmationRoute = "coordinators/" + registration + "/forum/" + forumId;
            var uri = new Uri(string.Format(Constants.RestUrl, confirmationRoute));
            var emptyBody = new StringContent("");

            try {
                await _client.DeleteAsync(uri);
            } catch (Exception ex) {
                Debug.WriteLine("[Coordinator API exception]:" + ex.Message);
            }
        }
    }
}
