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
                    return AdministratorParser.GetAdministratorParser(content, registration);
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
                    administrators = AdministratorParser.GetAdministratorsParser(content);
                }

                return administrators;
            } catch (Exception ex) {
                Debug.WriteLine("[Administrator API exception]: " + ex.Message);
                return null;
            }
        }

        public async Task<bool> PostAdministratorAsync(Models.Administrator administrator) {
            var uri = new Uri(string.Format(Constants.RestUrl, "administrators"));

            var body = AdministratorParser.PostAdministratorBuilder(administrator);

            var content = new StringContent(body.ToString(), Encoding.UTF8, "application/json");
            var contentString = await content.ReadAsStringAsync();

            try {
                var response = await _client.PostAsync(uri, content);
                if (response.IsSuccessStatusCode) {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    Debug.WriteLine("[Administrator API] - Post result: " + responseContent);
                    return true;
                } else {
                    var failedContent = await response.Content.ReadAsStringAsync();
                    Debug.WriteLine("[Administrator API] - Post response unsuccessful " + failedContent);
                    return false;
                }
            } catch (Exception ex) {
                Debug.WriteLine("[Administrator API exception]:" + ex.Message);
                return false;
            }
        }

        public async Task<bool> PutAdministratorAsync(string registration, Models.Administrator admin) {
            var uri = new Uri(string.Format(Constants.RestUrl, "administrators/" + registration));
            var oldAdmin = await GetAdministratorAsync(registration);

            var body = AdministratorParser.PutAdministratorBuilder(oldAdmin, admin);

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
