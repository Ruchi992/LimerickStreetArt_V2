
using LimerickStreetArtApp.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace LimerickStreetArtApp.Data
{
    public class UserDatabaseController
    {
        static object locker = new object();

        SQLiteConnection database;

        public UserDatabaseController()
        {
            database = DependencyService.Get<ISQLite>().GetConnection();
            database.CreateTable<UserAccount>();
        }

        public UserAccount GetUser()
        {
            if (database.Table<UserAccount>().Count() == 0)
            {
                return null;
            }
            else
            {
                return database.Table<UserAccount>().First();
            }
        }

        public void SaveUser(UserAccount user)
        {
            lock (locker)
            {
                try
                {
                    database.DeleteAll<UserAccount>();
                    UserAccount dbUser = database.Table<UserAccount>().FirstOrDefault(dbu => dbu.Username == user.Username);

                    if (dbUser == null)
                    {
                        dbUser = user;

                        database.Insert(dbUser);
                    }
                    else
                    {
                        database.Update(user);
                    }
                }catch(Exception e)
                {

                }
            }
        }

        public int DeleteUser(int id)
        {
            lock (locker)
            {
                return database.Delete<UserAccount>(id);
            }
        }
    }
}
