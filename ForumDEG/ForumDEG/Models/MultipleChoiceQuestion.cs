using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumDEG.Models {
    public class MultipleChoiceQuestion {
        public string Question { get; set; }
        public List<string> Options { get; set; }
        public bool MultipleAnswers { get; set; }
    }
}
