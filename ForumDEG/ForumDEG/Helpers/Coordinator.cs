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
