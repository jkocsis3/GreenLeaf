using SQLite;
using System;
using System.IO;

namespace GreenLeaf.Core.Data
{
    public static class DataBaseAccess
    {
        public static string DbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "greenLeaf.db");
        public static SQLiteConnection DbConnection => new SQLiteConnection(DbPath);

        public static double CurrentSchema = 2.0;
        
    }
}
