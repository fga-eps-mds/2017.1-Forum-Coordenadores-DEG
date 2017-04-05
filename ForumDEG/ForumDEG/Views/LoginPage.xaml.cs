using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ForumDEG.Views {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage {
        public LoginPage() {
            InitializeComponent();
        }

        private async void OnForgotPasswordTapped(object sender, EventArgs e) {
            var page = new Views.ForgotPasswordPopup();

            await Navigation.PushPopupAsync(page);
        }

        async void OnLoginButtonClicked(object sender, EventArgs e) {
            var user = new Models.User {
                email = emailEntry.Text,
                password = passwordEntry.Text
            };

            if (isLoginInfoCorrect(user)) {
                App.IsLoggedIn = true;
                Navigation.InsertPageBefore(new Views.MainPage(), this);
                await Navigation.PopAsync();
            } else {
                loginFailedLabel.Text = "Usuário ou senha incorretos!";
                passwordEntry.Text = string.Empty;
            }
        }

        private bool isLoginInfoCorrect (Models.User user) {
            return ((user.email == "test@test.com") && (user.password == "testpass"));
        }
    }
}
