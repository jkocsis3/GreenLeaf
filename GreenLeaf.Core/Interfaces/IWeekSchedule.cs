using GreenLeaf.Core.Interfaces;
using System.Collections.Generic;

namespace GreenLeaf.Core
{
    /// <summary>
    /// Represents the current week in the feeding schedule
    /// </summary>
    interface IWeekSchedule
    {
        /// <summary>
        /// The Nutrients assigned to a plant
        /// </summary>
        IEnumerable<INutrient> Nutrients { get;}

        /// <summary>
        /// The current week of the growth cycle
        /// </summary>
        int CurrentWeek { get;}

        /// <summary>
        /// Contains the name and the dose of each nutrient for the week.
        /// </summary>
        Dictionary<string, string> NameAndDose { get;}

    }
}
