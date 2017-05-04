using Acr.UserDialogs;
using ForumDEG.Interfaces;
using ForumDEG.Models;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace ForumDEG.ViewModels {
    class ChangePasswordViewModel {
        public Coordinator UpdatedCoordinator { get; private set; } = new Coordinator();

        public ICommand ChangePasswordClickedCommand { get; private set; }
        public ICommand CancelClickedCommand { get; private set; }

        private readonly IPageService _pageService;
        private readonly IUserDialogs _dialog;

        public string _actualPassword { get; set; }
        public string _newPassword { get; set; }
        public string _repeatedPassword { get; set; }

        public ChangePasswordViewModel(IPageService pageService, IUserDialogs dialog) {
            _pageService = pageService;
            _dialog = dialog;
            ChangePasswordClickedCommand = new Command(UpdatePassword);
            CancelClickedCommand = new Command(async () => await CancelAsync());
        }

        private async Task CancelAsync() {
            await _pageService.PopAsync();
        }

        private void UpdatePassword() {
            if(MakeVerifications()) {
                // updates password
                _dialog.ShowSuccess("Senha trocada com sucesso");
            }
        }

        private bool MakeVerifications() {
            if (!ValidateFields(_actualPassword) || !ValidateFields(_newPassword) || !ValidateFields(_repeatedPassword)) {
                _dialog.ShowError("Campos em branco não são permitidos!");
                return false;
            }
            if (!MatchPasswords(_newPassword, _repeatedPassword)) {
                _dialog.ShowError("A nova senha não está igual nos dois campos!");
                return false;
            }
            if (!VerifyActualPassword(_actualPassword)) {
                _dialog.ShowError("Senha atual está errada!");
                return false;
            }
            if(!ValidatePassword(_newPassword)) {
                _dialog.Alert("A senha deve conter pelo menos 8 caracteres com números e letras obrigatoriamente!");
                return false;
            }
            return true;
        }

        private bool ValidateFields(string field) {
            if (string.IsNullOrWhiteSpace(field))
                return false;
            return true;
        }

        private bool VerifyActualPassword(string password) {
            // TODO: verifies if password inserted as actual is really logged user password
            return true;
        }

        private bool MatchPasswords(string password1, string password2) {
            if (!string.Equals(password1, password2))
                return false;
            return true;
        }

        private bool ValidatePassword(string password) {
            // TODO: regex for checking if password contains necessary chars
            if(password.Length < 8)
                return false;
            return true;
        }
    }
}
