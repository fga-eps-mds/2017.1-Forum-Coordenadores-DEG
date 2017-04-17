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
        public UserRegistrationPage()
        {
            InitializeComponent();
            BindingContext = new UserRegistrationViewModel(new PageService());
            User_Type.SelectedIndex = 0;
        }

        private void User_Type_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (User_Type.SelectedIndex == 0)
            {
                Course.IsEnabled = true;
            }
            else
            {
                Course.SelectedIndex = -1;
                Course.IsEnabled = false;
            }
        }

        private void Confirmation_Clicked(object sender, EventArgs e){
           (BindingContext as UserRegistrationViewModel).CreateCoordinator();
        }
    }
}
