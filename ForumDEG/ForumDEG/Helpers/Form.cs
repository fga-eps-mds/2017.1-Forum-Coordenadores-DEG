using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ForumDEG.Helpers {
    public class Form {
        private HttpClient _client;

        public Form() {
            _client = new HttpClient();
            _client.MaxResponseContentBufferSize = 256000;
        }

        public async Task<Models.Form> GetFormAsync(string id) {
            var uri = new Uri(string.Format(Constants.RestUrl, "forms/" + id));

            try {
                var response = await _client.GetAsync(uri);

                if (response.IsSuccessStatusCode) {
                    var content = await response.Content.ReadAsStringAsync();
                    Debug.WriteLine("[Form API]: " + content);
                    return FormParser.GetFormParser(content, id);
                }

                return null;
            } catch (Exception ex) {
                Debug.WriteLine("[Form API exception]: " + ex.Message);
                return null;
            }
        }

        public async Task<List<Models.Form>> GetFormsAsync() {
            var uri = new Uri(string.Format(Constants.RestUrl, "forms"));

            try {
                var response = await _client.GetAsync(uri);
                List<Models.Form> forms = new List<Models.Form>();

                if (response.IsSuccessStatusCode) {
                    var content = await response.Content.ReadAsStringAsync();
                    Debug.WriteLine("[Form API] - Forms: " + content);
                    forms = FormParser.GetFormsParser(content);
                }

                return forms;
            } catch (Exception ex) {
                Debug.WriteLine("[Coordinator API exception]:" + ex.Message);
                return null;
            }
        }

        public async Task<bool> PostFormAsync (ViewModels.NewFormViewModel form) {
            var uri = new Uri(string.Format(Constants.RestUrl, "forms"));

            var body = FormParser.PostFormbuilder(form);

            var content = new StringContent(body.ToString(), Encoding.UTF8, "application/json");
            var contentString = await content.ReadAsStringAsync();
            Debug.WriteLine("[Form API] - content built: " + contentString);

            try {
                var response = await _client.PostAsync(uri, content);
                if (response.IsSuccessStatusCode) {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    Debug.WriteLine("[Form API] - Post result: " + responseContent);
                    return true;
                } else {
                    var failedContent = await response.Content.ReadAsStringAsync();
                    Debug.WriteLine("[Form API] - Post response unsuccessful " + failedContent);
                    return false;
                }
            } catch (Exception ex) {
                Debug.WriteLine("[Form API exception]:" + ex.Message);
                return false;
            }
        }
    }
}
