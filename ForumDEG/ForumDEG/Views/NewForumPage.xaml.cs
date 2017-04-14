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
        public NewForumPage() {
            InitializeComponent();
            BindingContext = new ViewModels.NewForumViewModel(new ViewModels.PageService());
            dateDatePicker.Date = DateTime.Now;
            dateDatePicker.MinimumDate = DateTime.Now;
        }

        private async void OnNewForumButtonClicked(object sender, EventArgs e) {
            (BindingContext as ViewModels.NewForumViewModel).CreateForum();

                Navigation.InsertPageBefore(new ForumsPage(), this);
                await Navigation.PopAsync();
            }
        }
    }
}
