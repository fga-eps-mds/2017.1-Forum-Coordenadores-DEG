using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumDEG.ViewModels {
    class LoginViewModel {
        public bool MakeLogin(string email, string password) {
            if (!email.Equals("") && password.Equals(email)) return true;
            return false;
        }
    }
}
