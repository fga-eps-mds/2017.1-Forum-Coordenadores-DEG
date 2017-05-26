using ForumDEG.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ForumDEG.Views {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AppMasterPage : MasterDetailPage {

        private AppMasterViewModel viewModel = new AppMasterViewModel(new PageService());

        public AppMasterPage() {
            BindingContext = viewModel;
            InitializeComponent();
        }

        private async Task LogoutButtonClicked(object sender, EventArgs e) {
            var answer = await DisplayAlert("Sair", "Tem certeza que deseja sair da sua conta?", "SIM", "NÃO");
            if (answer) {
                ForumDEG.Helpers.Settings.IsUserLogged = false;
                Navigation.InsertPageBefore(new LoginPage(), this);
                await Navigation.PopAsync();
            }
        }
    }
}

