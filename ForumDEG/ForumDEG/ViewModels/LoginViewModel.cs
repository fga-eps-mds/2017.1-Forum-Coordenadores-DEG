using Acr.UserDialogs;
using ForumDEG.Interfaces;
using System.Diagnostics;
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
            if (!IsAnyFieldEmpty()) return;
            if (!ValidateEmailRegex()) return;
            if (!ValidatePasswordRegex()) return;
            if (!ValidateOnDatabase()) return;
        }

        private void LogUser() {

        }

        private bool IsAnyFieldEmpty() {
            if (string.IsNullOrWhiteSpace(_userEmail) || string.IsNullOrWhiteSpace(_userPassword)) {
                return true;
            }
            return false;
        }

        private bool ValidateEmailRegex() {
            // validates email regex
            return true;
        }

        private bool ValidatePasswordRegex() {
            // validates password regex
            return true;
        }

        private bool ValidateOnDatabase() {
            // validate if user and password matches any in database
            return true;
        }
    }
}
