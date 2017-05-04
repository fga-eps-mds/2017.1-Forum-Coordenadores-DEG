using SQLite;

namespace ForumDEG.Models {
    public class ForumConfirmation {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [Indexed, Unique]
        public int ForumId { get; set; }

        [Indexed, Unique]
        public int UserId { get; set; }
    }
}
