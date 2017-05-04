using ForumDEG.Interfaces;
using ForumDEG.Models;
using ForumDEG.Utils;
using SQLite;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ForumDEG.DAO {
    public class ForumConfirmationDatabase : IDatabase<ForumConfirmation> {
        readonly SQLiteAsyncConnection _database;

        private static ForumConfirmationDatabase _forumConfirmationDatabase = null;

        public ForumConfirmationDatabase(string databasePath) {
            _database = new SQLiteAsyncConnection(databasePath);

            _database.CreateTableAsync<ForumConfirmation>().Wait();
        }

        public static ForumConfirmationDatabase getForumConfirmationDB {
            get {
                if (_forumConfirmationDatabase == null) {
                    _forumConfirmationDatabase = new ForumConfirmationDatabase(DependencyService.Get<ISQLite>().GetLocalFilePath("ForumConfirmation.db3"));
                }

                return _forumConfirmationDatabase;
            }
        }

        public Task<List<ForumConfirmation>> GetAll() {
            return _database.Table<ForumConfirmation>().ToListAsync();
        }

        public Task<ForumConfirmation> Get(int id) {
            return _database.Table<ForumConfirmation>().Where(i => i.Id == id).FirstOrDefaultAsync();
        }

        public Task<int> Save(ForumConfirmation newForumConfirmation) {

            if (newForumConfirmation.Id == 0) {
                return _database.InsertAsync(newForumConfirmation);
            } else {
                return _database.UpdateAsync(newForumConfirmation);
            }
        }

        public Task<int> Delete(ForumConfirmation forumConfirmation) {
            return _database.DeleteAsync(forumConfirmation);
        }
    }
}
