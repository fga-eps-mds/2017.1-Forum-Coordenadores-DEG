using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumDEG.Models {
    public class MultipleChoiceAnswer {
        public string Question { get; set; }
        public List<string> Answers { get; set; }
    }
}
