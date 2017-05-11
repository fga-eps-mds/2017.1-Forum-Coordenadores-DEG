using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ForumDEG;
using System.Diagnostics;
using Newtonsoft.Json.Linq;

namespace ForumDEG.Helpers {
    class Forum {
        private HttpClient _client;

        public Forum() {
            _client = new HttpClient();
            _client.MaxResponseContentBufferSize = 256000;
        }

        public async Task<Models.Forum> GetForumAsync(string id) {
            Debug.WriteLine("Inside getForumAsync");
            var uri = new Uri(string.Format(Constants.RestUrl, "forums/" + id));

            try {
                var response = await _client.GetAsync(uri);
                Debug.WriteLine("[Forum API] got response");
                if (response.IsSuccessStatusCode) {
                
                    var content = await response.Content.ReadAsStringAsync();
                    Debug.WriteLine("[Forum API]: " + content);

                    var obj = JObject.Parse(content);

                    string title = obj["theme"].ToString();
                    string place = obj["place"].ToString();
                    string schedules = obj["schedules"].ToString();
                    DateTime date = obj["date"].ToObject<DateTime>();
                    int seconds = obj["hour"].ToObject<int>();
                    TimeSpan hour = TimeSpan.FromSeconds(seconds);

                    Debug.WriteLine("[Forum API]: Forum theme:" + title);
                    Debug.WriteLine("[Forum API]: Forum place:" + place);
                    Debug.WriteLine("[Forum API]: Forum schedules:" + schedules);
                    Debug.WriteLine("[Forum API]: Forum date:" + date.ToString());
                    Debug.WriteLine("[Forum API]: Forum hour:" + hour.ToString(@"hh\:mm\:ss"));

                    Models.Forum forum = new Models.Forum {
                        Title = title,
                        Place = place,
                        Schedules = schedules,
                        Date = date,
                        Hour = hour
                    };

                    return forum;
                }
                return null;
            } catch (Exception ex) {
                Debug.WriteLine("[Forum API exception]:" + ex.Message);
                return null;
            }
        }
    }
}
