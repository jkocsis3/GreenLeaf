using GreenLeaf.Core.Utilities;
using GreenLeaf.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Windows.UI.Xaml.Controls;

namespace GreenLeaf.Core.Models
{
    public class GardenCollectionModel : INotifyPropertyChanged
    {

        #region Properties
        /// <summary>
        /// The Garden
        /// </summary>
        public Garden Garden { get; set; }

        /// <summary>
        /// The Plants associated with the Garden
        /// </summary>
        public IEnumerable<Plant> Plants { get; set; } 
        #endregion

        public GardenCollectionModel(Garden garden)
        {
            Garden = garden;
            if (!PopulateData())
            {
                Garden = new Garden();
                Plants = new List<Plant>();
            }
        }

        public GardenCollectionModel()
        {
            Garden = new Garden();
            Plants = new List<Plant>();
        }

        #region Private Methods.
        private bool PopulateData()
        {
            Plants = new List<Plant>();
            try
            {
                IOrderedEnumerable<Plant> orderedPlants = Garden?.GetPlantsForGarder().OrderBy(x => x.Name);
                Plants = orderedPlants;
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
        #endregion
        #region NotifyPropertyChanged members
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Handles the property changed event for the observable collection
        /// </summary>
        /// <param name="propertyName">The property which has changed</param>
        protected virtual void RaisePropertyChanged(string propertyName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        } 
        #endregion
    }
}
