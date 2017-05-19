using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ForumDEG.ViewModels;
using System.Diagnostics;

namespace ForumDEG.Views {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UsersPage : ContentPage {
        public UsersPage() {
            BindingContext = UsersViewModel.GetInstance();
            ViewModel.UpdateUsersList();
            InitializeComponent();
        }

        private void ItemSelected(object sender, SelectedItemChangedEventArgs e) {
            Debug.WriteLine("Clicked item");
            if (e.SelectedItem != null && ViewModel.SelectAdministratorCommand != null && ViewModel.SelectAdministratorCommand.CanExecute(e)) {
                ViewModel.SelectAdministratorCommand.Execute(e.SelectedItem);
                ViewModel.SelectedAdministrator = null;
                ViewModel.SelectedCoordinator = null;
            }
        }

    private UsersViewModel ViewModel {
            get { return (BindingContext as UsersViewModel); }
            set { BindingContext = value; }
        }
    }
}
