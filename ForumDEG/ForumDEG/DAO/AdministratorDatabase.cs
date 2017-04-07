using ForumDEG.Models;
using SQLite;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ForumDEG.Utils
{
    public class AdministratorDatabase
    {
        readonly SQLiteAsyncConnection _connection;

        public AdministratorDatabase(string databasePath)
        {
            _connection = new SQLiteAsyncConnection(databasePath);

            _connection.CreateTableAsync<Administrator>().Wait();
        }

        public Task<List<Administrator>> GetAllAdministrators()
        {
            return _connection.Table<Administrator>().ToListAsync();
        }

        public Task<Administrator> GetAdministrator(int id)
        {
            return _connection.Table<Administrator>().Where(i => i.Id == id).FirstOrDefaultAsync();
        }

        public Task<int> SaveAdministrator(Administrator newAdministrator) {

            if(newAdministrator.Id == 0) {
                return _connection.InsertAsync(newAdministrator);
            }
            else {
                return _connection.UpdateAsync(newAdministrator);
            }
        }

        public Task<int> DeleteAdministrator(Administrator administrator) {
            return _connection.DeleteAsync(administrator);
        }
    }
}
