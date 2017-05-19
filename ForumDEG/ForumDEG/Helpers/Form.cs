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
