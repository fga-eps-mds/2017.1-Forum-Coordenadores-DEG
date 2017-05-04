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

        string _actualPassword { get; set; }
        string _newPassword { get; set; }
        string _repeatedPassword { get; set; }

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
            if (string.IsNullOrWhiteSpace(_actualPassword) || string.IsNullOrWhiteSpace(_newPassword)) {
                _dialog.Alert("Campos em brancos!", "Erro");
            }
            else {
                _dialog.Alert("Senha alterada.", "Feito");
            }
        }
    }
}
