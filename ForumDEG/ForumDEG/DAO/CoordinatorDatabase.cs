using ForumDEG.Models;
using SQLite;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ForumDEG.Utils
{
    public class CoordinatorDatabase
    {
        readonly SQLiteAsyncConnection _connection;

        public CoordinatorDatabase(string databasePath)
        {
            _connection = new SQLiteAsyncConnection(databasePath);

            _connection.CreateTableAsync<Coordinator>().Wait();
        }

        public Task<List<Coordinator>> GetAllCoordinators()
        {
            return _connection.Table<Coordinator>().ToListAsync();
        }

        public Task<Coordinator> GetCoordinator(int id)
        {
            return _connection.Table<Coordinator>().Where(i => i.Id == id).FirstOrDefaultAsync();
        }

        public Task<int> SaveCoordinator(Coordinator newCoordinator)
        {

            if (newCoordinator.Id != 0)
            {
                return _connection.InsertAsync(newCoordinator);
            }
            else
            {
                return _connection.UpdateAsync(newCoordinator);
            }
        }

        public Task<int> DeleteCoordinator(Coordinator coordinator)
        {
            return _connection.DeleteAsync(coordinator);
        }
    }
}
