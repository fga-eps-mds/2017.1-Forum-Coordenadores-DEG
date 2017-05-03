using Acr.UserDialogs;
using ForumDEG.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ForumDEG.Views {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChangePasswordPage : ContentPage {
        ChangePasswordViewModel _viewModel;

        public ChangePasswordPage() {
            _viewModel = new ChangePasswordViewModel(new PageService());
            BindingContext = _viewModel;
            InitializeComponent();
        }
    }
}
