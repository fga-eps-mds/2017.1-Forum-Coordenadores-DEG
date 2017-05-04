using ForumDEG.Models;
using SQLite;
using System.Collections.Generic;
using System.Threading.Tasks;
using ForumDEG.Interfaces;
using Xamarin.Forms;

namespace ForumDEG.Utils
{
    public class CoordinatorDatabase : IDatabase<Coordinator>
    {
        readonly SQLiteAsyncConnection _database;

        private static CoordinatorDatabase _coordinatorDatabase = null;

        public CoordinatorDatabase(string databasePath)
        {
            _database = new SQLiteAsyncConnection(databasePath);

            _database.CreateTableAsync<Coordinator>().Wait();
        }

        public static CoordinatorDatabase getCoordinatorDB {
            get {
                if (_coordinatorDatabase == null) {
                    _coordinatorDatabase = new CoordinatorDatabase(DependencyService.Get<ISQLite>().GetLocalFilePath("Coordinator.db3"));
                }
                return _coordinatorDatabase;
            }
        }

        public Task<List<Coordinator>> GetAll()
        {
            return _database.Table<Coordinator>().ToListAsync();
        }

        public Task<Coordinator> Get(int id)
        {
            return _database.Table<Coordinator>().Where(i => i.Id == id).FirstOrDefaultAsync();
        }

        public Task<int> Save(Coordinator newCoordinator)
        {

            if (newCoordinator.Id == 0)
            {
                return _database.InsertAsync(newCoordinator);
            }
            else
            {
                return _database.UpdateAsync(newCoordinator);
            }
        }

        public Task<int> Delete(Coordinator coordinator)
        {
            return _database.DeleteAsync(coordinator);
        }
    }
}
