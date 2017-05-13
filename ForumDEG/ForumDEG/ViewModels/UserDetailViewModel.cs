using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumDEG.ViewModels {
    public class UserDetailViewModel {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Registration { get; set; }
        public string Email { get; set; }
        public string Course { get; set; }
        public bool IsAdministrator { get; set; }
        public bool IsCoordinator { get; set; }
    }
}
