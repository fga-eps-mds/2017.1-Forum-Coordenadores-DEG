using ForumDEG.Interfaces;
using ForumDEG.Models;
using SQLite;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ForumDEG.Utils {
    public class AdministratorDatabase : IDatabase<Administrator>
    {
        readonly SQLiteAsyncConnection _database;

        private static AdministratorDatabase _administratorDatabase = null;

        public AdministratorDatabase(string databasePath)
        {
            _database = new SQLiteAsyncConnection(databasePath);

            _database.CreateTableAsync<Administrator>().Wait();
        }

        public static AdministratorDatabase getAdmDB {
            get {
                if (_administratorDatabase == null) {
                    _administratorDatabase = new AdministratorDatabase(DependencyService.Get<ISQLite>().GetLocalFilePath("Administrator.db3"));
                }
                return _administratorDatabase;
            }
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
            Debug.WriteLine("Inside adm save");
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
