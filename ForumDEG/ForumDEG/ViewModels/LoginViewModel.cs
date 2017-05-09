using Acr.UserDialogs;
using ForumDEG.Interfaces;
using ForumDEG.Views;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Windows.Input;
using Xamarin.Forms;

namespace ForumDEG.ViewModels {
    class LoginViewModel {
        public string _userEmail { get; set; }
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
            if (!ValidateEmailRegex()) return false;
            if (!ValidatePasswordRegex()) return false;
            if (!ValidateOnDatabase()) return false;
            LogUser();
            return true;
        }

        private void LogUser() {

        }

        private bool IsAnyFieldEmpty() {
            if (string.IsNullOrWhiteSpace(_userEmail) || string.IsNullOrWhiteSpace(_userPassword)) {
                _dialog.Alert(message: "Não podem haver campos vazios!", okText: "OK");
                return true;
            }
            return false;
        }

        private bool ValidateEmailRegex() {
            // validates email regex
            bool isEmail = Regex.IsMatch(_userEmail, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase);
            if (!isEmail) {
                _dialog.Alert(message: "Email inválido!", okText: "OK");
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
