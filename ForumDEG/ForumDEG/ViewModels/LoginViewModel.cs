using Acr.UserDialogs;
using ForumDEG.Interfaces;
using ForumDEG.Views;
using System;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace ForumDEG.ViewModels {
    public class LoginViewModel {
        public string _userRegistration { get; set; }
        public string _userPassword { get; set; }

        private readonly Helpers.User _userService;

        private IPageService _pageService;
        private IUserDialogs _dialog;

        public LoginViewModel(IPageService pageService, IUserDialogs dialog) {
            _pageService = pageService;
            _dialog = dialog;
            _userService = new Helpers.User();
        }

        public async Task<bool> ValidateLogin() {
            // executed when login button is clicked
            if (IsAnyFieldEmpty()) return false;
            if (!ValidateRegistration()) return false;
            if (!ValidatePasswordRegex()) return false;
            if (!await ValidateOnDatabase()) return false;
            LogUser();
            return true;
        }

        private void LogUser() {
            App.Current.Properties["registration"] = _userRegistration;
        }

        private bool IsAnyFieldEmpty() {
            if (string.IsNullOrWhiteSpace(_userRegistration) || string.IsNullOrWhiteSpace(_userPassword)) {
                _dialog.Alert(message: "Não podem haver campos vazios!", okText: "OK");
                return true;
            }
            return false;
        }

        private bool ValidateRegistration() {
            // validates registration
            if (_userRegistration.Length < 6 || _userRegistration.Length > 12) {
                _dialog.Alert(message: "Matrícula inválida!", okText: "OK");
                return false;
            }
            return true;
        }

        private bool ValidatePasswordRegex() {
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
                    App.Current.Properties["isAdmin"] = true;
                    Debug.WriteLine("[User API]: Adm");
                    return true;
                } else if (result == "coordinator") {
                    App.Current.Properties["isAdmin"] = false;
                    Debug.WriteLine("[User API]: Coord");
                    return true;
                } else {
                    _dialog.Alert(message: "Matrícula ou Senha inválida!", okText: "OK");
                    return false;
                }
            } catch (Exception ex) {

                Debug.WriteLine("[User API exception]:" + ex.Message);
                return false;
            }
            
            
        }
    }
}
