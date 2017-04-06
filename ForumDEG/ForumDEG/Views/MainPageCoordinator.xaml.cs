using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ForumDEG.Views {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPageCoordinator : ContentPage {
        public MainPageCoordinator() {
            InitializeComponent();
        }
        async void OnLogoutButtonClicked(object sender, EventArgs e) {
            App.isLoggedIn = false;
            Navigation.InsertPageBefore(new LoginPage(), this);
            await Navigation.PopAsync();
        }
    }
}
