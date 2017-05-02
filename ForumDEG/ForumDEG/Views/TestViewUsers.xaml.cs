using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ForumDEG.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TestViewUsers : ContentPage
    {
        public TestViewUsers()
        {
            InitializeComponent();
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            AdministratorListView.ItemsSource = await App.AdministratorDatabase.GetAll();
            CoordinatorListView.ItemsSource = await App.CoordinatorDatabase.GetAll();
        }

        private void CoordListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            return;
        }

        private void AdministratorListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            return;
        }
    }
}
