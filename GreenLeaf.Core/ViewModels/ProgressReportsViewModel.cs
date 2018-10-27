using GreenLeaf.Core.Common;
using GreenLeaf.Core.Models;
using GreenLeaf.Core.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

namespace GreenLeaf.Core.ViewModels
{
    public class ProgressReportsViewModel : ViewModelBase
    {
        #region Fields

        private string _plantName;

        public string PlantName
        {
            get { return _plantName; }
            set
            {
                if (_plantName == value) return;
                _plantName = value;
                RaisePropertyChanged(nameof(PlantName));
            }
        }

        private string _scheduleName;

        public string ScheduleName
        {
            get { return _scheduleName; }
            set
            {
                if (_scheduleName == value) return;
                _scheduleName = value;
                RaisePropertyChanged(nameof(ScheduleName));
            }
        }


        public IEnumerable<Progress> ProgressReports { get; private set; }

        private Progress _selectedProgressReport;

        public Progress SelectedProgressReport
        {
            get { return _selectedProgressReport; }
            set
            {
                if (_selectedProgressReport == value) return;
                _selectedProgressReport = value;
                RaisePropertyChanged(nameof(SelectedProgressReport));
                UpdateData();
            }
        }

        private Dictionary<string, string> _nameAndDose;
        public Dictionary<string, string> NameAndDose
        {
            get
            {
                return _nameAndDose;
            }
            set
            {
                if (_nameAndDose == value) return;
                RaisePropertyChanged(nameof(NameAndDose));
                _nameAndDose = value;
            }
        }

        #endregion

        Plant _plant;
        #region Commands
        public ICommand DoneCommand { get; private set; }
        #endregion


        public ProgressReportsViewModel(int plantId)
        {
            Guards.ArgumentGuard(plantId);
            PopulateData(plantId);
            DoneCommand = new Command(ClosePage, CanClosePage);
            SelectedProgressReport = ProgressReports?.FirstOrDefault();
        }

        #region Command Methpods
        private void ClosePage(object obj)
        {
            Guards.ArgumentGuard(obj);
            
            NavigationEvents.RequestPage(Pages.Back);
        }

        private bool CanClosePage()
        {
            return true;
        }
        #endregion

        #region private methods
        private bool PopulateData(int plantId)
        {
            Guards.ArgumentGuard(plantId);

            try
            {
                ProgressReports = Progress.FindAllForPlant(plantId);

                _plant = Plant.Find(plantId);

                PlantName = $"Progress Report For {_plant.Name}";

                Schedule schedule = Schedule.Find(_plant.ScheduleId);
                ScheduleName = schedule.Name;

                WeekScheduleModel model = new WeekScheduleModel(_plant.ScheduleId, SelectedProgressReport.WeekNum);
                NameAndDose = model.NameAndDose;

                return ProgressReports.Any();
            }
            catch (Exception)
            {
                return false;
            }
        }

        private bool UpdateData()
        {
            try
            {
                WeekScheduleModel model = new WeekScheduleModel(_plant.ScheduleId, SelectedProgressReport.WeekNum);
                NameAndDose = model.NameAndDose;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        #endregion
    }
}
