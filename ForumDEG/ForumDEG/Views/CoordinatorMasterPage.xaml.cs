using ForumDEG.ViewModels;
using System;
using System.Collections.Generic;
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
            ViewModel.SelectForum();
            InitializeComponent();
        }

        private void seeDetailsButtonClicked(object sender, EventArgs e) {

        }

    }
}
