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
    public partial class UserRegistrationPage : ContentPage{
        private UserRegistrationViewModel _userRegistrationViewModel = new UserRegistrationViewModel(new PageService());
        public UserRegistrationPage(){
            InitializeComponent();
            BindingContext = _userRegistrationViewModel;
        }
    }
}
