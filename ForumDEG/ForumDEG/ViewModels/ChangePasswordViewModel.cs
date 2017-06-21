using Acr.UserDialogs;
using ForumDEG.Interfaces;
using ForumDEG.Models;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using System.Text.RegularExpressions;
using System.Diagnostics;
using ForumDEG.Helpers;

namespace ForumDEG.ViewModels {
    public class ChangePasswordViewModel {
        public Models.Coordinator LoggedCoordinator { get; private set; }

        public ICommand ChangePasswordClickedCommand { get; private set; }
        public ICommand CancelClickedCommand { get; private set; }

        private readonly IPageService _pageService;
        private readonly IUserDialogs _dialog;
        private readonly Helpers.Coordinator _coordinatorService;

        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
        public string RepeatedPassword { get; set; }

        public ChangePasswordViewModel(IPageService pageService, IUserDialogs dialog) {
            _pageService = pageService;
            _coordinatorService = new Helpers.Coordinator();
            _dialog = dialog;

            SetLoggedCoordinator();

            ChangePasswordClickedCommand = new Command(UpdatePassword);
            CancelClickedCommand = new Command(async () => await CancelAsync());
        }

        private async void SetLoggedCoordinator() {
            LoggedCoordinator = await _coordinatorService.GetCoordinatorAsync(Settings.UserReg);
        }

        private async Task CancelAsync() {
            await _pageService.PopAsync();
        }

        private async void UpdatePassword() {
            if (MakeVerifications()) {
                if (await _coordinatorService.PutCoordinatorAsync(LoggedCoordinator.Registration, LoggedCoordinator)) {
                    await _pageService.DisplayAlert("Editar Senha", "A sua senha foi editada com sucesso!", null, "OK");
                } else {
                    await _pageService.DisplayAlert("Editar Senha", "O sua senha não pôde ser editado. Tente novamente!", null, "OK");
                }
            }
        }

        private bool MakeVerifications() {
            if (!ValidateFields(CurrentPassword) || !ValidateFields(NewPassword) || !ValidateFields(RepeatedPassword)) {
                _dialog.ShowError("Campos em branco não são permitidos!");
                return false;
            }
            if (!MatchPasswords(NewPassword, RepeatedPassword)) {
                _dialog.ShowError("A nova senha não está igual nos dois campos!");
                return false;
            }
            if (!VerifyCurrentPassword(CurrentPassword)) {
                _dialog.ShowError("Senha atual está errada!");
                return false;
            }
            if (!ValidatePassword(NewPassword)) {
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

        private bool VerifyCurrentPassword(string password) {
            if (LoggedCoordinator.Password == password)
                return true;
            return false;
        }

        private bool MatchPasswords(string password1, string password2) {
            if (!string.Equals(password1, password2)) {
                Debug.WriteLine("[User Password]: Passwords didn't match!");
                return false;
            }

            return true;
        }

        private bool ValidatePassword(string password) {
            if (password.Length < 8)
                return false;

            var regex = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,15}$";
            var match = Regex.Match(password, regex);

            if (match.Success) {
                Debug.WriteLine("[User Password]: Valid!");
                return true;
            } else {
                Debug.WriteLine("[User Password]: Invalid!");
                return false;
            }
        }
    }
}
