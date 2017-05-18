using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumDEG.ViewModels {
    public class QuestionDetailViewModel {
        public string Title { get; set; }
        public ObservableCollection<string> Options { get; set; }
        public bool MultipleAnswers { get; set; }

        public QuestionDetailViewModel() {
            Options = new ObservableCollection<string>();
        }
    }
}
