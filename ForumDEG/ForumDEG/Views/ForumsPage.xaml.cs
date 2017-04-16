using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ForumDEG.Views {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ForumsPage : ContentPage {
        public ForumsPage() {
            InitializeComponent();
        }

        protected async override void OnAppearing() {
            base.OnAppearing();

            ForumListView.ItemsSource = await Utils.ForumDatabase.getForumDB.GetAllForums();
        }

        private void ForumListView_ItemSelected(object sender, SelectedItemChangedEventArgs e) {

        }
    }
}
