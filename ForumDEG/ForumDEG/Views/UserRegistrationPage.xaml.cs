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
        }

        private void Confirmation_Clicked(object sender, EventArgs e){
           (BindingContext as UserRegistrationViewModel).CreateCoordinator();
        }
    }
}
