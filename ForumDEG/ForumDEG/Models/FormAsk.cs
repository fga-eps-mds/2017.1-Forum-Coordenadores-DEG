using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ForumDEG.Models {
    public class FormAsk {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [Indexed]
        public int FormId { get; set; }

        // 1 = Multiple choices; 2 = Multiple Answers; 3 = Text
        public int AskType { get; set; }

        public String Options { get; set; }

        // Override ToString to debug database.
        /* Usage:
         * Debug.WriteLine(form.ToString());
        */
        public override string ToString() {

            return string.Format("[ForumAsks: ID={0}, FormId={1}, AskType={2}, Options={3}]", Id, FormId, AskType, Options);
        }
    }
}
