﻿using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumDEG.Models {
    public class Forum {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public DateTime CreatedOn { get; set; }

        public string _title { get; set; }
        public string _place { get; set; }
        public string _schedules { get; set; }
        public DateTime _date { get; set; }
        public TimeSpan _hour { get; set; }
    }
}
