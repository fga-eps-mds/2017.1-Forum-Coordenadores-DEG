using Acr.UserDialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ForumDEG.Views.Forms {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewMultipleQuestionPage : ContentPage {
        ViewModels.NewMultipleQuestionViewModel _viewModel;
        public NewMultipleQuestionPage(bool option) {
            _viewModel = new ViewModels.NewMultipleQuestionViewModel(option, 
                                                                     new ViewModels.PageService(), 
                                                                     UserDialogs.Instance);
            BindingContext = _viewModel;
            InitializeComponent();
        }
    }
}
