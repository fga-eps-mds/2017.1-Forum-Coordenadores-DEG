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
    public class CoordinatorDatabase
    {
        private SQLiteConnection _connection;

        public CoordinatorDatabase()
        {
            _connection = DependencyService.Get<InterfaceSQLite>().
                GetConnection();

            _connection.CreateTable<Coordinator>();
        }

        public void AddCoordinator(string name, string registration, 
            string course, string email, string password) {

            var newCoordinator = new Coordinator {
                Name = name,
                Registration = registration,
                Course = course,
                Email = email,
                Password = password,
                CreatedOn = DateTime.Now
            };
            _connection.Insert(newCoordinator);
        }
    }
}
