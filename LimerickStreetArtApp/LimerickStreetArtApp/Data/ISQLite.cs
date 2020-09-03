using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace LimerickStreetArtApp.Data
{
    public interface ISQLite
    {
        SQLiteConnection GetConnection();

    }
}
