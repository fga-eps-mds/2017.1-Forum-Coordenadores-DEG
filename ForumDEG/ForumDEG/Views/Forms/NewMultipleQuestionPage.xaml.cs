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
        public NewMultipleQuestionPage(bool option) {
            BindingContext = new ViewModels.NewMultipleQuestionViewModel(option ,new ViewModels.PageService(), UserDialogs.Instance);
            InitializeComponent();
        }
    }
}
