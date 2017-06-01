using ForumDEG.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumDEG.ViewModels {
    public class FormDetailViewModel {
        public int Id { get; set; }
        public string RemoteId { get; set; }
        public string Title { get; set; }
        public List<DiscursiveQuestion> DiscursiveQuestions { get; set; }
        public List<MultipleChoiceQuestion> MultipleChoiceQuestions { get; set; }

        public int QuestionsAmount {
            get { return (DiscursiveQuestions.Count + MultipleChoiceQuestions.Count); }
        }
    }
}
