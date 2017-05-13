using ForumDEG.ViewModels;
using System.Diagnostics;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ForumDEG.Views {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ForumDetailPage : ContentPage {
        private ForumDetailViewModel _viewModel;
        public ForumDetailPage() {
            _viewModel = ForumsViewModel.GetInstance().SelectedForum;
            BindingContext = _viewModel;
            InitializeComponent();
        }

        protected override void OnAppearing() {
            base.OnAppearing();
            _viewModel.GetConfirmation();
        }

    }
}
