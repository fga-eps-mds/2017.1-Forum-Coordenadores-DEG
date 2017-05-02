using ForumDEG.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ForumDEG.Views {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChangePasswordPage : ContentPage {
        ChangePasswordViewModel _viewModel;

        public ChangePasswordPage() {
            InitializeComponent();
            _viewModel = new ChangePasswordViewModel();
            BindingContext = _viewModel;
        }
    }
}
