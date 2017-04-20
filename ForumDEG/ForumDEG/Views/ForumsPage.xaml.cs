using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ForumDEG.ViewModels;

namespace ForumDEG.Views {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ForumsPage : ContentPage {   
        public ForumsPage() {
            BindingContext = ForumsViewModel.GetInstance();
            // calling simulation method
            ViewModel.UpdateForumsList();
            InitializeComponent();
        }

        override protected void OnAppearing() {
            if (ViewModel.SelectedForum != null)
                ViewModel.SelectedForum = null;
        }

        private void ItemSelected(object sender, SelectedItemChangedEventArgs e) {
            ViewModel.SelectForumCommand.Execute(e.SelectedItem);
        }

        private ForumsViewModel ViewModel {
            get { return (BindingContext as ForumsViewModel); }
            set { BindingContext = value; }
        }
    }
}
