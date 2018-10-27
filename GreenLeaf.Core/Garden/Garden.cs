using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GreenLeaf.Core.Data;
using GreenLeaf.Core.Data.TableDefinitions;
using GreenLeaf.Core.Interfaces;
using SQLite;

namespace GreenLeaf.Core
{
    [Table("Garden")]
    public class Garden : IGarden
    {
        #region Fields

        /// <summary>
        /// The Unique identifier of the table
        /// </summary>
        [AutoIncrement, PrimaryKey]
        public int Id { get; set; }

        /// <summary>
        /// The name of the garden.
        /// </summary>
        public string Name { get; set; }

        #endregion

        #region Public Methods

        /// <summary>
        /// Finds a single Garden
        /// </summary>
        /// <param name="id">The ID of the Garden</param>
        /// <returns>A <see cref="Garden"/></returns>
        public static Garden Find(int id)
        {
            try
            {
                using (SQLiteConnection connection = Data.DataBaseAccess.DbConnection)
                {
                    const string selectSql = "SELECT * FROM Garden WHERE Id = ?";
                    SQLiteCommand command = connection.CreateCommand(selectSql, id);
                    return command.ExecuteQuery<Garden>().SingleOrDefault();
                }
            }
            catch (SQLiteException)
            {
                return null;
            }
        }

        public static IEnumerable<Garden> FindAll()
        {
            try
            {
                const string selectSql = "SELECT * FROM Garden";
                using (SQLiteConnection connection = Data.DataBaseAccess.DbConnection)
                {
                    SQLiteCommand command = connection.CreateCommand(selectSql);
                    return command.ExecuteQuery<Garden>();
                }
            }
            catch (SQLiteException)
            {
                return null;
            }
                
            
        }

        /// <summary>
        /// Gets all the plants in a garden
        /// </summary>
        /// <returns>An Enumerable of <see cref="Plants"/></returns>
        public IEnumerable<Plant> GetPlantsForGarder()
        {
            try
            {
                const string selectPlantIds = "SELECT * FROM Plant WHERE GardenId = ?";
                List<Plant> plants = new List<Plant>();
                using (SQLiteConnection connection = Data.DataBaseAccess.DbConnection)
                {
                    SQLiteCommand command = connection.CreateCommand(selectPlantIds, Id);
                    return command.ExecuteQuery<Plant>();
                }
            }
            catch (SQLiteException)
            {

                return null; ;
            }
        }

        /// <summary>
        /// Saves the current Garden
        /// </summary>
        /// <returns>True for a successful save</returns>
        public bool Save()
        {
            const string selectGardenSql = "SELECT Id FROM Garden WHERE ID = ?";
            int result = 0;
            try
            {
                using (SQLiteConnection connection = Data.DataBaseAccess.DbConnection)
                {
                    SQLiteCommand command = connection.CreateCommand(selectGardenSql, Id);
                    result = command.ExecuteScalar<int>();
                }
                return result > 0 ? Update() : Add();
            }
            catch (SQLiteException)
            {
                return false;
            }
        }

        /// <summary>
        /// Deletes the Garden.
        /// </summary>
        /// <returns>True for a successful deletion</returns>
        public bool Delete()
        {
            const string selectGardenSql = "Delete FROM Garden WHERE ID = ?";
            int result = 0;
            try
            {
                using (SQLiteConnection connection = Data.DataBaseAccess.DbConnection)
                {
                    SQLiteCommand command = connection.CreateCommand(selectGardenSql, Id);
                    result = command.ExecuteNonQuery();
                }
                return result > 0;
            }
            catch (SQLiteException)
            {
                return false;
            }
        }

        #endregion

        #region Private Methods
        private bool Update()
        {
            const string updateGardenSql = "UPDATE Garden SET Id = ?, Name = ? WHERE ID = ? ";
            try
            {
                using (SQLiteConnection connection = Data.DataBaseAccess.DbConnection)
                {
                    SQLiteCommand command = connection.CreateCommand(updateGardenSql, Id, Name, Id);
                    int result = command.ExecuteNonQuery();
                    return result > 0;
                }
            }
            catch (SQLiteException)
            {
                return false;
            }
        }

        private bool Add()
        {
            const string addGardenSql = "INSERT INTO Garden (Name) VALUES (?)";
            try
            {
                using (SQLiteConnection connection = Data.DataBaseAccess.DbConnection)
                {
                    SQLiteCommand command = connection.CreateCommand(addGardenSql, Name);
                    int result = command.ExecuteNonQuery();
                    return result > 0;
                }
            }
            catch (SQLiteException)
            {
                return false;
            }
        }


        #endregion


    }
}
