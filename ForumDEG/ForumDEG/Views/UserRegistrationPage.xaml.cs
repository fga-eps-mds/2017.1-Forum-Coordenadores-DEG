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
    public partial class UserRegistrationPage : ContentPage
    {
        private UserRegistrationViewModel _userRegistrationViewModel = new UserRegistrationViewModel(new PageService());
        public UserRegistrationPage(){
            InitializeComponent();
            BindingContext = _userRegistrationViewModel;
            User_Type.SelectedIndex = 0;
        }

        private void User_Type_SelectedIndexChanged(object sender, EventArgs e){
            if (User_Type.SelectedIndex == 0){
                Course.IsEnabled = true;
            }
            else{
                Course.SelectedIndex = -1;
                Course.IsEnabled = false;
            }
        }

        private async void Confirmation_Clicked(object sender, EventArgs e){
            _userRegistrationViewModel.RegisterNewUser();
            await DisplayAlert("Confirmou", "Confirmou novo usuário!", "ok");
            await Navigation.PushAsync(new TestViewUsers());
        }
    }
}
