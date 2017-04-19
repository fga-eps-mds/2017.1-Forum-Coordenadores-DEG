using ForumDEG.Models;
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
            BindingContext = ForunsViewModel.GetInstance().SelectedForum;
            InitializeComponent();
        }
    }
}
