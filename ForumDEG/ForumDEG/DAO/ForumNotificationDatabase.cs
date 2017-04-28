using ForumDEG.Models;
using ForumDEG.Utils;
using SQLite;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ForumDEG.DAO {
    public class ForumNotificationDatabase {
        readonly SQLiteAsyncConnection _database;

        private static ForumNotificationDatabase _forumNotificationDatabase = null;

        private ForumNotificationDatabase(string databasePath) {
            _database = new SQLiteAsyncConnection(databasePath);

            _database.CreateTableAsync<ForumNotification>().Wait();
            Debug.WriteLine("FormAsksDatabase: Database created.");
        }

        public static ForumNotificationDatabase getFormDB {
            get {
                if (_forumNotificationDatabase == null) {
                    _forumNotificationDatabase = new ForumNotificationDatabase(DependencyService.Get<InterfaceSQLite>().GetLocalFilePath("ForumNotification.db3"));
                    Debug.WriteLine("ForumNotificationDatabase getFormDB: _forumNotificationDatabase getted.");
                }

                Debug.WriteLine("ForumNotificationDatabase getFormDB: returning _forumNotificationDatabase.");
                return _forumNotificationDatabase;
            }
        }

        public Task<List<ForumNotification>> GetAllForumNotifications() {
            return _database.Table<ForumNotification>().ToListAsync();
        }

        public Task<ForumNotification> GetForumNotification(int id) {
            return _database.Table<ForumNotification>().Where(i => i.Id == id).FirstOrDefaultAsync();
        }

        public Task<int> SaveForumNotification(ForumNotification newForumNotification) {

            if (newForumNotification.Id == 0) {
                return _database.InsertAsync(newForumNotification);
            } else {
                return _database.UpdateAsync(newForumNotification);
            }
        }

        public Task<int> DeleteForumNotification(ForumNotification forumNotification) {
            return _database.DeleteAsync(forumNotification);
        }
    }
}
