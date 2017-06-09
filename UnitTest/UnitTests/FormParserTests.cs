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
        private NewFormViewModel _form;
        private ForumDEG.Models.FormAnswer _formAnswer;
        private Mock<IPageService> _pageService;
        private QuestionDetailViewModel _question1;
        private QuestionDetailViewModel _question2;

        private static string content = "{\"discussive\":[{\"question\":\"Chama, chama, chama\"}],\"id\":\"3\",\"multipleChoices\":[{\"multiple_anwsers\":false,\"options\":[\"fun\",\"ci\",\"o\",\"na\"],\"question\":\"Funcionaaa\"},{\"multiple_anwsers\":true,\"options\":[\"uma\",\"duas\"],\"question\":\"lalala\"}],\"title\":\"Form que vai dar bom\"}";
        private static string contentList = "[" + content + "," + content+"]";
        private static string title = "Form que vai dar bom";
        private static string id = "3";
        private static List<ForumDEG.Models.DiscursiveQuestion> discursive;
        private static List<ForumDEG.Models.MultipleChoiceQuestion> multipleChoice;

        [SetUp()]
        public void SetUp() {
            _pageService = new Mock<IPageService>();
            _form = new NewFormViewModel(_pageService.Object);
            _formAnswer = new ForumDEG.Models.FormAnswer {
                CoordinatorId = "12345678",
                FormId = "0001",
                DiscursiveAnswers = new List<ForumDEG.Models.DiscursiveQuestion> {
                    new ForumDEG.Models.DiscursiveQuestion {
                        Question = "Questão Discursiva",
                        Answer = "Resposta Discursiva"
                    }
                },
                MultipleChoiceAnswers = new List<ForumDEG.Models.MultipleChoiceAnswer> {
                    new ForumDEG.Models.MultipleChoiceAnswer {
                        Question = "Questão Múltipla",
                        Answers = new List<string> { "Opção 1", "Opção 2" }
                    }
                }
            };

            discursive = new List<ForumDEG.Models.DiscursiveQuestion>();
            discursive.Add(new ForumDEG.Models.DiscursiveQuestion { Question = "Chama, chama, chama" });

            multipleChoice = new List<ForumDEG.Models.MultipleChoiceQuestion>();
            multipleChoice.Add(new ForumDEG.Models.MultipleChoiceQuestion("Funcionaaa", false) {
                new ForumDEG.Models.Option{
                    OptionText = "fun"
                },
                new ForumDEG.Models.Option{
                    OptionText = "ci"
                },
                new ForumDEG.Models.Option{
                    OptionText = "o"
                },
                new ForumDEG.Models.Option{
                    OptionText = "na"
                },
            });
            multipleChoice.Add(new ForumDEG.Models.MultipleChoiceQuestion("lalala", true) {
                new ForumDEG.Models.Option {
                    OptionText = "uma"
                },
                new ForumDEG.Models.Option {
                    OptionText = "duas"
                },
            });

            _form.Title = "Titulo";
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
            _form.MultipleChoiceQuestions.Add(_question1);
            _form.MultipleChoiceQuestions.Add(_question2);

            _form.DiscursiveQuestionsTitles.Add("Chama, chama, chama");
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
            for (int i = 0; i < multipleChoice.Count(); i++) {
                Assert.AreEqual(multipleChoice[i].Question, test.MultipleChoiceQuestions[i].Question);
                Assert.AreEqual(multipleChoice[i].MultipleAnswers, test.MultipleChoiceQuestions[i].MultipleAnswers);
                for (int j = 0; j < multipleChoice[i].Count; j++) {
                    Assert.AreEqual(multipleChoice[i][j].OptionText, test.MultipleChoiceQuestions[i][j].OptionText);
                }
            }
        }

        [Test()]
        public void PostFormbuilder_Title() {

            JObject obj = FormParser.PostFormbuilder(_form);

            var formBody = obj["form"];

            var formTitle = formBody["title"].ToString();;

            Assert.AreEqual(_form.Title, formTitle);
        }

        [Test()]
        public void PostFormbuilder_MultipleAnwsers() {

            JObject obj = FormParser.PostFormbuilder(_form);

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

            JObject obj = FormParser.PostFormbuilder(_form);

            var formBody = obj["form"];

            List<string> questionList = new List<string>();


            var questions = formBody["multipleChoices"].ToObject<List<JObject>>();

            questionList.Add(questions[0]["question"].ToString());
            questionList.Add(questions[1]["question"].ToString());

            Assert.AreEqual(_question1.Title, questionList[0]);
            Assert.AreEqual(_question2.Title, questionList[1]);
        }

        [Test()]
        public void FormParser_PostFormbuilder_DiscursiveQuestion() {

            JObject obj = FormParser.PostFormbuilder(_form);

            var formBody = obj["form"];
            var discursiveQuestions = formBody["discussive"].ToObject<List<JObject>>();

            var question = discursiveQuestions[0]["question"].ToString();

            Assert.AreEqual(_form.DiscursiveQuestionsTitles[0], question);
        }

        [Test()]
        public void PostFormbuilder_Options() {

            JObject obj = FormParser.PostFormbuilder(_form);

            var formBody = obj["form"];

            List<List<string>> questionList = new List<List<string>>();


            var questions = formBody["multipleChoices"].ToObject<List<JObject>>();

            questionList.Add(questions[0]["options"].ToObject<List<string>>());
            questionList.Add(questions[1]["options"].ToObject<List<string>>());

            Assert.AreEqual(_question1.Options, questionList[0]);
            Assert.AreEqual(_question2.Options, questionList[1]);
        }

        [Test()]
        public void FormParser_PostFormAnswerBuilder_Ids() {
            JObject obj = FormParser.PostFormAnswerBuilder(_formAnswer);

            var formBody = obj["formAnswer"];

            var formId = formBody["formId"].ToString();
            var coordId = formBody["coordinatorId"].ToString();

            Assert.AreEqual(_formAnswer.FormId, formId);
            Assert.AreEqual(_formAnswer.CoordinatorId, coordId);
        }

        [Test()]
        public void FormParser_PostFormAnswerBuilder_DiscursiveAnswers() {
            JObject obj = FormParser.PostFormAnswerBuilder(_formAnswer);

            var formBody = obj["formAnswer"];
            var discursiveAnswersList = formBody["discursiveAnswers"].ToObject<List<JObject>>();

            var discursiveQuestion = discursiveAnswersList[0]["question"].ToString();
            var discursiveAnswer = discursiveAnswersList[0]["answer"].ToString();

            Assert.AreEqual(_formAnswer.DiscursiveAnswers[0].Question, discursiveQuestion);
            Assert.AreEqual(_formAnswer.DiscursiveAnswers[0].Answer, discursiveAnswer);
        }

        [Test()]
        public void FormParser_PostFormAnswerBuilder_MultipleChoiceAnswers() {
            JObject obj = FormParser.PostFormAnswerBuilder(_formAnswer);

            var formBody = obj["formAnswer"];
            var multipleChoiceAnswersList = formBody["multipleChoiceAnswers"].ToObject<List<JObject>>();

            var questionTitle = multipleChoiceAnswersList[0]["question"].ToString();
            var optionsAnswered = multipleChoiceAnswersList[0]["answers"].ToObject<List<string>>();

            Assert.AreEqual(_formAnswer.MultipleChoiceAnswers[0].Question, questionTitle);
            Assert.AreEqual(_formAnswer.MultipleChoiceAnswers[0].Answers, optionsAnswered);
        }
    }



}
