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
            BindingContext = new Models.Forum();
            dateDatePicker.Date = DateTime.Now;
            dateDatePicker.MinimumDate = DateTime.Now;
        }
        
        private async void OnNewForumButtonClicked(object sender, EventArgs e) {

            var forum = (Models.Forum)BindingContext;

            await App.ForumDatabase.SaveForum(forum);
            await DisplayAlert("Fórum Criado", "Título: " + forum._title 
                + "\nLocal: " + forum._place 
                + "\nData: " + forum._date.ToString("dd/MM/yyyy")
                + "\nHora: " + forum._hour.ToString()
                + "\nPautas:\n" + forum._schedules 
                , "OK");

            Navigation.InsertPageBefore(new ForumsPage(), this);
            await Navigation.PopAsync();
        }
    }
}
