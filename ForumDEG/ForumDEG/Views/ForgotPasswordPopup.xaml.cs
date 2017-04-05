using ForumDEG.ViewModels;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ForumDEG.Views {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ForgotPasswordPopup : PopupPage {
        LoginViewModel lvm = new LoginViewModel();

        public ForgotPasswordPopup() {
            InitializeComponent();
        }

        private async void OnResetPasswordButtonClicked(object sender, EventArgs e) {
            if (lvm.RecoverPassword(recoverEmailEntry.Text)) {
                await DisplayAlert("Restauração de senha", "Um email foi enviado com as instruções para recuperação da senha", "OK");
                await PopupNavigation.PopAsync();
            }
            else {
                await DisplayAlert("Restauração de senha", "O email inserido é inválido", "OK");
            }
        }
    }
}
