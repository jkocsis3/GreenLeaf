using GreenLeaf.Core.Interfaces;
using System.Collections.Generic;

namespace GreenLeaf.Core
{
    public interface IGarden : IModel
    {
        /// <summary>
        /// The Unique identifier of the table
        /// </summary>
        int Id { get; set; }

        /// <summary>
        /// The name of the garden.
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// Gets all the plants in a garden
        /// </summary>
        /// <returns>An Enumerable of <see cref="Plants"/></returns>
        IEnumerable<Plant> GetPlantsForGarder();
    }
}