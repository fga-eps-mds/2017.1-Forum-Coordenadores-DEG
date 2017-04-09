using ForumDEG.Models;
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
    public partial class AdministratorsPage : ContentPage
    {
        public AdministratorsPage()
        {
            InitializeComponent();
            this.Title = "Administrator List";

            var toolbarItem = new ToolbarItem {
                Text = "+"
            };

            toolbarItem.Clicked += async (sender, e) => {
                await Navigation.PushAsync(new AdministratorAddPage() { BindingContext = new Administrator()});
            };

            ToolbarItems.Add(toolbarItem);
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            AdministratorListView.ItemsSource = await App.AdministratorDatabase.GetAllAdministrators();
        }

        async void Administrator_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if(e.SelectedItem != null) {
                await Navigation.PushAsync(new AdministratorAddPage() { BindingContext = e.SelectedItem as Administrator });
            }
        }
    }
}
