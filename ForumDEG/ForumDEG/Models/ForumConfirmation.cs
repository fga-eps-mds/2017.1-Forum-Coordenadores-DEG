using SQLite;

namespace ForumDEG.Models {
    public class ForumConfirmation {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [Indexed]
        public int ForumId { get; set; }

        [Indexed]
        public int UserId { get; set; }
    }
}
