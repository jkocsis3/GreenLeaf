using SQLite;

namespace GreenLeaf.Core.Data.TableDefinitions
{
    [Table("NuteXDose")]
    class NuteXDose
    {
        /// <summary>
        /// The ID of the nutrient
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The Dose for this week
        /// </summary>
        public double Dose { get; set; }

        /// <summary>
        /// The week of the scheudle.
        /// </summary>
        public int Week { get; set; }

    }
}
