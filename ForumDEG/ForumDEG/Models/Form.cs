using SQLite;
using System;

namespace ForumDEG.Models
{
    public class Form {
        [PrimaryKey, AutoIncrement, Unique]
        public int Id { get; set; }

        [Indexed]
        public int ForumId { get; set; }

        public DateTime CreatedOn { get; set; }

        // Override ToString to debug database.
        /* Usage:
         * Debug.WriteLine(form.ToString());
        */
        public override string ToString() {
            return string.Format("[Form: ID={0}, ForumID={1}, CreatedOn={2}]", Id, ForumId, CreatedOn);
        }
    }
}
