using ForumDEG.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumDEG.ViewModels {
    class ForumDetailViewModel {
        public string Title { get; set; }
        public string Place { get; set; }
        public string Schedules { get; set; }
        public DateTime Date { get; set; }
        public bool IsPast { get; }

        public ForumDetailViewModel() {
            IsPast = HasPassed();
        }

        public bool HasPassed() {
            return DateTime.Now - Date > TimeSpan.FromMinutes(0);
        }
    }
}
