using GreenLeaf.Core.Common;
using GreenLeaf.Core.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace GreenLeaf.Core.ViewModels
{
    public class AddProgressReportViewModel : ViewModelBase
    {
        #region Properties
        private string _imagePath;

        public string ImagePath
        {
            get { return _imagePath; }
            set
            {
                if (_imagePath == value) return;
                _imagePath = value;
                RaisePropertyChanged(nameof(ImagePath));
            }
        }

        private string _notes;

        public string Notes
        {
            get { return _notes; }
            set
            {
                if (_notes == value) return;
                _notes = value;
                RaisePropertyChanged(nameof(Notes));
            }
        }

        private int _weekNum;

        public int WeekNum
        {
            get { return _weekNum; }
            set
            {
                if (_weekNum == value) return;
                _weekNum = value;
                RaisePropertyChanged(nameof(WeekNum));
            }
        }

        private IEnumerable<int> _weekNums;

        public IEnumerable<int> WeekNums
        {
            get { return _weekNums; }
            set
            {
                if (_weekNums == value) return;
                _weekNums = value;
                RaisePropertyChanged(nameof(WeekNums));
            }
        }

        private Schedule _selectedSchedule;

        public Schedule SelectedSchedule
        {
            get { return _selectedSchedule; }
            set
            {
                if (_selectedSchedule == value) return;
                _selectedSchedule = value;
                RaisePropertyChanged(nameof(SelectedSchedule));
                WeekNums = PopulateWeeks(value);
            }
        }

        private int _selectedWeekNum;

        public int SelectedWeekNum
        {
            get { return _selectedWeekNum; }
            set
            {
                if (_selectedWeekNum == value) return;
                _selectedWeekNum = value;
                RaisePropertyChanged(nameof(SelectedWeekNum));
                UpdateSelectedNutrients(value);
            }
        }

        private Dictionary<string, string> _selectedNutrients;

        public Dictionary<string, string> SelectedNutrients
        {
            get { return _selectedNutrients; }
            set
            {
                if (_selectedNutrients == value) return;
                _selectedNutrients = value;
                RaisePropertyChanged(nameof(SelectedNutrients));
            }
        }


        public IEnumerable<Schedule> Schedules { get; private set; }

        public string Name { get; private set; }

        public IPlant Plant { get; private set; }

        public int ScheduleId { get; private set; }


        #endregion

        #region Commands

        public ICommand SaveCommand { get; private set; }
        public ICommand CancelCommand { get; private set; }

        #endregion

        public AddProgressReportViewModel(IPlant plant)
        {
            Plant = plant;
            PopulateData();
            SaveCommand = new Command(SaveReport, CanExecute);
            CancelCommand = new Command(Cancel, CanCancel);
        }

        #region Public Methods

        #endregion

        #region Private Methods

        private bool PopulateData()
        {           
            try
            {
                List<Schedule> schedules = Schedule.FindAll().ToList();               
                Schedules = schedules;
                SelectedSchedule = schedules.Where(x => x.Id == Plant.ScheduleId).FirstOrDefault();
                SelectedWeekNum = Plant.Week;
                Name = $"Progress Report for {Plant.Name}";
                Notes = "Enter Notes Here";
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private IEnumerable<int> PopulateWeeks(Schedule schedule)
        {
            List<int> weeks = new List<int>();

            foreach (KeyValuePair<int, double> item in Schedules.FirstOrDefault().Nutrients.FirstOrDefault().WeekAndDoseDict)
            {
                weeks.Add(item.Key);
            }

            return weeks;
        }

        private void UpdateSelectedNutrients(int weekNum)
        {
            Dictionary<string, string> results = new Dictionary<string, string>();
            Schedule schedule = SelectedSchedule;
            foreach (Nutrient item in SelectedSchedule.Nutrients)
            {
                string name = item.Name;
                KeyValuePair<int, double> nutrients = item.WeekAndDoseDict.Where(x => x.Key == weekNum).FirstOrDefault();
                results.Add(name, nutrients.Value.ToString());
            }
            SelectedNutrients = results;
        }

        private void SaveReport(object obj)
        {
            Progress progress = new Progress()
            {
                PlantId = Plant.Id,
                Notes = Notes,
                Image = ImagePath,
                WeekNum = Plant.Week,
                ScheduleID = Plant.ScheduleId,
                Date = DateTime.Now

            };

            if (progress.Save()) NavigationEvents.RequestPage(Pages.Back);
        }

        private bool CanExecute()
        {
            return ImagePath != null && Notes != null;
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
