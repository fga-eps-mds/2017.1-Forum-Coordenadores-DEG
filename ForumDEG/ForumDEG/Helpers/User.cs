using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ForumDEG.Helpers {
    class User {
        private HttpClient _client;



        public User() {

            _client = new HttpClient();

            _client.MaxResponseContentBufferSize = 256000;

        }

        public async Task<string> AuthenticateLogin(string _registration, string _password) {
            //var uri = new Uri(string.Format(Constants.RestUrl, "users/authenticate"));
            var uri = new Uri("https://forumdeg.herokuapp.com/api/users/authenticate");

            
            var registration = _registration;
            var password = _password;

            var body = new JObject();
            body.Add("password", password);
            body.Add("registration", registration);
            

            var content = new StringContent(body.ToString(), Encoding.UTF8, "application/json");
            //var contentString = await content.ReadAsStringAsync();
            Debug.WriteLine("[User API]: Antes do Try");

            try {

                var response = await _client.PostAsync(uri, content);
                Debug.WriteLine("[User API]: depois do response");
                if (response.IsSuccessStatusCode) {

                    var responseContent = await response.Content.ReadAsStringAsync();

                    Debug.WriteLine("[User API] - Post result: " + responseContent);

                    var _return = await response.Content.ReadAsStringAsync();
                    var obj = JObject.Parse(_return);
                    return obj["user"].ToString();

                } else {

                    var failedContent = await response.Content.ReadAsStringAsync();

                    Debug.WriteLine("[User API] - Post response unsuccessful "+ failedContent);

                    return "Fail";

                }

            } catch (Exception ex) {

                Debug.WriteLine("[User API exception]:" + ex.Message);

                return "Fail";

            }

        }
    }
}
