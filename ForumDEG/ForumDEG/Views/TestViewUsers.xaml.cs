using ForumDEG.Utils;
using ForumDEG.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ForumDEG.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TestViewUsers : ContentPage
    {
        public TestViewUsers()
        {
            BindingContext = UsersPageViewModel.GetInstance();
            ViewModel.UpdateUsersLists();
            InitializeComponent();
        }

        override protected void OnAppearing() {
            if (ViewModel.SelectedAdministrator != null)
                ViewModel.SelectedAdministrator = null;
            if (ViewModel.SelectedCoordinator != null)
                ViewModel.SelectedCoordinator = null;
        }

        private void ItemSelected(object sender, SelectedItemChangedEventArgs e) {
        }

        private UsersPageViewModel ViewModel {
            get { return (BindingContext as UsersPageViewModel); }
            set { BindingContext = value; }
        }
    }
}
