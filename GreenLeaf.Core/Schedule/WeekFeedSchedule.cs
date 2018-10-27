using GreenLeaf.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GreenLeaf.Core
{
    class WeekFeedSchedule : IWeekSchedule
    {
        #region Properties
        /// <summary>
        /// The Nutrients assigned to a plant
        /// </summary>
        public IEnumerable<INutrient> Nutrients { get; }

        /// <summary>
        /// The current week of the growth cycle
        /// </summary>
        public int CurrentWeek { get; }

        /// <summary>
        /// Contains the name and the dose of each nutrient for the week.
        /// </summary>
        public Dictionary<string, string> NameAndDose { get; } 
        #endregion

        public WeekFeedSchedule()
        {
            CurrentWeek = 2;
            NameAndDose = new Dictionary<string, string>();
            PopulateValues();
        }

        #region Private Methods
        private void PopulateValues()
        {
            List<Nutrient> nutrients = new List<Nutrient>();
            List<string> names = new List<string>() { "name1", "name2", "name311223", "name4", "name5", "name6", "name7", "name8", "name9", "name10" };          
            
            // build the list
           
            foreach(string name in names)
            {
                Nutrient nute = new Nutrient(CurrentWeek, name);
                nutrients.Add(nute);
            }
            //get the current week
            foreach (Nutrient item in nutrients)
            {
                double dose = item.WeekAndDoseDict.SingleOrDefault(x => x.Key == CurrentWeek).Value;
                string Name = item.Name;
                NameAndDose.Add(Name, dose.ToString());
            }

        }
        #endregion
    }
}
