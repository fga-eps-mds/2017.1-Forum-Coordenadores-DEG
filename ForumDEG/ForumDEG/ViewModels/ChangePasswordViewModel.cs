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

        public ICommand ChangePasswordClickedCommand;

        private readonly IPageService _pageService;
        private readonly IUserDialogs _userDialogs;

        string _actualPassword;
        string _newPassword;
        string _repeatedPassword;

        public ChangePasswordViewModel(IPageService pageService) {
            _pageService = pageService;
            ChangePasswordClickedCommand = new Command(async () => await UpdatePassword());
        }

        private async Task UpdatePassword() {
            if (string.IsNullOrWhiteSpace(_actualPassword) || string.IsNullOrWhiteSpace(_newPassword)) {
                await _pageService.DisplayAlert("Erro", "Campos em branco!", "OK");
            }
            else {
                await _pageService.DisplayAlert("Pronto", "Senha alterada.", "OK");
            }
        }
    }
}
