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
    public partial class NewForumPage : ContentPage {
        private NewForumViewModel _viewModel = new NewForumViewModel(new PageService());

        public NewForumPage() {
            InitializeComponent();

            BindingContext = _viewModel;

            dateDatePicker.Date = DateTime.Now;
            dateDatePicker.MinimumDate = DateTime.Now;
        }

        private async void OnNewForumButtonClicked(object sender, EventArgs e) {
            if (_viewModel.IsAnyFieldBlank()) {
                _viewModel.CreationFailed();
            } else {
                _viewModel.CreateForum();

                Navigation.InsertPageBefore(new ForunsPage(), this);
                await Navigation.PopAsync();
            }
        }
    }
}
