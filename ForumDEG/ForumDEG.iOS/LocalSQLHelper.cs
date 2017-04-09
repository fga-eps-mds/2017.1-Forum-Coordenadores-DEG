using ForumDEG.iOS;
using ForumDEG;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using System.IO;
using Foundation;

[assembly: Dependency(typeof(LocalSQLHelper))]

namespace ForumDEG.iOS
{
    public class LocalSQLHelper : ForumDEG.Utils.InterfaceSQLite
    {
        public string GetLocalFilePath(string filename)
        {
            string docFolder = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            string libFolder = Path.Combine(docFolder, "..", "Library", "Databases");
            if (!Directory.Exists(libFolder)) {
                Directory.CreateDirectory(libFolder);
            }
            return Path.Combine(libFolder, filename);
        }
    }
}
