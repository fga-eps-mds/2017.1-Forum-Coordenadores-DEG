using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumDEG.Models {
    public class SingleAnswerQuestion {
        public string Question { get; set; }
        public ObservableCollection<string> Options { get; set; }
    }
}
