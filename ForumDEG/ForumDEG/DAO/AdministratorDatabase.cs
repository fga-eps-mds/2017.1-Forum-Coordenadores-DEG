using ForumDEG.Interfaces;
using ForumDEG.Models;
using SQLite;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ForumDEG.Utils {
    public class AdministratorDatabase : IDatabase<Administrator>
    {
        readonly SQLiteAsyncConnection _database;

        public AdministratorDatabase(string databasePath)
        {
            _database = new SQLiteAsyncConnection(databasePath);

            _database.CreateTableAsync<Administrator>().Wait();
        }

        public Task<List<Administrator>> GetAll()
        {
            return _database.Table<Administrator>().ToListAsync();
        }

        public Task<Administrator> Get(int id)
        {
            return _database.Table<Administrator>().Where(i => i.Id == id).FirstOrDefaultAsync();
        }

        public Task<int> Save(Administrator newAdministrator) {

            if(newAdministrator.Id == 0) {
                return _database.InsertAsync(newAdministrator);
            }
            else {
                return _database.UpdateAsync(newAdministrator);
            }
        }

        public Task<int> Delete(Administrator administrator) {
            return _database.DeleteAsync(administrator);
        }
    }
}
