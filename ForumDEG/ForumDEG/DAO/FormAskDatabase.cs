using ForumDEG.Models;
using ForumDEG.Utils;
using SQLite;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;
using ForumDEG.Interfaces;

namespace ForumDEG.DAO {
    public class FormAskDatabase : IDatabase<FormAsk> {

        readonly SQLiteAsyncConnection _database;

        private static FormAskDatabase _formAskDatabase = null;

        private FormAskDatabase(string databasePath) {
            _database = new SQLiteAsyncConnection(databasePath);

            _database.CreateTableAsync<FormAsk>().Wait();
            Debug.WriteLine("FormAskDatabase: Database created.");
        }

        public static FormAskDatabase getFormDB {
            get {
                if (_formAskDatabase == null) {
                    _formAskDatabase = new FormAskDatabase(DependencyService.Get<ISQLite>().GetLocalFilePath("FormAsk.db3"));
                    Debug.WriteLine("FormAskDatabase getFormDB: _formAskDatabase getted.");
                }

                Debug.WriteLine("FormAskDatabase getFormDB: returning _formAskDatabase.");
                return _formAskDatabase;
            }
        }

        public Task<List<FormAsk>> GetAll() {
            return _database.Table<FormAsk>().ToListAsync();
        }

        public Task<FormAsk> Get(int id) {
            return _database.Table<FormAsk>().Where(i => i.Id == id).FirstOrDefaultAsync();
        }

        public Task<int> Save(FormAsk newFormAsk) {

            if (newFormAsk.Id == 0) {
                return _database.InsertAsync(newFormAsk);
            } else {
                return _database.UpdateAsync(newFormAsk);
            }
        }

        public Task<int> Delete(FormAsk formAsk) {
            return _database.DeleteAsync(formAsk);
        }
    }
}
