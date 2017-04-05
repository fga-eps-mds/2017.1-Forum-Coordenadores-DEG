using System;
using Xamarin.Forms;
using ForumDEG.Utils;
using System.IO;

[assembly: Dependency(typeof(SQLite_Android))]

namespace ForumDEG.Utils
{
    public class SQLite_Android
    {
        public SQLite_Android()
        {
        }

        public SQLite.Net.SQLiteConnection GetConnection()
        {
            var fileName = "User.db3";
            var documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            var libraryPath = Path.Combine(documentsPath, fileName);
            var path = Path.Combine(libraryPath, fileName);

            var platform = new SQLite.Net.Platform.XamarinAndroid.SQLitePlatformAndroid();
            var connection = new SQLite.Net.SQLiteConnection(platform, path);
            return connection;
        }
    }
}
