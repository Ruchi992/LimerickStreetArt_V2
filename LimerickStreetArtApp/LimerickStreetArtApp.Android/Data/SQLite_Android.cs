using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using DeliveryBusiness_App.Data;
using DeliveryBusiness_App.Droid.Data;
using SQLite;

[assembly: Xamarin.Forms.Dependency(typeof(SQLite_Android))]
namespace DeliveryBusiness_App.Droid.Data
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