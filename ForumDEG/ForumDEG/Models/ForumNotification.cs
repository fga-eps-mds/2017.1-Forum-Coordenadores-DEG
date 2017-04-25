using SQLite;
using System;

namespace ForumDEG.Models {
    public class ForumNotification {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [Indexed]
        public int ForumId { get; set; }

        public String Notification { get; set; }

        // Override ToString to debug database.
        /* Usage:
         * Debug.WriteLine(forumNotification.ToString());
        */
        public override string ToString() {
            return string.Format("[Form: ID={0}, ForumID={1}, Notification={2}]", Id, ForumId, Notification);
        }
    }
}
