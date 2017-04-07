using SQLite;
using System;

namespace ForumDEG.Models
{
    public class User {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Registration { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
