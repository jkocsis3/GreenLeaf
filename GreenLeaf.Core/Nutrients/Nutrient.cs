using GreenLeaf.Core.Common;
using GreenLeaf.Core.Interfaces;
using SQLite;
using System.Collections.Generic;

namespace GreenLeaf.Core
{
    [Table("Nutrients")]
    public class Nutrient : INutrient
    {
        /// <summary>
        /// The ID of the Nutrient
        /// </summary>
        public int id { get; set; }

        /// <summary>
        /// The name of the Nutrient
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Instructions for use
        /// </summary>
        public string Instructions { get; set; }

        /// <summary>
        /// Notes for the Nutrient
        /// </summary>
        public string Notes { get; set; }

        // <summary>
        /// The suported Growing Mediums for the Nutrient
        /// </summary>
        public GrowMediums Medium { get; set; }

        /// <summary>
        /// A <see cref="Dictionary{int, double}"/> containing the doses and weeks. >
        /// </summary>
        public Dictionary<int, double> WeekAndDoseDict { get; set; }

        // <summary>
        /// Foreign key to the Week and Dose table
        /// </summary>
        public string WadId { get; set; }

        /// <summary>
        /// Is the dose the same each week?
        /// </summary>
        public bool IsConstantValue { get; set; }

        /// <summary>
        /// The dose.
        /// </summary>
        public double ConstantDose { get; set; }

        public Nutrient()
        {
            //Name = "This is a name";
            //Instructions = "These are instructions";
            //Notes = "These are notes";
            //WeekAndDoseDict = new Dictionary<int, double>() { { 1,1.5 },{ 2,2.5 },{ 3,3.5 }, };
        }

        public Nutrient(int count, string name)
        {
            //Name = $"{name} for {count}";
            //Instructions = $"These are instructions {count}";
            //Notes = $"These are notes {count}";
            //WeekAndDoseDict = new Dictionary<int, double>() { { 1, 1.5 }, { 2, 2.5 }, { 3, 3.5 }, };
        }
    }
}
