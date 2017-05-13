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
                    return ForumParser.GetForumParser(content, id);
                }

                return null;
            } catch (Exception ex) {
                Debug.WriteLine("[Forum API exception]:" + ex.Message);
                return null;
            }
        }

        public async Task<List<Models.Forum>> GetForumsAsync() {
            var uri = new Uri(string.Format(Constants.RestUrl, "forums"));

            try {
                var response = await _client.GetAsync(uri);
                List<Models.Forum> forums = new List<Models.Forum>();

                if (response.IsSuccessStatusCode) {
                    var content = await response.Content.ReadAsStringAsync();
                    Debug.WriteLine("[Forum API] - Forums: " + content);
                    forums = ForumParser.GetForumsParser(content);
                }
                return forums;
            } catch (TaskCanceledException) {
                throw;
            } catch (Exception ex) {
                Debug.WriteLine("[Forum API exception]:" + ex.Message);
                return null;
            }
        }

        public async Task<bool> PostForumAsync(Models.Forum forum) {
            var uri = new Uri(string.Format(Constants.RestUrl, "forums"));

            var body = ForumParser.PostForumBuilder(forum);

            Debug.WriteLine("[Forum API] - preparing to build content");

            var content = new StringContent(body.ToString(), Encoding.UTF8, "application/json");
            var contentString = await content.ReadAsStringAsync();
            Debug.WriteLine("[Forum API] - content built: " + contentString);

            try {
                var response = await _client.PostAsync(uri, content);
                if (response.IsSuccessStatusCode) {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    Debug.WriteLine("[Forum API] - Post result: " + responseContent);
                    return true;
                } else {
                    var failedContent = await response.Content.ReadAsStringAsync();
                    Debug.WriteLine("[Forum API] - Post response unsuccessful " + failedContent);
                    return false;
                }
            } catch (Exception ex) {
                Debug.WriteLine("[Forum API exception]:" + ex.Message);
                return false;
            }
        }

        public async Task<bool> PutForumAsync(string id, Models.Forum newForum) {
            var uri = new Uri(string.Format(Constants.RestUrl, "forums/" + id));
            var oldForum = await GetForumAsync(id);

            var body = ForumParser.PutForumBuilder(oldForum, newForum);

            var content = new StringContent(body.ToString(), Encoding.UTF8, "application/json");
            var contentString = await content.ReadAsStringAsync();

            try {
                var response = await _client.PutAsync(uri, content);
                if (response.IsSuccessStatusCode) {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    Debug.WriteLine("[Forum API] - Put result: " + responseContent);
                    return true;
                } else {
                    var failedContent = await response.Content.ReadAsStringAsync();
                    Debug.WriteLine("[Forum API] - Put response unsuccessful " + failedContent);
                    return false;
                }
            } catch (Exception ex) {
                Debug.WriteLine("[Forum API exception]:" + ex.Message);
                return false;
            }
        }

        public async Task<bool> DeleteForumAsync(string id) {
            var uri = new Uri(string.Format(Constants.RestUrl, "forums/" + id));
            var emptyBody = new StringContent(""); // for some reason can't send request without body

            try {
                var response = await _client.DeleteAsync(uri);

                if (response.IsSuccessStatusCode) {
                    var content = await response.Content.ReadAsStringAsync();
                    Debug.WriteLine("[Forum API] - Delete result: " + content);
                    return true;
                } else {
                    var failedContent = await response.Content.ReadAsStringAsync();
                    Debug.WriteLine("[Forum API] - Delete response unsuccessful " + failedContent);
                    return false;
                }
            } catch (Exception ex) {
                Debug.WriteLine("[Forum API exception]:" + ex.Message);
                return false;
            }
        }
    }
}
