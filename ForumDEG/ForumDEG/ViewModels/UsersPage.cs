using ForumDEG.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ForumDEG.ViewModels
{
    public class UsersPage: ContentPage
    {
        private UserDatabase _database;
        private ListView _userList;

        public UsersPage (UserDatabase database) {
            _database = database;
            Title = "Usuários";
            var users = _database.GetAllUsers();

            _userList = new ListView();
            _userList.ItemsSource = users;
            _userList.ItemTemplate = new DataTemplate(typeof(TextCell));
            _userList.ItemTemplate.SetBinding(TextCell.TextProperty, "Name");
            _userList.ItemTemplate.SetBinding(TextCell.TextProperty, "Registration");
            _userList.ItemTemplate.SetBinding(TextCell.TextProperty, "Email");
            _userList.ItemTemplate.SetBinding(TextCell.TextProperty, "Passowrd");
            _userList.ItemTemplate.SetBinding(TextCell.DetailProperty, "CreatedOn");

            var toolbarItem = new ToolbarItem   {
                Name = "Add",
                Command = new Command(() => Navigation.PushAsync(new UserEntryPage(this, (AdministratorDatabase)database)))
            };

            ToolbarItems.Add(toolbarItem);

            Content = _userList;
        }

        public void Refresh()   {
            _userList.ItemsSource = _database.GetAllUsers();
        }
    }
}
