using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ForumDEG.ViewModels;

namespace ForumDEG.Views {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ForumEditPage : ContentPage {
        private ForumEditViewModel _viewModel = new ForumEditViewModel(new PageService());
        public ForumEditPage() {
            InitializeComponent();

            BindingContext = _viewModel;

            dateDatePicker.Date = DateTime.Now;
            dateDatePicker.MinimumDate = DateTime.Now;
        }
    }
}
