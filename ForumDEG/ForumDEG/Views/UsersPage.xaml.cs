using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ForumDEG.ViewModels;

namespace ForumDEG.Views {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UsersPage : ContentPage {
        public UsersPage() {
            BindingContext = UsersViewModel.GetInstance();
            ViewModel.UpdateUsersList();
            InitializeComponent();
        }

        override protected void OnAppearing() {
            if (ViewModel.SelectedAdministrator != null)
                ViewModel.SelectedAdministrator = null;
            if (ViewModel.SelectedCoordinator != null)
                ViewModel.SelectedCoordinator = null;
        }

        private async void ItemSelected(object sender, SelectedItemChangedEventArgs e) {
            await ViewModel.SelectAdministrator(e.SelectedItem as UserDetailViewModel);
        }

        private UsersViewModel ViewModel {
            get { return (BindingContext as UsersViewModel); }
            set { BindingContext = value; }
        }
    }
}
