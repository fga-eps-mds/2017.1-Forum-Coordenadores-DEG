using SQLite;
using System;

namespace ForumDEG.Models
{
    public class Forum
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public DateTime CreatedOn { get; set; }

        public string Room { get; set; }

        public string Building { get; set; }

        public string ForumTheme { get; set; }
    }
}
