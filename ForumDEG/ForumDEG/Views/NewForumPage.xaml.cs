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
        }

        private void OnNewForumButtonClicked(object sender, EventArgs e) {
            var forum = new Models.Forum {
                _title = titleEntry.Text,
                _place = placeEntry.Text,
                _date = dateDatePicker.Date,
                _hour = timeTimePicker.Time,
                _schedules = schedulesEditor.Text
            };
            DisplayAlert("Fórum Criado", "Título: " + forum._title 
                + "\nLocal: " + forum._place 
                + "\nData: " + forum._date.ToString("dd/MM/yyyy")
                + "\nHora: " + forum._hour.ToString()
                + "\nPautas:\n" + forum._schedules
                , "OK");
        }
    }
}
