using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumDEG.Models
{
    public class Coordinator : User {
        private string _course;
        public string Course { get => _course; set => _course = value; }
    }
}
