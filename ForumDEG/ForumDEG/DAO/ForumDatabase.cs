using ForumDEG.Models;
using SQLite;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ForumDEG.Utils
{
    public class ForumDatabase
    {
        readonly SQLiteAsyncConnection _database;

        private static ForumDatabase _forumDatabase = null;

        private ForumDatabase(string databasePath)
        {
            _database = new SQLiteAsyncConnection(databasePath);

            _database.CreateTableAsync<Forum>().Wait();
        }

        public static ForumDatabase getForumDB {
            get {
                if (_forumDatabase == null) {
                    _forumDatabase = new ForumDatabase(DependencyService.Get<InterfaceSQLite>().GetLocalFilePath("Forum.db3"));
                }

                return _forumDatabase;
            }
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
