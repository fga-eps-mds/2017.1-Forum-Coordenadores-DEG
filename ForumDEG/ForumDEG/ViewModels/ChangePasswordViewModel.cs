using Acr.UserDialogs;
using ForumDEG.Interfaces;
using ForumDEG.Models;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using System.Text.RegularExpressions;

namespace ForumDEG.ViewModels {
    class ChangePasswordViewModel {
        public Coordinator Coordinator { get; private set; }

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

            Coordinator = GetLoggedCoordinator();

            ChangePasswordClickedCommand = new Command(UpdatePassword);
            CancelClickedCommand = new Command(async () => await CancelAsync());
        }

        private Coordinator GetLoggedCoordinator() {
            Coordinator LoggedCoordinator = new Coordinator(); 

            LoggedCoordinator.Id = 123;
            LoggedCoordinator.Course = "Engenharia";
            LoggedCoordinator.Email = "email@email.com";
            LoggedCoordinator.Name = "Marigué";
            LoggedCoordinator.Password = "123456aA";
            LoggedCoordinator.Registration = "150151624";

            return LoggedCoordinator;
        }

        private async Task CancelAsync() {
            await _pageService.PopAsync();
        }

        private void UpdatePassword() {
            if (MakeVerifications()) {
                Coordinator.Password = _newPassword; // fake update
                // must update on database
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
            if (!ValidatePassword(_newPassword)) {
                _dialog.Alert("A senha deve conter:\n  - De 8 a 15 caracteres\n  - Pelo menos uma letra maiúscula e uma minúscula\n  - Pelo menos um número");
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
            if (Coordinator.Password == password)
                return true;
            return false;
        }

        private bool MatchPasswords(string password1, string password2) {
            if (!string.Equals(password1, password2))
                return false;
            return true;
        }

        private bool ValidatePassword(string password) {
            if (password.Length < 8)
                return false;

            var regex = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,15}$";
            var match = Regex.Match(password, regex);

            if (!match.Success)
                return false;

            return true;
        }
    }
}
