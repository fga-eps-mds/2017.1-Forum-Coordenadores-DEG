using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ForumDEG.Views.Forms {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class QuestionDetailPage : ContentPage {
        public QuestionDetailPage(ViewModels.NewFormViewModel formViewModel) {
            BindingContext = formViewModel;
            InitializeComponent();
        }
    }
}
