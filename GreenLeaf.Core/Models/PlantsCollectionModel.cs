using SQLite;
using System;
using System.Collections.Generic;

namespace GreenLeaf.Core.Models
{
    class PlantsCollectionModel
    {
        #region Properties
        /// <summary>
        /// The plants in the collection
        /// </summary>
        public IEnumerable<Plant> Plants { get; set; } 
        #endregion

        public PlantsCollectionModel(bool isEmpty)
        {
            if(!isEmpty) Plants = FindAllPlants();
        }


        #region private Methods

        private IEnumerable<Plant> FindAllPlants()
        {
            IEnumerable<Plant> collection;
            List<Plant> plants = new List<Plant>();

            const string selectAllPlants = "SELECT * FROM Plant";
            using (SQLiteConnection connection = Data.DataBaseAccess.DbConnection)
            {
                SQLiteCommand command = connection.CreateCommand(selectAllPlants);
                try
                {
                    collection = command.ExecuteQuery<Plant>();
                    plants.AddRange(collection);
                }
                catch (Exception)
                {
                    throw;
                }
            }
            foreach(Plant plant in collection)
            {
                plant.Strain = Strain.Find(plant.StrainId);
            }
            return plants;
        }

        #endregion
    }
}
