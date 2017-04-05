using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using ForumDEG.Models;

namespace ForumDEG.ViewModels {
    class LoginViewModel {
        private List<User> users = new List<User>() {
            new User("John", "xablau@email.com", "passwordjohn", "150154647"),
            new User("Romeu", "migue@email.com", "pequeno", "141405672"),
            new User("Painillo", "danillo@pai.com", "paizao", "210213456"),
            new User("Thiago Boy", "thiago@stickers.com", "10horasStickers", "456436798"),
            new User("Clarissa Girl", "clarissa@doguinho.com", "savethedogs", "142345090"),
            new User("Marigué, a Migueriana", "marigue@migueriana.com", "soumiguemsm", "099294545")
        };

        private User _user;

        public bool MakeLogin(string email, string password) {
            // TODO: check if email is valid with IsValidEmail when the method get fixed
            _user = FindUser(email);
            if (_user != null)
                if (_user.password == password) return true;
            return false;
        }

        public bool RecoverPassword(string email) {
            //if (!IsValidEmail(email)) return false;
            var user = FindUser(email);
            if (user != null) return true;
            return false;
        }

        private User FindUser(string email) {
            foreach (var user in users)
                if (user.email == email) return user;
            return null;
        }

        // This method is for some unknown reason givind an exception
        //private bool IsValidEmail(string email) {
        //    Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
        //    Match match = regex.Match(email);
        //    return match.Success;
        //}
    }
}
