using Acr.UserDialogs;
using ForumDEG.Interfaces;
using ForumDEG.Helpers;
using ForumDEG.Views;
using System;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using System.ComponentModel;

namespace ForumDEG.ViewModels {
    public class LoginViewModel : BaseViewModel {
        public string _userRegistration { get; set; }
        public string _userPassword { get; set; }

        private readonly Helpers.User _userService;

        private IPageService _pageService;
        private IUserDialogs _dialog;

        private bool _activityIndicator;
        public bool ActivityIndicator {
            get {
                return _activityIndicator;
            }
            set {
                if (_activityIndicator != value) {
                    _activityIndicator = value;

                    OnPropertyChanged("ActivityIndicator");
                }
            }
        }

        public LoginViewModel(IPageService pageService, IUserDialogs dialog) {
            _pageService = pageService;
            _dialog = dialog;
            _userService = new Helpers.User();
            ActivityIndicator = false;
        }

        public async Task<bool> ValidateLogin() {
            // executed when login button is clicked
            ActivityIndicator = true;
            if (IsAnyFieldEmpty()) return false;
            if (!ValidateRegistration()) return false;
            if (!ValidatePasswordRegex()) return false;
            ActivityIndicator = true;
            if (!await ValidateOnDatabase()) return false;
            LogUser();
            return true;
        }

        private void LogUser() {
            ActivityIndicator = false;
            Settings.UserReg = _userRegistration;
            Settings.IsUserLogged = true;
        }

        private bool IsAnyFieldEmpty() {
            ActivityIndicator = false;
            if (string.IsNullOrWhiteSpace(_userRegistration) || string.IsNullOrWhiteSpace(_userPassword)) {
                _dialog.Alert(message: "Não podem haver campos vazios!", okText: "OK");
                return true;
            }
            return false;
        }

        private bool ValidateRegistration() {
            // validates registration
            ActivityIndicator = false;
            if (_userRegistration.Length < 6 || _userRegistration.Length > 12) {
                _dialog.Alert(message: "Matrícula inválida!", okText: "OK");
                return false;
            }
            return true;
        }

        private bool ValidatePasswordRegex() {
            ActivityIndicator = false;
            var regex = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,15}$";
            var match = Regex.Match(_userPassword, regex);

            if (match.Success)
                return true;

            _dialog.Alert(message: "A senha não segue os padrões corretos:\n - De 8 a 15 caracteres\n " +
                "- Pelo menos uma letra maiúscula e uma minúscula\n  - Pelo menos um número", okText: "OK");
            return false;
        }

        private async Task<bool> ValidateOnDatabase() {
            try {
                string result = await _userService.AuthenticateLogin(_userRegistration, _userPassword);
                if (result == "administrator") {
                    Settings.IsUserAdmin = true;
                    Settings.IsUserCoord = false;
                    Debug.WriteLine("[User API]: Adm");
                    return true;
                } else if (result == "coordinator") {
                    Settings.IsUserAdmin = false;
                    Settings.IsUserCoord = true;
                    Debug.WriteLine("[User API]: Coord");
                    return true;
                } else {
                    ActivityIndicator = false;
                    Debug.WriteLine("[LOGIN VIEW MODEL]: when activity indicator should be false, activity indicator is: " + ActivityIndicator);
                    _dialog.Alert(message: "Matrícula ou Senha inválida!", okText: "OK");
                    return false;
                }
            } catch (Exception ex) {

                Debug.WriteLine("[User API exception]:" + ex.Message);
                ActivityIndicator = false;
                return false;
            }
            
            
        }
    }
}
