using SQLite;
using System.Collections.Generic;
using System.Linq;

namespace ForumDEG.Models {
    public class FormAnswer {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [Indexed]
        public int FormAskId { get; set; }

        [Indexed]
        public int UserId { get; set; }

        public string TextAnswer { get; set; }

        public int OptionAnswerPosition { get; set; }

        public string MultipleAnswerPositions { get; set; }

        // Override ToString to debug database.
        /* Usage:
         * Debug.WriteLine(form.ToString());
        */
        public override string ToString() {

            return string.Format("[FormAnswers: ID={0}, FormAskId={1}, UserId={2}, TextAnswer={3}, OptionAnswerPosition={4}, MultipleAnswerPositions={5}]", Id, FormAskId, UserId, TextAnswer, OptionAnswerPosition, MultipleAnswerPositions);
        }
    }
}
