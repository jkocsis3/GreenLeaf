using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace GreenLeaf.Core.Interfaces
{
    
    interface IProgress : IModel
    {
        /// <summary>
        /// The Unique ID of the Progress Update
        /// </summary>        
        int Id { get; set; }

        /// <summary>
        /// The Plant
        /// </summary>
        int PlantId { get; set; }

        /// <summary>
        /// The image for the plant.
        /// </summary>
        string Image { get; set; }

        /// <summary>
        /// Notes for the Progress Report
        /// </summary>
        string Notes { get; set; }
        
        /// <summary>
        /// The Date the Progress Report was created.
        /// </summary>
        DateTime Date { get; set; }        

        /// <summary>
        /// The week number for the schedule
        /// </summary>
        int WeekNum { get; set; }

        
        /// <summary>
        /// The ID of the schedule
        /// </summary>
        int ScheduleID { get; set; }

    }
}
