using ForumDEG.Interfaces;
using ForumDEG.Models;
using SQLite;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ForumDEG.DAO {
    public class ForumConfirmationDatabase : IDatabase<ForumConfirmation> {
        readonly SQLiteAsyncConnection _database;

        public ForumConfirmationDatabase(string databasePath) {
            _database = new SQLiteAsyncConnection(databasePath);

            _database.CreateTableAsync<ForumConfirmation>().Wait();
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
