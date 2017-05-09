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

        public ICommand MakeLoginCommand { get; private set; }

        private IPageService _pageService;
        private IUserDialogs _dialog;

        public LoginViewModel(IPageService pageService, IUserDialogs dialog) {
            _pageService = pageService;
            _dialog = dialog;

            MakeLoginCommand = new Command(MakeLogin);
        }

        private void MakeLogin() {
            // executed when login button is clicked
            if (IsAnyFieldEmpty()) return;
            if (!ValidateEmailRegex()) return;
            if (!ValidatePasswordRegex()) return;
            if (!ValidateOnDatabase()) return;
            LogUser();
        }

        private async void LogUser() {
            await _pageService.PushAsync(new AppMasterPage());
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
