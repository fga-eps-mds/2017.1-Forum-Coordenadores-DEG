using ForumDEG.Models;
using SQLite;
using System.Collections.Generic;
using System.Threading.Tasks;
using ForumDEG.Interfaces;

namespace ForumDEG.Utils
{
    public class CoordinatorDatabase : IDatabase<Coordinator>
    {
        readonly SQLiteAsyncConnection _database;

        public CoordinatorDatabase(string databasePath)
        {
            _database = new SQLiteAsyncConnection(databasePath);

            _database.CreateTableAsync<Coordinator>().Wait();
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
