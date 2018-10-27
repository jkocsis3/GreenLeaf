using GreenLeaf.Core.Data;
using GreenLeaf.Core.Interfaces;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GreenLeaf.Core
{
    public class Progress : IProgress
    {

        /// <summary>
        /// The Unique ID of the Plant
        /// </summary>
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        /// <summary>
        /// The Plant
        /// </summary>
        public int PlantId { get; set; }

        /// <summary>
        /// The image for the plant.
        /// </summary>
        public string Image { get; set; }

        /// <summary>
        /// Notes for the Progress Report
        /// </summary>
        public string Notes { get; set; }

        /// <summary>
        /// The Date the Progress Report was created.
        /// </summary>
        public DateTime Date { get; set; }
        
        /// <summary>
        /// The week number for the schedule
        /// </summary>
        public int WeekNum { get; set; }

        [Ignore]
        /// <summary>
        /// The ID of the schedule
        /// </summary>
        public int ScheduleID { get; set; }

        /// <summary>
        /// Delete the current Progress Report
        /// </summary>
        /// <returns>True if Successful</returns>
        public bool Delete()
        {
            const string deleteProgressSql = "DELETE FROM Progress where Id = ?";
            try
            {
                using (SQLiteConnection connection = DataBaseAccess.DbConnection)
                {
                    SQLiteCommand command = connection.CreateCommand(deleteProgressSql, Id);
                    return command.ExecuteNonQuery() > 0;
                }
            }
            catch (SQLiteException)
            {
                return false;
            }
        }

        /// <summary>
        /// Save the current Progress Report
        /// </summary>
        /// <returns>True if Successful</returns>
        public bool Save()
        {
            const string selectProgressSql = "SELECT Id FROM Progress where Id = ?";
            int result = 0;
            try
            {
                using (SQLiteConnection connection = DataBaseAccess.DbConnection)
                {
                    SQLiteCommand command = connection.CreateCommand(selectProgressSql, Id);
                    result = command.ExecuteScalar<int>();
                    return result > 0 ? Update() : Add(); 
                }
            }
            catch (SQLiteException)
            {
                return false;
            }
        }

        public Progress Find(int id)
        {
            const string selectProgressSql = "SELECT * FROM Progress where Id = ?";
            try
            {
                using (SQLiteConnection connection = DataBaseAccess.DbConnection)
                {
                    SQLiteCommand command = connection.CreateCommand(selectProgressSql, id);
                    return command.ExecuteQuery<Progress>().SingleOrDefault();
                }
            }
            catch (SQLiteException)
            {
                return null;
            }
        }

        public static IEnumerable<Progress> FindAllForPlant(int plantId)
        {
            const string selectProgressSql = "SELECT * FROM Progress where PlantId = ?";
            try
            {
                using (SQLiteConnection connection = DataBaseAccess.DbConnection)
                {
                    SQLiteCommand command = connection.CreateCommand(selectProgressSql, plantId);
                    return command.ExecuteQuery<Progress>();
                }
            }
            catch (SQLiteException)
            {
                return null;
            }
        }

        #region Private Methods
        private bool Add()
        {
            const string addProgressSql = "INSERT INTO Progress (Image, Notes, PlantId, WeekNum) VALUES(?,?,?,?)";
            try
            {
                using (SQLiteConnection connection = DataBaseAccess.DbConnection)
                {
                    SQLiteCommand command = connection.CreateCommand(addProgressSql, Image, Notes, PlantId, WeekNum);
                    int result = command.ExecuteNonQuery();
                    return result > 0;
                }
            }
            catch (SQLiteException)
            {
                return false;
            }
        }

        private bool Update()
        {
            const string addProgressSql = "Update Progress SET PlantId = ?, Image = ?, Notes = ? WHERE id = ?";
            try
            {
                using (SQLiteConnection connection = DataBaseAccess.DbConnection)
                {
                    SQLiteCommand command = connection.CreateCommand(addProgressSql, PlantId, Image, Notes, Id);
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
