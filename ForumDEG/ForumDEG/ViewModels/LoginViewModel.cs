using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumDEG.ViewModels {
    class LoginViewModel {
        public bool MakeLogin(string email, string password) {
            return (!email.Equals("") && password.Equals(email));
        }
    }
}
