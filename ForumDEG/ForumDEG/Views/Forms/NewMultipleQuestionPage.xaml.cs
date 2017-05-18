using Acr.UserDialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ForumDEG.ViewModels;

namespace ForumDEG.Views.Forms {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewMultipleQuestionPage : ContentPage {
        NewMultipleQuestionViewModel _viewModel;
        public NewMultipleQuestionPage(bool option, NewFormViewModel formViewModel) {
            _viewModel = new NewMultipleQuestionViewModel(option, 
                                                          new PageService(), 
                                                          UserDialogs.Instance,
                                                          formViewModel);
            BindingContext = _viewModel;
            InitializeComponent();
        }
    }
}
