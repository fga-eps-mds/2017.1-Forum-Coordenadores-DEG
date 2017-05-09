using Acr.UserDialogs;
using ForumDEG.ViewModels;
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
    }
}
