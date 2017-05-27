using ForumDEG.Models;
using SQLite;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;
using ForumDEG.Interfaces;

namespace ForumDEG.Utils
{
    public class ForumDatabase : IDatabase<Forum>
    {
        readonly SQLiteAsyncConnection _database;

        private static ForumDatabase _forumDatabase = null;

        public ForumDatabase(string databasePath)
        {
            _database = new SQLiteAsyncConnection(databasePath);

            _database.CreateTableAsync<Forum>().Wait();
        }

        public static ForumDatabase getForumDB {
            get {
                if (_forumDatabase == null) {
                    _forumDatabase = new ForumDatabase(DependencyService.Get<ISQLite>().GetLocalFilePath("Forum.db3"));
                }

                return _forumDatabase;
            }
        }

        public Task<List<Forum>> GetAll() {
            return _database.Table<Forum>().ToListAsync();
        }

        public Task<Forum> Get(int id) {
            return _database.Table<Forum>().Where(i => i.Id == id).FirstOrDefaultAsync();
        }

        public Task<int> Save(Forum newForum) {

            if (newForum.Id == 0) {
                return _database.InsertAsync(newForum);
            }
            else {
                return _database.UpdateAsync(newForum);
            }
        }

        public Task<int> Delete(Forum Forum) {
            return _database.DeleteAsync(Forum);
        }
    }
}
