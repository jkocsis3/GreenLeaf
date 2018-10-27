using GreenLeaf.Core.Data.TableDefinitions;
using GreenLeaf.Core.Interfaces;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GreenLeaf.Core
{
    [Table("FeedSchedule")]
    public class Schedule : ISchedule
    {
        #region Fields

        /// <summary>
        /// THe unique identifier for the schedule
        /// </summary>
        /// [AutoIncrement, PrimaryKey]
        public int Id { get; set; }

        /// <summary>
        /// The name of the schedule
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The number of weeks in the schedule
        /// </summary>
        public int Weeks { get; set; }

        /// <summary>
        /// An enumerable of nutrients for the schedule.
        /// </summary>
        public IEnumerable<Nutrient> Nutrients { get; set; }

        #endregion

        public Schedule()
        {
                
        }

        public Schedule(int schedNum)
        {
            
        }

        #region Data Methods

        public bool Save()
        {
            throw new NotImplementedException();
        }

        public bool Delete()
        {
            throw new NotImplementedException();
        }

        public static Schedule Find(int schedNum)
        {
            return BuildSchedule(schedNum);
        }

        public static int GetScheduleIDForPlant(int plantId)
        {
            const string selectScheduleSql = "SELECT id, name, weeks FROM FeedSchedule WHERE id = ?";
            const string selectPlantSql = "SELECT * FROM Plant where Id = ?";
            try
            {
                using (SQLiteConnection connection = Data.DataBaseAccess.DbConnection)
                {
                    SQLiteCommand command = connection.CreateCommand(selectPlantSql, plantId);
                    Plant plantResult = command.ExecuteQuery<Plant>().FirstOrDefault();

                    command = connection.CreateCommand(selectScheduleSql, plantResult.ScheduleId);
                    Schedule scheduleResult = command.ExecuteQuery<Schedule>().FirstOrDefault();
                    return scheduleResult.Id;
                }
            }
            catch (SQLiteException)
            {
                return 0;
            }
        }

        public static IEnumerable<Schedule> FindAll()
        {
            List<Schedule> schedules = new List<Schedule>();
            const string selectSql = "SELECT id FROM FeedSchedule";
            try
            {
                using (SQLiteConnection connection = Data.DataBaseAccess.DbConnection)
                {
                    SQLiteCommand command = connection.CreateCommand(selectSql);
                    List<Schedule> results = command.ExecuteQuery<Schedule>();
                    foreach (Schedule sched in results)
                    {
                        schedules.Add(BuildSchedule(sched.Id));
                    }
                    return schedules;
                }
            }
            catch (SQLiteException)
            {
                return null;
            }
        }

        #endregion

        #region Private Methods

        private static Schedule BuildSchedule(int schedNum)
        {
            Schedule sched = new Schedule();
            const string selectNameSql = "SELECT * FROM FeedSchedule WHERE id= ?";
            SQLiteCommand command = Data.DataBaseAccess.DbConnection.CreateCommand(selectNameSql, schedNum);
            Schedule feedSchedule = command.ExecuteQuery<Schedule>().SingleOrDefault();


            const string selectNutrientIdsSql = "SELECT nuteId FROM SchedXNute WHERE schedId = ?";
            command = Data.DataBaseAccess.DbConnection.CreateCommand(selectNutrientIdsSql, schedNum);
            int[] nutrientIds = command.ExecuteQuery<SchedXNute>().Select(x => x.NuteId).ToArray();


            List<Nutrient> nutes = new List<Nutrient>();
            const string selectNutrientsSql = "SELECT * FROM Nutrients where Id = ?";
            const string selectDosesSql = "SELECT * FROM NuteXDose WHERE Id = ? ORDER BY week";

            foreach (int i in nutrientIds)
            {
                command = Data.DataBaseAccess.DbConnection.CreateCommand(selectNutrientsSql, i);
                Nutrient nuteresult = command.ExecuteQuery<Nutrient>().SingleOrDefault();

                nuteresult.WeekAndDoseDict = new Dictionary<int, double>();

                command = Data.DataBaseAccess.DbConnection.CreateCommand(selectDosesSql, i);
                List<NuteXDose> results = command.ExecuteQuery<NuteXDose>().ToList();

                if (nuteresult.IsConstantValue)
                {
                    int x = 0;
                    while(x < feedSchedule.Weeks)
                    {
                        nuteresult?.WeekAndDoseDict.Add(x, nuteresult.ConstantDose);
                        x++;
                    }
                    nutes.Add(nuteresult);
                    break;
                }

                foreach (NuteXDose item in results)
                {
                    nuteresult?.WeekAndDoseDict.Add(item.Week, item.Dose);
                }                
                nutes.Add(nuteresult);
            }

            if (feedSchedule == null) return null;

            sched.Name = feedSchedule.Name;
            sched.Id = feedSchedule.Id;
            sched.Weeks = feedSchedule.Weeks;
            sched.Nutrients = nutes;
            return sched;
        }
        #endregion

    }
}
