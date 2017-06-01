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

        [SetUp()]
        public void SetUp() {
            _pageService = new Mock<IPageService>();
            form = new NewFormViewModel(_pageService.Object);

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

            multipleAnswersList.Add(questions[0]["multiple-anwsers"].ToObject<bool>());
            multipleAnswersList.Add(questions[1]["multiple-anwsers"].ToObject<bool>());

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
