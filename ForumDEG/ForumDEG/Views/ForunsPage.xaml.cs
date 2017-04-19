using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ForumDEG.ViewModels;

namespace ForumDEG.Views {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ForunsPage : ContentPage {
        
        public ForunsPage() {
            BindingContext = ForunsViewModel.GetInstance();
            // calling simulation method
            ViewModel.UpdateForumsList();
            InitializeComponent();
        }

        override protected void OnAppearing() {
            if (ViewModel.SelectedForum != null)
                ViewModel.SelectedForum = null;
        }

        private async void ItemSelected(object sender, SelectedItemChangedEventArgs e) {
            await ViewModel.SelectForum(e.SelectedItem as ForumDetailViewModel);
        }

        private ForunsViewModel ViewModel {
            get { return (BindingContext as ForunsViewModel); }
            set { BindingContext = value; }
        }
    }
}
