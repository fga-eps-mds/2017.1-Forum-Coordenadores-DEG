using ForumDEG.Models;
using SQLite;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ForumDEG.Utils
{
    public class AdministratorDatabase
    {
        readonly SQLiteAsyncConnection _database;

        public AdministratorDatabase(string databasePath)
        {
            _database = new SQLiteAsyncConnection(databasePath);

            _database.CreateTableAsync<Administrator>().Wait();
        }

        public Task<List<Administrator>> GetAllAdministrators()
        {
            return _database.Table<Administrator>().ToListAsync();
        }

        public Task<Administrator> GetAdministrator(int id)
        {
            return _database.Table<Administrator>().Where(i => i.Id == id).FirstOrDefaultAsync();
        }

        public Task<int> SaveAdministrator(Administrator newAdministrator) {

            if(newAdministrator.Id == 0) {
                return _database.InsertAsync(newAdministrator);
            }
            else {
                return _database.UpdateAsync(newAdministrator);
            }
        }

        public Task<int> DeleteAdministrator(Administrator administrator) {
            return _database.DeleteAsync(administrator);
        }
    }
}
