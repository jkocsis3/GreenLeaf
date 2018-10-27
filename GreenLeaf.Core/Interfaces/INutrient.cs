using GreenLeaf.Core.Common;
using SQLite;
using System.Collections.Generic;

namespace GreenLeaf.Core.Interfaces
{
    interface INutrient
    {
        /// <summary>
        /// The ID of the Nutrient
        /// </summary>
        [PrimaryKey, AutoIncrement]
        int id { get; set; }

        /// <summary>
        /// The name of the Nutrient
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// Instructions for use
        /// </summary>
        string Instructions { get; set; }

        /// <summary>
        /// Notes for the Nutrient
        /// </summary>
        string Notes { get; set; }

        /// <summary>
        /// Is the dose the same each week?
        /// </summary>
        bool IsConstantValue { get; set; }

        /// <summary>
        /// The dose.
        /// </summary>
        double ConstantDose { get; set; }

        /// <summary>
        /// The supported Growing Mediums for the Nutrient
        /// </summary>
        [Ignore]
        GrowMediums Medium { get; set; }

        /// <summary>
        /// Foreign key to the Week and Dose table
        /// </summary>
        [Column("weekanddoseid")]
        string WadId { get; set; }

        /// <summary>
        /// A <see cref="Dictionary{int, double}"/> containing the doses and weeks. >
        /// </summary>
        Dictionary<int, double> WeekAndDoseDict { get; set; }
    }
}
