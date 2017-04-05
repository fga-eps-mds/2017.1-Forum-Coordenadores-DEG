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
    public class UserDatabase  {
        private SQLiteConnection _connection;

        public UserDatabase () {
            _connection = DependencyService.Get<InterfaceSQLite>().
                GetConnection();

            _connection.CreateTable<User>();
        }

        public IEnumerable<User> GetAllUsers() {
            return (from t in _connection.Table<User>() select t).ToList();
        }

        public IEnumerable<User> GetAllAdministrators()
        {
            return (from t in _connection.Table<Administrator>() select t).ToList();
        }

        internal void AddAdministrator(object thought)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<User> GetAllCoordinators()
        {
            return (from t in _connection.Table<Coordinator>() select t).ToList();
        }

        public IEnumerable<User> GetUser(int id) {
            yield return _connection.Table<User>().FirstOrDefault(t => t.id == id);
        }

        public void DeleteUser(int id) {
            _connection.Delete<User>(id);
        }
    }
}
