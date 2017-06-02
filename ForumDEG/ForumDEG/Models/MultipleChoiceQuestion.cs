using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumDEG.Models {
    public class MultipleChoiceQuestion : ObservableCollection<Option> {
        public string Question { get; set; }
        public bool MultipleAnswers { get; set; }

        public MultipleChoiceQuestion(string question, bool multipleAnswers) {
            Question = question;
            MultipleAnswers = multipleAnswers;
        }
    }
}
