using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using ForumDEG.Models;
using ForumDEG.Utils;
using Android.Util;

namespace ForumDEG.ViewModels {
    class LoginViewModel {

        private List<Administrator> administrators = null;
        private List<Coordinator> coordinators = null ;
        private User _user;

        protected async void LoadCoordinators() {
            Log.Info("LoadCoordinators", "Carregando lista de coord");

            coordinators = await App.CoordinatorDatabase.GetAllCoordinators();

        }

        protected async void LoadAdminstrators() {
            Log.Info("LoadAdminstrators", "Carregando lista de Admin");
            administrators = await App.AdministratorDatabase.GetAllAdministrators();
        }

        private bool FindCoord(string email) {
            LoadCoordinators();
            Log.Info("FindCoord", "Carregado lista de Coord");
            if(coordinators != null)
            foreach (var coordinator in coordinators) {
                if (coordinator.Email == email) {
                    Log.Info("FindCoord", "Coord encontrado");
                    _user = coordinator;
                    return true;
                }
            }
            Log.Info("FindCoord", "Coord não encontrado");
            return false;
        }

        public bool FindAdmin(string email) {
            LoadAdminstrators();
            int i = 1;
            Log.Info("FindAdmin", "Lista de Admin Carregada");
            if(administrators != null) {
                foreach (Administrator administrator in administrators.Where(
                administrator => administrator != null)) {
                    Log.Info("Adminstrator", i.ToString());
                    if (administrator.Email == email) {
                        Log.Info("FindAdmin", "Admin encontrado");
                        _user = administrator;
                        return true;
                    }
                }
            }
            Log.Info("FindAdmin", "Admin nao encontrado");
            return false;
        }

        public bool MakeLogin(string email, string password) { 
            Administrator adm = new Administrator();
            App.AdministratorDatabase.SaveAdministrator(adm);
            Log.Info("MakeLogin", "Procurando admin");
            FindAdmin(email);
            if (_user == null) {
               FindCoord(email);
            }
            if (_user != null) {
                Log.Info("MakeLogin", "salvando info de user logado");
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
