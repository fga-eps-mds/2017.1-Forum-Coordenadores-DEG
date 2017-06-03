using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ForumDEG.Models;

namespace ForumDEG.Helpers {
    public class FormParser {
        public static Models.Form GetFormParser(string content, string id) {
            var obj = JObject.Parse(content);

            var result = obj["result"];
            string title = result["title"].ToString();
            Debug.WriteLine("[Form Parser] - GET - Title: " + title);

            List<MultipleChoiceQuestion> multipleChoiceQuestions = new List<MultipleChoiceQuestion>();
            List<DiscursiveQuestion> discursiveQuestions = new List<DiscursiveQuestion>();

            if (result["multipleChoices"] != null) {
                JArray multipleChoices = (JArray)result["multipleChoices"];
                Debug.WriteLine("[Form Parser] - GET - Multiple Choices: " + multipleChoices.ToString());

                foreach (JObject question in multipleChoices) {
                    string questionTitle = question["question"].ToString();
                    bool multipleAnswers = question["multiple_anwsers"].ToObject<bool>();
                    Debug.WriteLine("[Form Parser] - GET - Multiple answers: " + multipleAnswers.ToString());
                    List<string> optionsList = new List<string>();

                    JArray options = (JArray)question["options"];
                    foreach (string option in options) {
                        optionsList.Add(option);
                        Debug.WriteLine("[Form Parser] - GET - Option: " + option);
                    }
                    Debug.WriteLine("[Form Parser] - GET - Question Title: " + questionTitle);
                    multipleChoiceQuestions.Add(new MultipleChoiceQuestion {
                        Question = questionTitle,
                        MultipleAnswers = multipleAnswers,
                        Options = optionsList
                    });
                }
            }
            
            if (result["discussive"] != null) {
                JArray discursives = (JArray)result["discussive"];

                foreach (JObject question in discursives) {
                    Debug.WriteLine("[Form Parser] - GET - Discursive: " + question);
                    var questionTitle = question["question"].ToString();
                    discursiveQuestions.Add(new DiscursiveQuestion {
                        Question = questionTitle
                    });
                }
            }

            Models.Form newForm = new Models.Form {
                Title = title,
                MultipleChoiceQuestions = multipleChoiceQuestions,
                DiscursiveQuestions = discursiveQuestions,
                RemoteId = id
            };

            return newForm;
        }
        public static List<Models.Form> GetFormsParser(string content) {
            List<Models.Form> forms = new List<Models.Form>();
            var formsArray = JArray.Parse(content);

            foreach (JObject form in formsArray) {
                string title = form["title"].ToString();
                string id = form["id"].ToString();
                Debug.WriteLine("[Form Parser] - GET - Title: " + title);

                List<MultipleChoiceQuestion> multipleChoiceQuestions = new List<MultipleChoiceQuestion>();
                List<DiscursiveQuestion> discursiveQuestions = new List<DiscursiveQuestion>();

                if (form["multipleChoices"] != null) {
                    JArray multipleChoices = (JArray)form["multipleChoices"];
                    Debug.WriteLine("[Form Parser] - GET - Multiple Choices: " + multipleChoices.ToString());

                    foreach (JObject question in multipleChoices) {
                        string questionTitle = question["question"].ToString();
                        bool multipleAnswers = question["multiple_anwsers"].ToObject<bool>();
                        Debug.WriteLine("[Form Parser] - GET - Multiple answers: " + multipleAnswers.ToString());
                        List<string> optionsList = new List<string>();

                        JArray options = (JArray)question["options"];
                        foreach (string option in options) {
                            optionsList.Add(option);
                            Debug.WriteLine("[Form Parser] - GET - Option: " + option);
                        }
                        Debug.WriteLine("[Form Parser] - GET - Question Title: " + questionTitle);
                        multipleChoiceQuestions.Add(new MultipleChoiceQuestion {
                            Question = questionTitle,
                            MultipleAnswers = multipleAnswers,
                            Options = optionsList
                        });
                    }
                }

                if (form["discussive"] != null) {
                    JArray discursives = (JArray)form["discussive"];

                    foreach (JObject question in discursives) {
                        Debug.WriteLine("[Form Parser] - GET - Discursive: " + question);
                        var questionTitle = question["question"].ToString();
                        discursiveQuestions.Add(new DiscursiveQuestion {
                            Question = questionTitle
                        });
                    }
                }

                forms.Add(new Models.Form {
                    Title = title,
                    MultipleChoiceQuestions = multipleChoiceQuestions,
                    DiscursiveQuestions = discursiveQuestions,
                    RemoteId = id
                });
            }

            return forms;
        }
        public static JObject PostFormbuilder(ViewModels.NewFormViewModel form) {

            JObject body = new JObject();
            JObject formContent = new JObject(); 
            JArray multipleChoice = new JArray();
            JArray discursive = new JArray();

            var multipleChoices = form.MultipleChoiceQuestions;
            formContent.Add("title", form.Title);
            foreach ( ViewModels.QuestionDetailViewModel question  in multipleChoices){
                JObject multipleChoiceQuestion = new JObject();
                JArray multipleChoiceOptions = new JArray();              
                multipleChoiceQuestion.Add("multiple_anwsers", question.MultipleAnswers);
                foreach (string option in question.Options) {
                    multipleChoiceOptions.Add(option);
                }
                multipleChoiceQuestion.Add("options", multipleChoiceOptions);
                multipleChoiceQuestion.Add("question", question.Title);

                multipleChoice.Add(multipleChoiceQuestion);
                Debug.WriteLine("[Form API] - Parser 1 " + multipleChoice.ToString());

            }
            Debug.WriteLine("[Form API] - Parser 2 " + multipleChoice.ToString());

            var discursiveQuestions = form.DiscursiveQuestionsTitles;
            foreach (string question in discursiveQuestions) {
                JObject discursiveQuestion = new JObject();
                discursiveQuestion.Add("question", question);

                discursive.Add(discursiveQuestion);
            }

            formContent.Add("multipleChoices", multipleChoice);
            formContent.Add("discussive", discursive);
            body.Add("form", formContent);

            return body;
        }
    }
}
