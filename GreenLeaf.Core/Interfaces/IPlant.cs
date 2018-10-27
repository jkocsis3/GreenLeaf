using GreenLeaf.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace GreenLeaf.Core
{
    public interface IPlant : IModel
    {
        /// <summary>
        /// The Id of the plant
        /// </summary>
        int Id { get; set; }

        /// <summary>
        /// The name of the plant
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// The strain
        /// </summary>
        IStrain Strain { get; set; }

        /// <summary>
        /// The ID of the strain
        /// </summary>
        int StrainId { get; set; }

        /// <summary>
        /// The date planted
        /// </summary>
        DateTime DatePlanted { get; set; }

        /// <summary>
        /// The projected date the veg cycle start
        /// </summary>
        DateTime ProjectedVegCycle { get; set; }

        /// <summary>
        /// The date the veg cycle started
        /// </summary>
        DateTime VegCycleStarted { get; set; }

        /// <summary>
        /// The projected date the flower cycle starts
        /// </summary>
        DateTime ProjectedFlowerCycle { get; set; }

        /// <summary>
        /// The date the flower cycle started
        /// </summary>
        DateTime FlowerCycleStarted { get; set; }

        /// <summary>
        /// The projected harvest date
        /// </summary>
        DateTime ProjectedHarvest { get; set; }

        /// <summary>
        /// The Unique identifier of the nutrient schedule
        /// </summary>
        int ScheduleId { get; set; }

        /// <summary>
        /// The actual date of the harvest
        /// </summary>
        DateTime HarvestDate { get; set; }

        /// <summary>
        /// The nutrient program
        /// <example> GH Flora Trio</example>
        /// <example> Fox Farm Trio</example>
        /// </summary>
        string FeedScheduleName { get; set; }

        /// <summary>
        /// The path to the current image.
        /// </summary>
        string ImagePath { get; set; }

        /// <summary>
        /// The current week of the schedule
        /// </summary>
        int Week { get; set; }

        /// <summary>
        /// The garden the plant is assigned to.
        /// </summary>
        int GardenId { get; set; }

    }
}
