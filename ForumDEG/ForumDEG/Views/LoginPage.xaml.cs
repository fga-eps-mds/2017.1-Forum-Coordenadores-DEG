using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ForumDEG.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ForumDEG.Views {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage {
        private LoginViewModel _lvm = new LoginViewModel();

        public LoginPage() {
            InitializeComponent();
        }

        async void OnLoginButtonClicked(object sender, EventArgs e) {
            if (_lvm.MakeLogin(emailEntry.Text, passwordEntry.Text)) {
                if (Helpers.Settings.IsAdmin) {
                    Navigation.InsertPageBefore(new Views.MainPageAdministrator(), this);
                } else {
                    Navigation.InsertPageBefore(new Views.MainPageCoordinator(), this);
                }
                await Navigation.PopAsync();
            } else {
                loginFailedLabel.Text = "Usuário ou senha incorretos!";
                passwordEntry.Text = string.Empty;
            }
        }
    }
}
