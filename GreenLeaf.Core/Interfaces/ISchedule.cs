using GreenLeaf.Core.Interfaces;
using System.Collections.Generic;

namespace GreenLeaf.Core
{
    public interface ISchedule : IModel
    {
        /// <summary>
        /// THe unique identifier for the schedule
        /// </summary>
        int Id { get; }

        /// <summary>
        /// The name of the schedule
        /// </summary>
        string Name { get; }

        /// <summary>
        /// An enumerable of nutrients for the schedule.
        /// </summary>
        IEnumerable<Nutrient> Nutrients { get; }

        /// <summary>
        /// The number of weeks in the schedule
        /// </summary>
        int Weeks { get; }

    }
}
