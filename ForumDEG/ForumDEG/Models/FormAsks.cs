using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumDEG.Models {
    public class FormAsks {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [Indexed]
        public int FormId { get; set; }

        [Indexed]
        public int UserId { get; set; }

        public int AskType { get; set; }

        public List<String> Asks { get; set; }

        public List<String> Answers { get; set; }

        // Override ToString to debug database.
        /* Usage:
         * Debug.WriteLine(form.ToString());
        */
        public override string ToString() {

            return string.Format("[ForumAsks: ID={0}, FormId={1}, UserId={2}, AskType={3}, Asks={4}, Answers={5}]", Id, FormId, UserId, AskType, Asks.ToList(), Answers.ToList());
        }
    }
}
