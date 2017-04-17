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
    public partial class ForumDetailPage : ContentPage {
        public ForumDetailPage() {
            InitializeComponent();
            if (ForunsViewModel.GetInstance().GetSelected() == null)
                throw new Exception("Forum detail page with no selection");
            BindingContext = ForunsViewModel.GetInstance().GetSelected();
        }
    }
}
