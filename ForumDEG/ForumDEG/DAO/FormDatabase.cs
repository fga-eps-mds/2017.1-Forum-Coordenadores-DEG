using ForumDEG.Models;
using ForumDEG.Utils;
using SQLite;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;
using ForumDEG.Interfaces;
using System.Collections.Generic;

namespace ForumDEG.DAO {
    public class FormDatabase : IDatabase<Form> {
        readonly SQLiteAsyncConnection _database;

        private static FormDatabase _formDatabase = null;

        private FormDatabase(string databasePath) {
            _database = new SQLiteAsyncConnection(databasePath);

            _database.CreateTableAsync<Form>().Wait();
            Debug.WriteLine("FormDatabase: Database created.");
        }

        public static FormDatabase getFormDB {
            get {
                if (_formDatabase == null) {
                    _formDatabase = new FormDatabase(DependencyService.Get<ISQLite>().GetLocalFilePath("Form.db3"));
                    Debug.WriteLine("FormDatabase getFormDB: _formDatabase getted.");
                }

                Debug.WriteLine("FormDatabase getFormDB: returning _formDatabase.");
                return _formDatabase;
            }
        }

        public Task<List<Form>> GetAll() {
            return _database.Table<Form>().ToListAsync();
        }

        public Task<Form> Get (int id) {
            return _database.Table<Form>().Where(i => i.Id == id).FirstOrDefaultAsync();
        }

        public Task<int> Save(Form newForm) {

            if (newForm.Id == 0) {
                return _database.InsertAsync(newForm);
            } else {
                return _database.UpdateAsync(newForm);
            }
        }

        public Task<int> Delete(Form form) {
            return _database.DeleteAsync(form);
        }
    }
}
