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
        CoordinatorMasterPageViewModel ViewModel;

        public  CoordinatorMasterPage() {
            ViewModel = CoordinatorMasterPageViewModel.GetInstance();
            BindingContext = ViewModel;
            ViewModel.SelectForum();
            ViewModel.SelectForm();
            Debug.WriteLine("[CoordinatorMasterPage]: goes to select forum");
            InitializeComponent();  
        }

        protected override bool OnBackButtonPressed() {
            return false;
        }

        protected override void OnAppearing() {
            base.OnAppearing();
            ViewModel.SelectForum();
        }
    }
}
