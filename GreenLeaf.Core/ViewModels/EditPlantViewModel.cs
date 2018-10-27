using GreenLeaf.Core.Common;
using GreenLeaf.Core.Data.TableDefinitions;
using GreenLeaf.Core.Interfaces;
using GreenLeaf.Core.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace GreenLeaf.Core.ViewModels
{
    public class EditPlantViewModel : ViewModelBase
    {
        #region Properties
        public Plant Plant { get; set; }

        private Garden _selectedGarden;

        public Garden SelectedGarden
        {
            get { return _selectedGarden; }
            set
            {
                if (_selectedGarden == value) return;
                _selectedGarden = value;
                Plant.GardenId = value.Id;
                RaisePropertyChanged(nameof(SelectedGarden));
                _isDirty = true;
            }
        }

        public IEnumerable<Garden> Gardens { get; set; }

        public string Name
        {
            get { return Plant?.Name; }
            set
            {
                if (Plant.Name == value) return;
                Plant.Name = value;
                RaisePropertyChanged(nameof(Name));
                _isDirty = true;
            }
        }

        public DateTime DatePlanted
        {
            get { return Plant.DatePlanted; }
            set
            {
                if (Plant.DatePlanted == value) return;
                Plant.DatePlanted = value;
                UpdateDates(value);
                RaisePropertyChanged(nameof(DatePlanted));
                _isDirty = true;
            }
        }

        public DateTime VegDate
        {
            get { return Plant.VegCycleStarted; }
            set
            {
                if (Plant.VegCycleStarted == value) return;
                Plant.VegCycleStarted = value;
                RaisePropertyChanged(nameof(VegDate));
                _isDirty = true;
            }
        }

        public DateTime FlowerDate
        {
            get { return Plant.FlowerCycleStarted; }
            set
            {
                if (Plant.FlowerCycleStarted == value) return;
                Plant.FlowerCycleStarted = value;
                RaisePropertyChanged(nameof(FlowerDate));
                _isDirty = true;
            }
        }

        public DateTime HarvestDate
        {
            get { return Plant.HarvestDate; }
            set
            {
                if (Plant.HarvestDate == value) return;
                Plant.HarvestDate = value;
                RaisePropertyChanged(nameof(HarvestDate));
                _isDirty = true;
            }
        }

        public IEnumerable<Strain> Strains { get; private set; }

        public IStrain SelectedStrain
        {
            get { return Plant?.Strain; }
            set
            {
                if (Plant.Strain == value) return;
                Plant.Strain = value;
                Plant.StrainId = value.Id;
                RaisePropertyChanged(nameof(SelectedStrain));
                _isDirty = true;
            }
        }


        public IEnumerable<Schedule> Schedules { get; set; }

        private Schedule _selectedSchedule;

        public Schedule SelectedSchedule
        {
            get { return Schedules.Where(x => x.Id == Plant?.ScheduleId).FirstOrDefault(); }
            set
            {
                if (Plant.FeedScheduleName == value.Name) return;
                _selectedSchedule = Schedules.Where(x => x.Id == value.Id).FirstOrDefault();
                Plant.ScheduleId = value.Id;
                RaisePropertyChanged(nameof(SelectedSchedule));
                _isDirty = true;
            }
        }
        #endregion

        private bool _isDirty = false;

        #region Commands

        public ICommand SaveCommand { get; private set; }
        public ICommand CancelCommand { get; private set; }

        #endregion

        public EditPlantViewModel()
        {
            if (!PopulateData(null))
            {
                Plant = new Plant();
                _isDirty = false;
            }
        }

        public EditPlantViewModel(IPlant plant)
        {
            if(!PopulateData(plant))
            {
                Plant = new Plant();
                _isDirty = false;
            }
        }

        #region Private Methods
        private bool PopulateData(IPlant plant)
        {
            SaveCommand = new Command(Save, CanSave);
            CancelCommand = new Command(Cancel, CanCancel);

            try
            {
                Strains = Strain.FindAll().OrderBy(x => x.Name);
                Schedules = Schedule.FindAll().OrderBy(x => x.Name);
                Gardens = Garden.FindAll().OrderBy(x => x.Name);

                if (plant == null)
                {
                    Plant = new Plant();
                    return true;
                }

                Plant = Plant.Find(plant.Id);
                SelectedSchedule = Schedules.Where(x => x.Id == plant.ScheduleId).FirstOrDefault();
                SelectedStrain = Strains.Where(x => x.Id == plant.Strain.Id).FirstOrDefault();
                SelectedGarden = Gardens.Where(x => x.Id == plant.GardenId).SingleOrDefault();
                _isDirty = false;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private void UpdateDates(DateTime value)
        {
            //Typical veg period is 4 weeks.
            if (Plant == null || Plant.Strain == null || value == null) return;
            Plant.ProjectedVegCycle = value.AddDays(7);
            Plant.ProjectedFlowerCycle = Plant.ProjectedVegCycle.AddDays(28);
            Plant.ProjectedHarvest = Plant.ProjectedFlowerCycle.AddDays(Plant.Strain.FlowerPeriod * 7);
            RaisePropertyChanged(nameof(Plant));
        }

        private void Save(object obj)
        {
            
            if(Plant.Save())
            {
                NavigationEvents.RequestPage(Pages.Back);
            }

        }

        private bool CanSave()
        {
            return _isDirty;
        }

        private void Cancel(object obj)
        {
            NavigationEvents.RequestPage(Pages.Back);
        }
        private bool CanCancel()
        {
            return true;
        }
        #endregion
    }
}
