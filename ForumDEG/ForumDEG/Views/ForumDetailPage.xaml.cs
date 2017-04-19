using ForumDEG.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ForumDEG.Views {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ForumDetailPage : ContentPage {
        public ForumDetailPage() {
            BindingContext = ForumsViewModel.GetInstance().SelectedForum;
            InitializeComponent();
        }
    }
}
