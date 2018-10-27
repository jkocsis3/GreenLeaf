using SQLite;

namespace GreenLeaf.Core.Data.TableDefinitions
{
    [Table("SchedXNute")]
    class SchedXNute
    {
        /// <summary>
        /// The unique ID of the schedule.
        /// </summary>
        public int SchedID { get; set; }

        /// <summary>
        /// The unique ID of the nutrient.
        /// </summary>
        public int NuteId { get; set; }

    }
}
