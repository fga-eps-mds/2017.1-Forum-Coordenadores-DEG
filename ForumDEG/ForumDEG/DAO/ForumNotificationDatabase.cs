using ForumDEG.Models;
using ForumDEG.Utils;
using SQLite;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;
using ForumDEG.Interfaces;

namespace ForumDEG.DAO {
    public class ForumNotificationDatabase : IDatabase<ForumNotification> {
        readonly SQLiteAsyncConnection _database;

        private static ForumNotificationDatabase _forumNotificationDatabase = null;

        private ForumNotificationDatabase(string databasePath) {
            _database = new SQLiteAsyncConnection(databasePath);

            _database.CreateTableAsync<ForumNotification>().Wait();
            Debug.WriteLine("FormAsksDatabase: Database created.");
        }

        public static ForumNotificationDatabase getForumNotificationDB {
            get {
                if (_forumNotificationDatabase == null) {
                    _forumNotificationDatabase = new ForumNotificationDatabase(DependencyService.Get<ISQLite>().GetLocalFilePath("ForumNotification.db3"));
                    Debug.WriteLine("ForumNotificationDatabase getFormDB: _forumNotificationDatabase getted.");
                }

                Debug.WriteLine("ForumNotificationDatabase getFormDB: returning _forumNotificationDatabase.");
                return _forumNotificationDatabase;
            }
        }

        public Task<List<ForumNotification>> GetAll() {
            return _database.Table<ForumNotification>().ToListAsync();
        }

        public Task<ForumNotification> Get(int id) {
            return _database.Table<ForumNotification>().Where(i => i.Id == id).FirstOrDefaultAsync();
        }

        public Task<int> Save(ForumNotification newForumNotification) {

            if (newForumNotification.Id == 0) {
                return _database.InsertAsync(newForumNotification);
            } else {
                return _database.UpdateAsync(newForumNotification);
            }
        }

        public Task<int> Delete(ForumNotification forumNotification) {
            return _database.DeleteAsync(forumNotification);
        }
    }
}
