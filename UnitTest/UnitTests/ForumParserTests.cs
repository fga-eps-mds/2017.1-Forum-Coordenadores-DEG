using NUnit.Framework;
using ForumDEG.Models;
using ForumDEG.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using System.Diagnostics;

namespace Tests {
    class ForumParserTests {
        public static string content = "{\"date\":\"2017-05-20T00:00:00.000Z\",\"hour\":43200,\"id\":\"b3153003-b444-4d94-b4a5-b3b749ae6726\",\"place\":\"Na casa da árvore\",\"schedules\":\"O forum será sobre a grande obra prima de adventure time e a terra de Ooo\",\"theme\":\"Adventure Time\"}";
        public static string contentList = "[" + content + "," + content + "]";

        public static string theme = "Adventure Time";
        public static string place = "Na casa da árvore";
        public static string schedules = "O forum será sobre a grande obra prima de adventure time e a terra de Ooo";
        public static TimeSpan hour = TimeSpan.FromSeconds(43200);
        public static DateTime date = DateTime.Parse("2017-05-20 00:00:00.000");

        public static string newTheme = "New Theme";
        public static string newPlace = "New Place";
        public static string newSchedules = "New Schedules";
        public static TimeSpan newHour = TimeSpan.FromSeconds(0);
        public static DateTime newDate = DateTime.Parse("2017-06-10 00:00:00.000");

        [Test()]
        public void GetForumParser_WhenCalled_ShouldCreateForumWithCorrectProperties() {
            Forum forum = ForumParser.GetForumParser(content, Constants.ForumId);

            Assert.AreEqual(theme, forum.Title);
            Assert.AreEqual(place, forum.Place);
            Assert.AreEqual(schedules, forum.Schedules);
            Assert.AreEqual(hour, forum.Hour);
            Assert.AreEqual(date, forum.Date);
        }

        [Test()]
        public void GetForumsParser_WhenCalled_ShouldCreateListWithCorrectAmountOfForums() {
            List<Forum> forums = ForumParser.GetForumsParser(contentList);

            var listSize = forums.Count;

            Assert.AreEqual(2, listSize);
        }

        [Test()]
        public void PostForumBuilder_WhenCalled_ShouldCreateObjectWithCorrectData() {
            Forum forum = new Forum {
                Title = theme,
                Place = place,
                Schedules = schedules,
                Date = date,
                Hour = hour
            };

            JObject obj = ForumParser.PostForumBuilder(forum);

            var forumBody = obj["forum"];

            var forumTheme = forumBody["theme"].ToString();
            var forumPlace = forumBody["place"].ToString();
            var forumSchedules = forumBody["schedules"].ToString();
            var forumDate = forumBody["date"].ToObject<DateTime>();
            int seconds = forumBody["hour"].ToObject<int>();
            TimeSpan forumHour = TimeSpan.FromSeconds(seconds);

            Assert.AreEqual(theme, forumTheme);
            Assert.AreEqual(place, forumPlace);
            Assert.AreEqual(schedules, forumSchedules);
            Assert.AreEqual(date, forumDate);
            Assert.AreEqual(hour, forumHour);
        }

        [Test()]
        public void PutForumBuilder_AllFieldsChanged() {
            Forum oldForum = new Forum {
                Title = theme,
                Place = place,
                Schedules = schedules,
                Date = date,
                Hour = hour
            };

            Forum newForum = new Forum {
                Title = newTheme,
                Place = newPlace,
                Schedules = newSchedules,
                Date = newDate,
                Hour = newHour
            };

            JObject obj = ForumParser.PutForumBuilder(oldForum, newForum);

            var forumBody = obj["forum"];

            var forumTheme = forumBody["theme"].ToString();
            var forumPlace = forumBody["place"].ToString();
            var forumSchedules = forumBody["schedules"].ToString();
            var forumDate = forumBody["date"].ToObject<DateTime>();
            int seconds = forumBody["hour"].ToObject<int>();
            TimeSpan forumHour = TimeSpan.FromSeconds(seconds);

            Assert.AreEqual(newTheme, forumTheme);
            Assert.AreEqual(newPlace, forumPlace);
            Assert.AreEqual(newSchedules, forumSchedules);
            Assert.AreEqual(newDate, forumDate);
            Assert.AreEqual(newHour, forumHour);
        }

        [Test()]
        public void PutForumBuilder_OneFieldChanged() {
            Forum oldForum = new Forum {
                Title = theme,
                Place = place,
                Schedules = schedules,
                Date = date,
                Hour = hour
            };

            Forum newForum = new Forum {
                Title = newTheme,
                Hour = hour, // using old values to stop autosetting
                Date = date
            };

            JObject obj = ForumParser.PutForumBuilder(oldForum, newForum);

            var forumBody = obj["forum"];
            Debug.WriteLine(forumBody.ToString());
            var children = forumBody.Count();

            var forumTheme = forumBody["theme"].ToString();

            Assert.AreEqual(newTheme, forumTheme);
            Assert.AreEqual(1, children);
        }

        [Test()]
        public void PutForumBuilder_SomeFieldsChanged() {
            Forum oldForum = new Forum {
                Title = theme,
                Place = place,
                Schedules = schedules,
                Date = date,
                Hour = hour
            };

            Forum newForum = new Forum {
                Title = newTheme,
                Hour = newHour, 
                Date = newDate
            };

            JObject obj = ForumParser.PutForumBuilder(oldForum, newForum);

            var forumBody = obj["forum"];
            Debug.WriteLine(forumBody.ToString());
            var children = forumBody.Count();

            var forumTheme = forumBody["theme"].ToString();
            var forumDate = forumBody["date"].ToObject<DateTime>();
            int seconds = forumBody["hour"].ToObject<int>();
            TimeSpan forumHour = TimeSpan.FromSeconds(seconds);

            Assert.AreEqual(newTheme, forumTheme);
            Assert.AreEqual(newDate, forumDate);
            Assert.AreEqual(newHour, forumHour);
            Assert.AreEqual(3, children);
        }

        [Test()]
        public void PutForumBuilder_NoFieldsChanged() {
            Forum oldForum = new Forum {
                Title = theme,
                Place = place,
                Schedules = schedules,
                Date = date,
                Hour = hour
            };

            Forum newForum = new Forum {
                Hour = hour, // using old values to stop autosetting
                Date = date
            };

            JObject obj = ForumParser.PutForumBuilder(oldForum, newForum);

            var forumBody = obj["forum"];
            Debug.WriteLine(forumBody.ToString());
            var children = forumBody.Count();

            Assert.AreEqual(0, children);
        }
    }
}
