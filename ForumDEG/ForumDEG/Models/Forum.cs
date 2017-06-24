﻿using SQLite;
using System;

namespace ForumDEG.Models {
    public class Forum {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public DateTime CreatedOn { get; set; }

        public string Title { get; set; }

        public string Place { get; set; }

        public string Schedules { get; set; }

        public DateTime Date { get; set; }

        public TimeSpan Hour { get; set; }

        public string RemoteId { get; set; } // hopefully temporary fix

        public int Confirmations { get; set; }
    }
}
