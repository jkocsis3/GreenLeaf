using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace GreenLeaf.Core.Unit.Tests.Data
{
    class TestData
    {
        public static string TestDbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "greenLeafTest.db");
        public static SQLiteConnection TestDbConnection => new SQLiteConnection(TestDbPath);

        public static bool CopyDateBase()
        {            
            try
            {
                Assembly assembly = typeof(TestData).GetTypeInfo().Assembly;
                Stream stream = assembly.GetManifestResourceStream("GreenLeaf.Core.greenleafDatabaseTest.db");
                stream.Position = 0;
                byte[] buffer = new byte[stream.Length];
                for (int totalBytesCopied = 0; totalBytesCopied < stream.Length;)
                {
                    totalBytesCopied += stream.Read(buffer, totalBytesCopied,
                        Convert.ToInt32(stream.Length) - totalBytesCopied);
                }

                string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "greenLeafTest.db");
                File.WriteAllBytes(dbPath, buffer);
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }
    }
}
