using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ForumDEG.ViewModels;
using System.Diagnostics;

namespace ForumDEG.Views {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ForumsPage : ContentPage {   
        public ForumsPage() {
            BindingContext = ForumsViewModel.GetInstance();
            // calling simulation method
            ViewModel.UpdateForumsList();
            InitializeComponent();
        }

        override protected void OnAppearing() {}

        private void ItemSelected(object sender, SelectedItemChangedEventArgs e) {
            Debug.WriteLine("Clicked item");

            if (e.SelectedItem != null && ViewModel.SelectForumCommand != null && ViewModel.SelectForumCommand.CanExecute(e)) {
                ViewModel.SelectForumCommand.Execute(e.SelectedItem);
                ViewModel.SelectedForum = null;
            }

        }
       

    private ForumsViewModel ViewModel {
            get { return (BindingContext as ForumsViewModel); }
            set { BindingContext = value; }
        }
    }
}
