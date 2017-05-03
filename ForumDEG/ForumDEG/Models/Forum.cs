﻿using SQLite;
using System;
using System.Collections.Generic;

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

        // Current coordinator that confirmed to a forum
        public int UserIdConfirmed { get; set; }
    }
}
