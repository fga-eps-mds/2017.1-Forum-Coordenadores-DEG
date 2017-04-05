using ForumDEG.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ForumDEG.ViewModels
{
    public class UserEntryPage : ContentPage
    {
        private UsersPage _parent;
        private AdministratorDatabase _database;

        public UserEntryPage(UsersPage parent, AdministratorDatabase database)
        {
            _parent = parent;
            _database = database;
            Title = "Add New Administrator";

            var _name = new Entry();
            var _registration = new Entry();
            var _email = new Entry();
            var _password = new Entry();
            var button = new Button
            {
                Text = "Add"
            };

            button.Clicked += async (object sender, EventArgs e) => {
                var name = _name.Text;
                var registration = _registration.Text;
                var email = _email.Text;
                var passowrd = _password.Text;

                _database.AddAdministrator(name, registration, email, passowrd);

                await Navigation.PopAsync();


                _parent.Refresh();
            };

            Content = new StackLayout
            {
                Spacing = 20,
                Padding = new Thickness(20),
                Children = { _name, button },   
            };
        }
    }
}
