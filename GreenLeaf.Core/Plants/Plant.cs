
using GreenLeaf.Core.Interfaces;
using GreenLeaf.Core.Utilities;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GreenLeaf.Core
{
    public class Plant : IPlant
    {
        #region Fields
        /// <summary>
        /// The Id of the plant
        /// </summary>
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        /// <summary>
        /// The name of the plant
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The ID of the strain
        /// </summary>
        public int StrainId { get; set; }

        /// <summary>
        /// The date planted
        /// </summary>
        public DateTime DatePlanted { get; set; }

        /// <summary>
        /// The projected date the veg cycle start
        /// </summary>
        public DateTime ProjectedVegCycle { get; set; }

        /// <summary>
        /// The date the veg cycle started
        /// </summary>
        public DateTime VegCycleStarted { get; set; }

        /// <summary>
        /// The projected date the flower cycle starts
        /// </summary>
        public DateTime ProjectedFlowerCycle { get; set; }

        /// <summary>
        /// The date the flower cycle started
        /// </summary>
        public DateTime FlowerCycleStarted { get; set; }

        /// <summary>
        /// The projected harvest date
        /// </summary>
        public DateTime ProjectedHarvest { get; set; }

        /// <summary>
        /// The Unique identifier of the nutrient schedule
        /// </summary>
        public int ScheduleId { get; set; }

        /// <summary>
        /// The actual date of the harvest
        /// </summary>
        public DateTime HarvestDate { get; set; }

        /// <summary>
        /// The garden the plant is assigned to.
        /// </summary>
        public int GardenId { get; set; }

        /// <summary>
        /// The current week of the schedule
        /// </summary>
        [Ignore]
        public int Week { get; set; }

        /// <summary>
        /// The strain
        /// </summary>
        [Ignore]
        public IStrain Strain { get; set; }

        /// <summary>
        /// The nutrient program
        /// <example> GH Flora Trio</example>
        /// <example> Fox Farm Trio</example>
        /// </summary>
        [Ignore]
        public string FeedScheduleName { get; set; }

        /// <summary>
        /// The path to the current image.
        /// </summary>
        [Ignore]
        public string ImagePath { get; set; }

        #endregion

        public Plant()
        {

        }

        #region Public Methods
        /// <summary>
        /// Find a Plant by ID
        /// </summary>
        /// <param name="id">The id of the requested Plant</param>
        /// <returns>The <see cref="Plant"/></returns>
        public static Plant Find(int id)
        {
            const string selectPlantSql = "SELECT * FROM Plant WHERE id = ?";
            try
            {
                using (SQLiteConnection connection = Data.DataBaseAccess.DbConnection)
                {
                    SQLiteCommand command = connection.CreateCommand(selectPlantSql, id);
                    Plant plant = command.ExecuteQuery<Plant>().SingleOrDefault();
                    Strain strain = Core.Strain.Find(plant.StrainId);

                    if (plant == null) return null;

                    TimeSpan span = DateTime.Now.Subtract(plant.DatePlanted);
                    plant.Week = span.Days / 7;
                    plant.ImagePath = ImageHandler.GetMostRecentDefaultImage(plant.Id);
                    plant.Strain = strain;

                    return plant;
                }
            }
            catch (SQLiteException)
            {
                return null;
            }
        }

        public static IEnumerable<Plant> FindPlantsForGarden(int gardenId)
        {
            List<Plant> plants = new List<Plant>();
            const string selectPlantIdsSql = "SELECT * FROM Plant Where GardenId = ?";
            try
            {
                SQLiteCommand command = Data.DataBaseAccess.DbConnection.CreateCommand(selectPlantIdsSql, gardenId);

                return command.ExecuteQuery<Plant>();
            }
            catch (SQLiteException)
            {
                return null;
            }
        }

        public bool Save()
        {
            int result = 0;
            const string selectPlantSql = "SELECT Id FROM plant WHERE id = ?";
            try
            {
                using (SQLiteConnection connection = Data.DataBaseAccess.DbConnection)
                {
                    SQLiteCommand command = connection.CreateCommand(selectPlantSql, Id);
                    result = command.ExecuteScalar<int>();
                }

                return result > 0 ? Update() : Add();
            }
            catch (SQLiteException)
            {
                return false;
            }
        }

        public bool Delete()
        {
            const string deletePlantSql = "DELETE FROM plant WHERE id = ?";
            try
            {
                using (SQLiteConnection connection = Data.DataBaseAccess.DbConnection)
                {
                    SQLiteCommand command = connection.CreateCommand(deletePlantSql, Id);
                    return command.ExecuteScalar<int>() > 0;
                }
            }
            catch (SQLiteException)
            {
                return false;
            }
        }
        #endregion

        #region Private Methods
        public bool Add()
        {
            const string AddPlantSql = "INSERT INTO Plant (Name, DatePlanted, ProjectedVegCycle, VegCycleStarted, ProjectedFlowerCycle, FlowerCycleStarted, ProjectedHarvest, HarvestDate, StrainId, ScheduleId, GardenId)" +
                "VALUES (?,?,?,?,?,?,?,?,?,?,?) ";
            try
            {
                using (SQLiteConnection connection = Data.DataBaseAccess.DbConnection)
                {
                    SQLiteCommand command = connection.CreateCommand(AddPlantSql, new object[] { Name, DatePlanted, ProjectedVegCycle, VegCycleStarted, ProjectedFlowerCycle, ProjectedFlowerCycle, ProjectedHarvest, HarvestDate, StrainId, ScheduleId, GardenId });
                    int result = command.ExecuteNonQuery();
                    return result > 0;
                }
            }
            catch (SQLiteException)
            {
                return false;
            }
        }

        public bool Update()
        {
            const string UpdatePlantSql = "Update Plant set Name =?, DatePlanted=?, ProjectedVegCycle=?, VegCycleStarted=?, ProjectedFlowerCycle=?, FlowerCycleStarted=?, ProjectedHarvest=?, HarvestDate=?, StrainId=?, ScheduleId=?, GardenId=?" +
                   "WHERE Id =?";

            try
            {
                ProjectedVegCycle = DatePlanted.AddDays(7);

                using (SQLiteConnection connection = Data.DataBaseAccess.DbConnection)
                {
                    SQLiteCommand command = connection.CreateCommand(UpdatePlantSql, new object[] { Name, DatePlanted, ProjectedVegCycle, VegCycleStarted, ProjectedFlowerCycle, FlowerCycleStarted, ProjectedHarvest, HarvestDate, StrainId, ScheduleId, GardenId, Id });
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
