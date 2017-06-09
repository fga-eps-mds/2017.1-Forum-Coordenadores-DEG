using SQLite;
using System.Collections.Generic;
using System.Linq;

namespace ForumDEG.Models {
    public class FormAnswer {
        public string FormId { get; set; }
        public string CoordinatorId { get; set; }
        public List<DiscursiveQuestion> DiscursiveAnswers { get; set; }
        public List<MultipleChoiceAnswer> MultipleChoiceAnswers { get; set; }
    }
}
