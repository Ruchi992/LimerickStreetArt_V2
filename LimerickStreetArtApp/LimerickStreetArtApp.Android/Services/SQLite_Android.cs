
using System.IO;

using LimerickStreetArtApp.Data;
using LimerickStreetArtApp.Droid.Services;
using SQLite;

[assembly: Xamarin.Forms.Dependency(typeof(SQLite_Android))]
namespace LimerickStreetArtApp.Droid.Services
{
    class SQLite_Android : ISQLite
    {
        public SQLiteConnection GetConnection()
        {
            string sqliteFileName = "AppDB.db3";

            string documentPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);

            string path = Path.Combine(documentPath, sqliteFileName);

            var conn = new SQLiteConnection(path);

            return conn;
        }
    }
}