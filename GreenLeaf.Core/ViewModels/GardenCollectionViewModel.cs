using GreenLeaf.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GreenLeaf.Core.ViewModels
{
    public class GardenCollectionViewModel : ViewModelBase
    {
        #region Fields

        private IEnumerable<GardenCollectionModel> _model;

        public IEnumerable<GardenCollectionModel> Model
        {
            get { return _model; }
            set
            {
                if (_model == value) return;
                _model = value;
                RaisePropertyChanged(nameof(Model));
            }
        }

        
        private IGarden _selectedGarden;

        public IGarden SelectedGarden
        {
            get { return _selectedGarden; }
            set
            {
                if (_selectedGarden == value || value == null) return;
                _selectedGarden = value;
                RaisePropertyChanged(nameof(SelectedGarden));
            }
        }

        

        private Plant _selectedPlant;

        public Plant SelectedPlant
        {
            get { return _selectedPlant; }
            set
            {
                if(_selectedPlant == value) return;
                _selectedPlant = value;
                RaisePropertyChanged(nameof(SelectedPlant));
            }
        }

        #endregion

        #region Commands
        #endregion

        #region ctor

        public GardenCollectionViewModel()
        {
            if (!FillGardens()) Model = new List<GardenCollectionModel>();
        }

        #endregion

        #region Public Methods

        public bool AddNewGarden(string name)
        {
            Garden garden = new Garden
            {
                Name = name,
                Id = -1
            };
            if (garden.Save())
            {
                return FillGardens();
            }
            return false;
            
        }

        #endregion

        #region Private Methods

        private bool FillGardens()
        {
            try
            {
                IEnumerable<Garden> gardenList = Garden.FindAll();
                List<GardenCollectionModel> model = new List<GardenCollectionModel>();
                foreach (Garden garden in gardenList)
                {
                    model.Add(new GardenCollectionModel(garden));
                }
                
                IOrderedEnumerable<GardenCollectionModel> orderedModel = model.OrderBy(x => x.Garden.Name);
                Model = model;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        #endregion

        #region Command Methods

        #endregion
    }

}
