using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumDEG.Models {
    public class MultipleAnswersQuestion : ObservableCollection<Option> {
        public string Question { get; set; }

        public MultipleAnswersQuestion(string question) {
            Question = question;
        }
    }
}
