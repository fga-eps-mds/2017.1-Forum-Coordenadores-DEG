using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ForumDEG.ViewModels;
using ForumDEG.Helpers;
using Moq;
using ForumDEG.Interfaces;
using Newtonsoft.Json.Linq;
using System.Diagnostics;
using System.Net.Http;

namespace Tests {
    class FormParserTests {
        private NewFormViewModel form;
        private Mock<IPageService> _pageService;
        private QuestionDetailViewModel _question1;
        private QuestionDetailViewModel _question2;

        private static string content = "{\"discussive\":[{\"question\":\"Chama, chama, chama\"}],\"id\":\"3\",\"multipleChoices\":[{\"multiple_anwsers\":false,\"options\":[\"fun\",\"ci\",\"o\",\"na\"],\"question\":\"Funcionaaa\"},{\"multiple_anwsers\":true,\"options\":[\"uma\",\"duas\"],\"question\":\"lalala\"}],\"title\":\"Form que vai dar bom\"}";
        private static string contentList = "[" + content + "," + content+"]";
        private static string title = "Form que vai dar bom";
        private static string id = "3";
        private static List<ForumDEG.Models.DiscursiveQuestion> discursive;
        private static List<ForumDEG.Models.MultipleChoiceQuestion> multiplechoice;

        [SetUp()]
        public void SetUp() {
            _pageService = new Mock<IPageService>();
            form = new NewFormViewModel(_pageService.Object);

            discursive = new List<ForumDEG.Models.DiscursiveQuestion>();
            discursive.Add(new ForumDEG.Models.DiscursiveQuestion { Question = "Chama, chama, chama" });

            multiplechoice = new List<ForumDEG.Models.MultipleChoiceQuestion>();
            multiplechoice.Add(new ForumDEG.Models.MultipleChoiceQuestion {
                MultipleAnswers = false,
                Question = "Funcionaaa",
                Options = new List<string> { "fun", "ci", "o", "na" }
            });
            multiplechoice.Add(new ForumDEG.Models.MultipleChoiceQuestion {
                MultipleAnswers = true,
                Question = "lalala",
                Options = new List<string> { "uma", "duas"}
            });

            form.Title = "Titulo";
            _question1 = new QuestionDetailViewModel {
                Title = "pergunta1",
                MultipleAnswers = true
            };
            _question1.Options.Add("reposta1");
            _question1.Options.Add("reposta2");

            _question2 = new QuestionDetailViewModel {
                Title = "pergunta2",
                MultipleAnswers = false
            };
            _question2.Options.Add("reposta1");
            _question2.Options.Add("reposta2");
            form.MultipleChoiceQuestions.Add(_question1);
            form.MultipleChoiceQuestions.Add(_question2);
        }

        [Test()]
        public void GetFormParser_WhenCalled_CreateListWithCorrectAmountOfForm() {
            List<ForumDEG.Models.Form> test = FormParser.GetFormsParser(contentList);

            var listSize = test.Count;

            Assert.AreEqual(2, listSize);
        }

        [Test()]
        public void GetFormParser_WhenCalled_CreateFormWithCorrectProperties() {
            ForumDEG.Models.Form test = FormParser.GetFormParser("{\"result\":"+content+"}", "3");

            Assert.AreEqual(title, test.Title);
            Assert.AreEqual(id, test.RemoteId);
           for(int i = 0; i < discursive.Count(); i++) {
                Assert.AreEqual(discursive[i].Question, test.DiscursiveQuestions[i].Question);
            }
            for (int i = 0; i < discursive.Count(); i++) {
                Assert.AreEqual(multiplechoice[i].Question, test.MultipleChoiceQuestions[i].Question);
                Assert.AreEqual(multiplechoice[i].MultipleAnswers, test.MultipleChoiceQuestions[i].MultipleAnswers);
                Assert.AreEqual(multiplechoice[i].Options, test.MultipleChoiceQuestions[i].Options);
            }
        }

        [Test()]
        public void PostFormbuilder_Title() {

            JObject obj = FormParser.PostFormbuilder(form);

            var formBody = obj["form"];

            var formTitle = formBody["title"].ToString();;

            Assert.AreEqual(form.Title, formTitle);
        }

        [Test()]
        public void PostFormbuilder_MultipleAnwsers() {

            JObject obj = FormParser.PostFormbuilder(form);

            var formBody = obj["form"];

            List<bool> multipleAnswersList = new List<bool>();


            var questions = formBody["multipleChoices"].ToObject<List<JObject>>();

            multipleAnswersList.Add(questions[0]["multiple_anwsers"].ToObject<bool>());
            multipleAnswersList.Add(questions[1]["multiple_anwsers"].ToObject<bool>());

            Assert.AreEqual(_question1.MultipleAnswers,multipleAnswersList[0]);
            Assert.AreEqual(_question2.MultipleAnswers, multipleAnswersList[1]);
        }

        [Test()]
        public void PostFormbuilder_Question() {

            JObject obj = FormParser.PostFormbuilder(form);

            var formBody = obj["form"];

            List<string> questionList = new List<string>();


            var questions = formBody["multipleChoices"].ToObject<List<JObject>>();

            questionList.Add(questions[0]["question"].ToString());
            questionList.Add(questions[1]["question"].ToString());

            Assert.AreEqual(_question1.Title, questionList[0]);
            Assert.AreEqual(_question2.Title, questionList[1]);
        }

        [Test()]
        public void PostFormbuilder_Options() {

            JObject obj = FormParser.PostFormbuilder(form);

            var formBody = obj["form"];

            List<List<string>> questionList = new List<List<string>>();


            var questions = formBody["multipleChoices"].ToObject<List<JObject>>();

            questionList.Add(questions[0]["options"].ToObject<List<string>>());
            questionList.Add(questions[1]["options"].ToObject<List<string>>());

            Assert.AreEqual(_question1.Options, questionList[0]);
            Assert.AreEqual(_question2.Options, questionList[1]);
        }
    }



}
