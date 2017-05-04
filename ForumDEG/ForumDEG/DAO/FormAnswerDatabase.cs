using ForumDEG.Models;
using ForumDEG.Utils;
using SQLite;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;
using ForumDEG.Interfaces;

namespace ForumDEG.DAO {
    public class FormAnswerDatabase : IDatabase<FormAnswer> {

        readonly SQLiteAsyncConnection _database;

        private static FormAnswerDatabase _FormAnswerDatabase = null;

        private FormAnswerDatabase(string databasePath) {
            _database = new SQLiteAsyncConnection(databasePath);

            _database.CreateTableAsync<FormAnswer>().Wait();
            Debug.WriteLine("FormAnswerDatabase: Database created.");
        }

        public static FormAnswerDatabase getFormDB {
            get {
                if (_FormAnswerDatabase == null) {
                    _FormAnswerDatabase = new FormAnswerDatabase(DependencyService.Get<ISQLite>().GetLocalFilePath("FormAnswers.db3"));
                    Debug.WriteLine("FormAnswerDatabase getFormDB: _FormAnswerDatabase getted.");
                }

                Debug.WriteLine("FormAnswerDatabase getFormDB: returning _FormAnswerDatabase.");
                return _FormAnswerDatabase;
            }
        }

        public Task<List<FormAnswer>> GetAll() {
            return _database.Table<FormAnswer>().ToListAsync();
        }

        public Task<FormAnswer> Get(int id) {
            return _database.Table<FormAnswer>().Where(i => i.Id == id).FirstOrDefaultAsync();
        }

        public Task<int> Save(FormAnswer newFormAnswers) {

            if (newFormAnswers.Id == 0) {
                return _database.InsertAsync(newFormAnswers);
            } else {
                return _database.UpdateAsync(newFormAnswers);
            }
        }

        public Task<int> Delete(FormAnswer formAnswers) {
            return _database.DeleteAsync(formAnswers);
        }
    }
}
