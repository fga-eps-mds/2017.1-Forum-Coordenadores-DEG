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
    public partial class NewFormPage : ContentPage {
        public NewFormPage() {
            InitializeComponent();
            BindingContext = new NewFormViewModel(new PageService());
        }
    }
}
