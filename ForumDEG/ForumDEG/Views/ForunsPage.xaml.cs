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
    public partial class ForunsPage : ContentPage {
        private ForunsViewModel _fvm;
        
        public ForunsPage() {
            InitializeComponent();

            _fvm = new ForunsViewModel();
            forumList.ItemsSource = _fvm.GetUpdatedList();
        }

        void Handle_Refreshing(object sender, System.EventArgs e) {
            forumList.ItemsSource = _fvm.GetUpdatedList();
            forumList.EndRefresh();
        }

        void Handle_Selected(object sender, System.EventArgs e) {
            if (e == null) return;
            ((ListView)sender).SelectedItem = null;
        }
    }
}
