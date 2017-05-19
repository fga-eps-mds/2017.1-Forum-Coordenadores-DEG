using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumDEG.Helpers {
    public class FormParser {
        public static JObject PostFormbuilder(ViewModels.NewFormViewModel form) {

            JObject body = new JObject();
            JObject formContent = new JObject(); 
            JArray multipleChoice = new JArray();


            var multipleChoices = form.MultipleChoiceQuestions;
            foreach ( ViewModels.QuestionDetailViewModel question  in multipleChoices){
                JObject multipleChoiceQuestion = new JObject();
                JArray multipleChoiceOptions = new JArray();

                foreach (string option in question.Options) {
                    multipleChoiceOptions.Add(option);
                }
                multipleChoiceQuestion.Add("options", multipleChoiceOptions);
                multipleChoiceQuestion.Add("question", question.Title);

                multipleChoice.Add(multipleChoiceQuestion);
                Debug.WriteLine("[Form API] - Parser 1 " + multipleChoice.ToString());

            }
            Debug.WriteLine("[Form API] - Parser 2 " + multipleChoice.ToString());

            formContent.Add("multipleChoices", multipleChoice);
            body.Add("form", formContent);

            return body;
        }
    }
}
