using System;
using GreenLeaf.Core.Common;
using GreenLeaf.Core.Data;
using SQLite;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using GreenLeaf.Core.Data.TableDefinitions;
using File = System.IO.File;

namespace GreenLeaf.Core.Utilities
{
    public class BasicData
    {
        //private SQLiteConnection dbConnection = GreenLeaf.Data.GreenLeafDatabase.testDbConnection;
        
        /// <summary>
        /// Copies the initial database to private storage.
        /// </summary>
        /// <returns></returns>
        public static bool CopyDateBase()
        {
            const string selectString = "SELECT [schema] FROM systemInfo";
            
            using (SQLiteConnection connection = Data.DataBaseAccess.DbConnection)
            {
                SQLiteCommand command = connection.CreateCommand(selectString);
                try
                {
                    SystemInfo result;
                    result = command.ExecuteQuery<SystemInfo>().FirstOrDefault();
                    if ((result?.Schema >= Data.DataBaseAccess.CurrentSchema)) return true;

                }
                catch (SQLiteException)
                {
                    //Thrown if the table doesnt exist, ie the database doesnt exist.  Just keep going if this pops
                }
            }            
            
            try
            {
                Assembly assembly = typeof(BasicData).GetTypeInfo().Assembly;
                Stream stream = assembly.GetManifestResourceStream("GreenLeaf.Core.greenleafDatabase.db");
                stream.Position = 0;
                byte[] buffer = new byte[stream.Length];
                for (int totalBytesCopied = 0; totalBytesCopied < stream.Length;)
                {
                    totalBytesCopied += stream.Read(buffer, totalBytesCopied,
                        Convert.ToInt32(stream.Length) - totalBytesCopied);
                }

                string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "greenLeaf.db");
                File.WriteAllBytes(dbPath, buffer);
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public static bool CopyDateBaseBack()
        {
            
            try
            {
                
                string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "greenLeaf.db");
                string storageLocation = @"D:\Development\GreenLeaf\GreenLeaf.Shared\greenleafLIVE.db";
                File.Copy(dbPath, storageLocation);
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }
    }
}
