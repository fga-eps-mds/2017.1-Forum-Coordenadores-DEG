using Acr.UserDialogs;
using ForumDEG.ViewModels;
using System.Diagnostics;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ForumDEG.Views {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage {
        private LoginViewModel _viewModel;

        public LoginPage() {
            _viewModel = new LoginViewModel(new PageService(), UserDialogs.Instance);
            BindingContext = _viewModel;

            InitializeComponent();
        }

        public async void Validate() {
            if (await _viewModel.ValidateLogin()) {
                if (Helpers.Settings.IsUserAdmin) {
                    Navigation.InsertPageBefore(new AppMasterPage(), this);
                    await Navigation.PopAsync();
                }
                else {
                    Navigation.InsertPageBefore(new CoordinatorMasterPage(), this);
                    await Navigation.PopAsync();
                }
            }
        } 

        public void LoginClicked() {
            Validate();
            Debug.WriteLine("[User API]: Acabou");
        }
    }
}
