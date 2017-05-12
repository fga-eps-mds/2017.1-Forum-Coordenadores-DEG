using Acr.UserDialogs;
using ForumDEG.Interfaces;
using ForumDEG.Views;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Windows.Input;
using Xamarin.Forms;

namespace ForumDEG.ViewModels {
    public class LoginViewModel {
        public string _userRegistration { get; set; }
        public string _userPassword { get; set; }

        private IPageService _pageService;
        private IUserDialogs _dialog;

        public LoginViewModel(IPageService pageService, IUserDialogs dialog) {
            _pageService = pageService;
            _dialog = dialog;
        }

        public bool ValidateLogin() {
            // executed when login button is clicked
            if (IsAnyFieldEmpty()) return false;
            if (!ValidateRegistration()) return false;
            if (!ValidatePasswordRegex()) return false;
            if (!ValidateOnDatabase()) return false;
            LogUser();
            return true;
        }

        private void LogUser() {
            App.Current.Properties["registration"] = _userRegistration;
            App.Current.Properties["isAdmin"] = true; // TODO: Check if logged is really admin or not
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

        private bool ValidateOnDatabase() {
            // validate if user and password matches any in database
            return true;
        }
    }
}
