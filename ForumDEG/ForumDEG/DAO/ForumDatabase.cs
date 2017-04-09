using ForumDEG.Models;
using SQLite;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ForumDEG.Utils
{
    public class ForumDatabase
    {
        readonly SQLiteAsyncConnection _database;

        public ForumDatabase(string databasePath)
        {
            _database = new SQLiteAsyncConnection(databasePath);

            _database.CreateTableAsync<Forum>().Wait();
        }

        public Task<List<Forum>> GetAllForums()
        {
            return _database.Table<Forum>().ToListAsync();
        }

        public Task<Forum> GetForum(int id)
        {
            return _database.Table<Forum>().Where(i => i.Id == id).FirstOrDefaultAsync();
        }

        public Task<int> SaveForum(Forum newForum)
        {

            if (newForum.Id == 0)
            {
                return _database.InsertAsync(newForum);
            }
            else
            {
                return _database.UpdateAsync(newForum);
            }
        }

        public Task<int> DeleteForum(Forum Forum)
        {
            return _database.DeleteAsync(Forum);
        }
    }
}
