using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using ForumDEG.Models;
using ForumDEG.Utils;

namespace ForumDEG.ViewModels {
    class LoginViewModel {

        private User _user;

        protected async Task <List<Coordinator>> LoadCoordinators() {
            List<Coordinator> coor;
            coor = await App.CoordinatorDatabase.GetAllCoordinators();
            return coor;
        }

        protected async Task<List<Administrator>> LoadAdminstrators() {

            return await App.AdministratorDatabase.GetAllAdministrators();
        }

        private async Task<bool> FindCoord(string email) {
            List<Coordinator> coordinators = null;
            coordinators = await LoadCoordinators();
            if(coordinators != null)
            foreach (var coordinator in coordinators) {
                if (coordinator.Email == email) {
                    _user = coordinator;
                    return true;
                }
            }
            return false;
        }

        public async Task<bool> FindAdmin(string email) {
            List<Administrator> administrators = null;
            administrators = await LoadAdminstrators();
            if(administrators != null) {
                foreach (Administrator administrator in administrators.Where(
                administrator => administrator != null)) {
 
                    if (administrator.Email == email) {
                        _user = administrator;
                        return true;
                    }
                }
            }
            return false;
        }

        public async Task<bool> MakeLogin(string email, string password) {
            bool adm = await FindAdmin(email);
            bool coor = await FindCoord(email);

            if (adm || coor) {
                if (_user.Password == password) {
                    Helpers.Settings.IsLoggedIn = true;
                    Helpers.Settings.UserId = _user.Id;
                    Helpers.Settings.UserName = _user.Name;
                    if(_user is Administrator) {
                        Helpers.Settings.IsAdmin = true;
                    } else {
                        Helpers.Settings.IsAdmin = false;
                    }
                    return true;
                }
            }
            return false;
        }
    }
}
