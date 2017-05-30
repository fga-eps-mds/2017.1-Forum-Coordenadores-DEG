using SQLite;
using System;
using System.Collections.Generic;

namespace ForumDEG.Models
{
    public class Form {
        public int Id { get; set; }
        public string RemoteId { get; set; }
        public string Title { get; set; }
        public List<DiscursiveQuestion> DiscursiveQuestions { get; set; }
        public List<MultipleChoiceQuestion> MultipleChoiceQuestions { get; set; }
    }
}
