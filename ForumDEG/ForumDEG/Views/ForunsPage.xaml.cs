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
        
        public ForunsPage() {
            InitializeComponent();
            forumList.ItemsSource = ForunsViewModel.GetInstance().GetUpdatedList();
        }

        void Handle_Refreshing(object sender, System.EventArgs e) {
            forumList.ItemsSource = ForunsViewModel.GetInstance().GetUpdatedList();
            forumList.EndRefresh();
        }

        private async void forumList_ItemSelected(object sender, SelectedItemChangedEventArgs e) {
            ForunsViewModel.GetInstance().Select(e.SelectedItem);
            await Navigation.PushAsync(new ForumDetailPage());
        }
    }
}
