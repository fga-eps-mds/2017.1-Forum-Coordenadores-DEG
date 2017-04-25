using ForumDEG.Models;
using ForumDEG.Utils;
using SQLite;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ForumDEG.DAO {
    class FormAsksDatabase {

        readonly SQLiteAsyncConnection _database;

        private static FormAsksDatabase _formAsksDatabase = null;

        private FormAsksDatabase(string databasePath) {
            _database = new SQLiteAsyncConnection(databasePath);

            _database.CreateTableAsync<FormAsks>().Wait();
            Debug.WriteLine("FormAsksDatabase: Database created.");
        }

        public static FormAsksDatabase getFormDB {
            get {
                if (_formAsksDatabase == null) {
                    _formAsksDatabase = new FormAsksDatabase(DependencyService.Get<InterfaceSQLite>().GetLocalFilePath("FormAsks.db3"));
                    Debug.WriteLine("FormAsksDatabase getFormDB: _formAsksDatabase getted.");
                }

                Debug.WriteLine("FormAsksDatabase getFormDB: returning _formAsksDatabase.");
                return _formAsksDatabase;
            }
        }
    }
}
