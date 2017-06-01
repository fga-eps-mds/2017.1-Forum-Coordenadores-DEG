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
    public partial class FormsPage : ContentPage {
        public FormsPage() {
            BindingContext = FormsViewModel.GetInstance();
            _viewModel.UpdateFormsList();
            InitializeComponent();
        }

        override protected void OnAppearing() { }

        private void ItemSelected(object sender, SelectedItemChangedEventArgs e) {
            Debug.WriteLine("Clicked item");

            if (e.SelectedItem != null && _viewModel.SelectFormCommand != null && _viewModel.SelectFormCommand.CanExecute(e)) {
                _viewModel.SelectFormCommand.Execute(e.SelectedItem);
                _viewModel.SelectedForm = null;
            }

        }

        private FormsViewModel _viewModel {
            get { return (BindingContext as FormsViewModel); }
            set { BindingContext = value; }
        }
    }
}
