using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumDEG.Helpers {
    public class ForumParser {
        public static Models.Forum GetForumParser(string content, string id) {
            var obj = JObject.Parse(content);
            
            string title = obj["theme"].ToString();
            string place = obj["place"].ToString();
            string schedules = obj["schedules"].ToString();
            DateTime date = obj["date"].ToObject<DateTime>();
            int seconds = obj["hour"].ToObject<int>();
            TimeSpan hour = TimeSpan.FromSeconds(seconds);
            string remoteId = obj["id"].ToString();

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
                Hour = hour,
                RemoteId = remoteId,
            };

            return forum;
        }

        public static List<Models.Forum> GetForumsParser(string content) {
            List<Models.Forum> forums = new List<Models.Forum>();
            var objArray = JArray.Parse(content);

            foreach (JObject obj in objArray) {
                int confirmations = 0;
                if (obj["coordinators"] != null) {
                    confirmations = obj["coordinators"].ToObject<int>();
                }
                string title = obj["theme"].ToString();
                string place = obj["place"].ToString();
                string schedules = obj["schedules"].ToString();
                DateTime date = obj["date"].ToObject<DateTime>();
                int seconds = obj["hour"].ToObject<int>();
                TimeSpan hour = TimeSpan.FromSeconds(seconds);
                string remoteId = obj["id"].ToString();

                Debug.WriteLine("[Forum API]: Forum theme:" + title);
                Debug.WriteLine("[Forum API]: Forum place:" + place);
                Debug.WriteLine("[Forum API]: Forum schedules:" + schedules);
                Debug.WriteLine("[Forum API]: Forum date:" + date.ToString());
                Debug.WriteLine("[Forum API]: Forum hour:" + hour.ToString(@"hh\:mm\:ss"));

                forums.Add(new Models.Forum {
                    Title = title,
                    Place = place,
                    Schedules = schedules,
                    Date = date,
                    Hour = hour,
                    RemoteId = remoteId,
                    Confirmations = confirmations
                });
            }

            return forums;
        }

        public static JObject PostForumBuilder(Models.Forum forum) {
            var theme = forum.Title;
            var place = forum.Place;
            var schedules = forum.Schedules;
            var date = forum.Date;
            var hour = forum.Hour.TotalSeconds;

            var forumContents = new JObject();
            forumContents.Add("theme", theme);
            forumContents.Add("place", place);
            forumContents.Add("schedules", schedules);
            forumContents.Add("date", date);
            forumContents.Add("hour", hour);

            var body = new JObject();
            body.Add("forum", forumContents);

            return body;
        }

        public static JObject PutForumBuilder(Models.Forum oldForum, Models.Forum newForum) {
            var forumData = new JObject();

            if (oldForum.Title != newForum.Title && !String.IsNullOrEmpty(newForum.Title)) {
                var theme = newForum.Title;
                forumData.Add("theme", theme);
            }

            if (oldForum.Place != newForum.Place && !String.IsNullOrEmpty(newForum.Place)) {
                var place = newForum.Place;
                forumData.Add("place", place);
            }

            if (oldForum.Schedules != newForum.Schedules && !String.IsNullOrEmpty(newForum.Schedules)) {
                var schedules = newForum.Schedules;
                forumData.Add("schedules", schedules);
            }

            if (oldForum.Date != newForum.Date && newForum.Date != null) {
                var date = newForum.Date;
                forumData.Add("date", date);
            }

            if (oldForum.Hour != newForum.Hour && newForum.Hour != null) {
                var hour = newForum.Hour.TotalSeconds;
                forumData.Add("hour", hour);
            }

            var body = new JObject();
            body.Add("forum", forumData);

            return body;
        }
    }
}
