using ForumDEG.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ForumDEG.Views {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UserDetailPage : ContentPage {
        public UserDetailPage(UserDetailViewModel _viewModel) {
            BindingContext = _viewModel;
            InitializeComponent();
        }
    }
}
