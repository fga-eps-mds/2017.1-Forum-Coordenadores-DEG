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
                    return CoordinatorParser.GetCoordinatorParser(content, registration);
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
                    coordinators = CoordinatorParser.GetCoordinatorsParser(content);
                }

                return coordinators;
            } catch (Exception ex) {
                Debug.WriteLine("[Coordinator API exception]:" + ex.Message);
                return null;
            }
        }

        public async Task<bool> PostCoordinatorAsync(Models.Coordinator coordinator) {
            var uri = new Uri(string.Format(Constants.RestUrl, "coordinators"));

            var body = CoordinatorParser.PostCoordinatorBuilder(coordinator);

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

            var body = CoordinatorParser.PutCoordinatorBuilder(oldCoordinator, coordinator);

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
