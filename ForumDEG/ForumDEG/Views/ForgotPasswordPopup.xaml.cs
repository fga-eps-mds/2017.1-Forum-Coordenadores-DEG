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
        public ForgotPasswordPopup() {
            InitializeComponent();
        }

        private async void OnResetPasswordButtonClicked(object sender, EventArgs e) {
            await DisplayAlert("Restauração de senha", "Um email foi enviado com as instruções para recuperação da senha", "OK");
            await PopupNavigation.PopAsync();
        }
    }
}
