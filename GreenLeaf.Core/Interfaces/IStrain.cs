using GreenLeaf.Core.Common;
using GreenLeaf.Core.Interfaces;

namespace GreenLeaf.Core
{
    public interface IStrain : IModel
    {
        /// <summary>
        /// The Unique identifier
        /// </summary>
        int Id { get; set; }

        /// <summary>
        /// The name of the strain
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// The strain's genetics
        /// </summary>
        Genetics Genetics { get; set; }

        /// <summary>
        /// General Information about the strain
        /// </summary>
        string About { get; set; }

        /// <summary>
        /// The average flowering period in weeks
        /// </summary>
        int FlowerPeriod { get; set; }

        /// <summary>
        /// The average height of the plant in inches
        /// </summary>
        int Height { get; set; }
        
        /// <summary>
        /// The average yield of the plant in sqft
        /// </summary>
        float Yield { get; set; }



    }
}
