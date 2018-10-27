using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using GreenLeaf.Core.Data;
using GreenLeaf.Core.Data.TableDefinitions;
using SQLite;

namespace GreenLeaf.Core.Models
{
    class WeekScheduleModel
    {
        #region Properties
        /// <summary>
        /// a <see cref="Dictionary{string, string}"/> of the nutrient names and doses for the selected week
        /// </summary>
        public Dictionary<string, string> NameAndDose { get; set; }

        /// <summary>
        /// The name of the schedule
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The week of the schedule.
        /// </summary>
        public int WeekNum { get; set; }
        #endregion

        public WeekScheduleModel(int schedNum, int weekNum)
        {
            WeekNum = weekNum;
            BuildModel(schedNum);
        }

        #region Private Methods

        private bool BuildModel(int schedNum)
        {
            Schedule schedule;
            List<Nutrient> nutes = new List<Nutrient>();
            using (SQLiteConnection connection = Data.DataBaseAccess.DbConnection)
            {
                const string selectWeeksSql = "SELECT * FROM FeedSchedule WHERE id= ?";
                SQLiteCommand command = connection.CreateCommand(selectWeeksSql, schedNum);
                schedule = command.ExecuteQuery<Schedule>().SingleOrDefault();

                const string selectNutrientIdsSql = "SELECT nuteId FROM SchedXNute WHERE schedId = ?";
                command = connection.CreateCommand(selectNutrientIdsSql, schedNum);
                var result = command.ExecuteQuery<SchedXNute>();
                int[] values = result.Select(x => x.NuteId).ToArray();

                
                const string selectNutrientsSql = "SELECT * FROM Nutrients where Id = ?";
                const string selectDosesSql = "SELECT * FROM NuteXDose WHERE Id = ?";
                foreach (int i in values)
                {
                    command = connection.CreateCommand(selectNutrientsSql, i);
                    Nutrient nuteresult = command.ExecuteQuery<Nutrient>().SingleOrDefault();
                    nuteresult.WeekAndDoseDict = new Dictionary<int, double>();
                    command = connection.CreateCommand(selectDosesSql, i);
                    List<NuteXDose> results = command.ExecuteQuery<NuteXDose>().ToList();
                    foreach (NuteXDose item in results)
                    {
                        nuteresult?.WeekAndDoseDict.Add(item.Week, item.Dose);
                    }
                    nutes.Add(nuteresult);
                } 
            }

            NameAndDose = new Dictionary<string, string>();
            int maxWeeks = schedule.Weeks;
            Name = schedule.Name;
            foreach (var item in nutes)
            {
                int currentWeek = WeekNum < maxWeeks ? WeekNum : maxWeeks;

                var currentWeekDoses = item.WeekAndDoseDict?.SingleOrDefault(x => x.Key == currentWeek);
                NameAndDose.Add(item.Name, currentWeekDoses?.Value.ToString(CultureInfo.InvariantCulture));
            }
            return true;
        }

        #endregion
    }
}
