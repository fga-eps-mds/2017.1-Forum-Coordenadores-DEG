using ForumDEG.Models;
using SQLite.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ForumDEG.Utils
{
    class AdministratorDatabase
    {
        private SQLiteConnection _connection;

        public AdministratorDatabase()
        {
            _connection = DependencyService.Get<InterfaceSQLite>().
                GetConnection();

            _connection.CreateTable<Administrator>();
        }

        public void AddAdministrator(string name, string registration, 
            string email, string password) {

            var newAdministrator = new Administrator {
                Name = name,
                Registration = registration,
                Email = email,
                Password = password,
                CreatedOn = DateTime.Now
            };
            _connection.Insert(newAdministrator);
        }
    }
}
