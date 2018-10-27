using SQLite;

using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace GreenLeaf.Core.Utilities
{
    static class TestData
    {
        public static bool CreateDatabase()
        {
            try
            {
                Assembly assembly = typeof(BasicData).GetTypeInfo().Assembly;
                Stream stream = assembly.GetManifestResourceStream("GreenLeaf.Core.greenleafDatabase.db");
                stream.Position = 0;
                byte[] buffer = new byte[stream.Length];
                //int totalBytesCopied = 0;
                for (int totalBytesCopied = 0; totalBytesCopied < stream.Length;)
                {
                    totalBytesCopied += stream.Read(buffer, totalBytesCopied,
                        Convert.ToInt32(stream.Length) - totalBytesCopied);
                }

                string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "greenLeaf.db");
                File.WriteAllBytes(dbPath, buffer);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        
    }
}
