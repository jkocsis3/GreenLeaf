using GreenLeaf.Core.Common;
using GreenLeaf.Core.Data;
using GreenLeaf.Core.Interfaces;
using SQLite;
using System.Collections.Generic;
using System.Linq;

namespace GreenLeaf.Core
{
    public class Strain : IStrain
    {
        #region Fields
        /// <summary>
        /// The Unique identifier
        /// </summary>
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        /// <summary>
        /// The name of the strain
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The strain's genetics
        /// </summary>
        public Genetics Genetics { get; set; }

        /// <summary>
        /// General Information about the strain
        /// </summary>
        public string About { get; set; }

        /// <summary>
        /// The average flowering period in weeks
        /// </summary>
        public int FlowerPeriod { get; set; }

        /// <summary>
        /// The average height of the plant in inches
        /// </summary>
        public int Height { get; set; }

        /// <summary>
        /// The average yield of the plant in sqft
        /// </summary>
        public float Yield { get; set; }
        #endregion

        public Strain()
        {

        }

        public static Strain Find(int id)
        {
            const string selectStrainSql = "SELECT * FROM Strain where id = ?";
            using (SQLiteConnection connection = Data.DataBaseAccess.DbConnection)
            {
                SQLiteCommand command = connection.CreateCommand(selectStrainSql, id);
                Strain strain = command.ExecuteQuery<Strain>().SingleOrDefault();
                return strain;
            }

        }

        public static IEnumerable<Strain> FindAll()
        {
            const string selectStrainSql = "SELECT * FROM Strain";
            using (SQLiteConnection connection = Data.DataBaseAccess.DbConnection)
            {
                SQLiteCommand command = connection.CreateCommand(selectStrainSql);
                return command.ExecuteQuery<Strain>().OrderBy(x => x.Name);
            }
        }

        public bool Save()
        {
            int result = 0;
            const string checkSql = "SELECT id FROM Strain where ID = ?";
            using (SQLiteConnection connection = DataBaseAccess.DbConnection)
            {
                SQLiteCommand command = connection.CreateCommand(checkSql, Id);
                result = command.ExecuteScalar<int>();
            }

            return result > 0 ? Update() : Add();

        }

        public bool Delete()
        {
            const string deleteStrainSql = "DELETE FROM Strain WHERE Id = ?";
            try
            {
                using (SQLiteConnection connection = DataBaseAccess.DbConnection)
                {
                    SQLiteCommand command = connection.CreateCommand(deleteStrainSql, Id);
                    return command.ExecuteNonQuery() > 0;
                }
            }
            catch (SQLiteException)
            {
                return false;
                throw;
            }
        }

        private bool Add()
        {
            const string addStrainSql = "INSERT INTO Strain (Name, Genetics, About, FlowerPeriod, Height, Yield) VALUES (?,?,?,?,?,?)";
            try
            {
                using (SQLiteConnection connection = DataBaseAccess.DbConnection)
                {
                    SQLiteCommand command = connection.CreateCommand(addStrainSql, Name, Genetics, About, FlowerPeriod, Height, Yield);
                    return command.ExecuteNonQuery() > 0;
                }
            }
            catch (SQLiteException)
            {
                return false;
            }
        }

        private bool Update()
        {
            const string updateStrainSql = "UPDATE Strain SET Name =?, Genetics =?, About =?, FlowerPeriod =?, Height =?, Yeild =? WHERE Id = ?";
            try
            {
                using (SQLiteConnection connection = DataBaseAccess.DbConnection)
                {
                    SQLiteCommand command = connection.CreateCommand(updateStrainSql, Name, Genetics, About, FlowerPeriod, Height, Yield, Id);
                    return command.ExecuteNonQuery() > 0;
                }
            }
            catch (SQLiteException)
            {
                return false;
                throw;
            }
        }
    }
}
