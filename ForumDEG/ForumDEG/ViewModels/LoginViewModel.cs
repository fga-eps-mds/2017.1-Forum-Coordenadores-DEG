using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using ForumDEG.Models;

namespace ForumDEG.ViewModels {
    class LoginViewModel {
        private List<Administrator> adminstrators = GetAllAdministrators();
        private List<Coordinator> coordinators = GetAllCoordinator();

        private User _user;

        public User MakeLogin(string email, string password) {
            // TODO: check if email is valid with IsValidEmail when the method get fixed
            _user = FindAdmin(email);
            if (_user == null) {
                _user = FindCoord(email);
            }
            if (_user != null) {
                if (_user._password == password) return true;
            }
            return false;
        }

        public bool RecoverPassword(string email) {
            //if (!IsValidEmail(email)) return false;
            var user = FindUser(email);
            if (user != null) return true;
            return false;
        }

        private bool FindCoord(string email) {
            foreach (var coordinator in coordinators) {
                if (coordinator._email == email) {
                    _user = coordinator;
                    return true;
                }
                    
                
            }
                
            return false;
        }

        public bool FindAdmin(string email) {
            foreach (var administrator in administrators) {
                if (administrator._email == email) {
                    _user = administrator;
                    return true;
                }
                
            }
            return false;
        }

        public bool IsAdmin(User user) {

        }

        // This method is for some unknown reason givind an exception
        //private bool IsValidEmail(string email) {
        //    Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
        //    Match match = regex.Match(email);
        //    return match.Success;
        //}
    }
}
