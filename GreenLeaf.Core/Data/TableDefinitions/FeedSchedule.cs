using SQLite;
using System.Collections.Generic;
using System.Linq;

namespace GreenLeaf.Core.Data.TableDefinitions
{
    [Table("FeedSchedule")]
    public class FeedSchedule
    {
        /// <summary>
        /// The unique identifier for the schedule
        /// </summary>
        [AutoIncrement, PrimaryKey]
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
        /// Finds a Feeding Schedule by ID
        /// </summary>
        /// <param name="id">The unique ID of the Schedule</param>
        /// <returns>A <see cref="FeedSchedule"/></returns>
        public static FeedSchedule Find(int id)
        {
            FeedSchedule sched = new FeedSchedule();
            const string selectScheduleNameSql = "SELECT * FROM FeedSchedule WHERE id = ?";
            try
            {
                using (SQLiteConnection connection = DataBaseAccess.DbConnection)
                {
                    SQLiteCommand command = connection.CreateCommand(selectScheduleNameSql, id);
                    return command.ExecuteQuery<FeedSchedule>().SingleOrDefault();
                }

            }
            catch (SQLiteException)
            {
                return null;
            }
        }

        /// <summary>
        /// Finds all the schedules in the system
        /// </summary>
        /// <returns>An Enumerable of <see cref="FeedSchedule"/></returns>
        public static IEnumerable<FeedSchedule> FindAll()
        {
            List<FeedSchedule> sched = new List<FeedSchedule>();
            const string selectScheduleNameSql = "SELECT * FROM FeedSchedule";
            try
            {
                using (SQLiteConnection connection = DataBaseAccess.DbConnection)
                {
                    SQLiteCommand command = connection.CreateCommand(selectScheduleNameSql);
                    return command.ExecuteQuery<FeedSchedule>();
                }
            }
            catch (SQLiteException)
            {
                return null;
            }
        }
    }
}
