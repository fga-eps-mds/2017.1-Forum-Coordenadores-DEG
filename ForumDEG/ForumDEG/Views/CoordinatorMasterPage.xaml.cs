using ForumDEG.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ForumDEG.Views {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CoordinatorMasterPage : ContentPage {

        public  CoordinatorMasterPage() {
            CoordinatorMasterPageViewModel ViewModel = CoordinatorMasterPageViewModel.GetInstance();
            BindingContext = ViewModel;
            Debug.WriteLine("[CoordinatorMasterPage]: goes to select forum");
            ViewModel.SelectForum();
            try{
                InitializeComponent();  
            }
            catch(Exception ex) { 
                Debug.WriteLine("[CoordinatorMasterPage]: initialize component " + (ex.Message));
            }
        }

        private void seeDetailsButtonClicked(object sender, EventArgs e) {

        }

    }
}
