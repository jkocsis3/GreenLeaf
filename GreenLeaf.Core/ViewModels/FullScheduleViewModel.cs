using GreenLeaf.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Input;
using Windows.UI.Xaml.Data;

namespace GreenLeaf.Core.ViewModels
{
    public class FullScheduleViewModel : ViewModelBase
    {
        private ISchedule _schedule;
        public ISchedule Schedule
        {
            get { return _schedule; }
            set
            {
                if (_schedule == value) return;
                _schedule = value;
                RaisePropertyChanged(nameof(Schedule));
            }
        }

        private Dictionary<String, string[]> _weeksAndDoses;

        public Dictionary<String, string[]> WeeksAndDoses
        {
            get { return _weeksAndDoses; }
            set
            {
                if (_weeksAndDoses == value) return;
                _weeksAndDoses = value;
                RaisePropertyChanged(nameof(WeeksAndDoses));
            }
        }

        private int _weekAndDosesCount;
        public int WeekAndDosesCount
        {
            get
            {
                return _weekAndDosesCount;
            }
            set
            {
                if (_weekAndDosesCount == value) return;
                _weekAndDosesCount = value;
                RaisePropertyChanged(nameof(WeekAndDosesCount));
            }
        }
        private int _nutrientsCount;

        public int NutrientsCount
        {
            get
            {
                return _nutrientsCount;
            }
            set
            {
                if (_nutrientsCount == value) return;
                _nutrientsCount = value;
                RaisePropertyChanged(nameof(NutrientsCount));
            }
        }

        private readonly Dictionary<string, int[,]> WeekTest = new Dictionary<string, int[,]>()
        {
            { "Nute1", new int[,]{ { 1 , 11 }, {2,22 }, {3,33 }, {4,44 }, {5,55 } } },
            {"Nute2", new int[,]{ { 11 , 111 }, {12,122 }, {13,133 }, {14,144 }, {15,155 } } }
        };

        #region Commands

        public ICommand CloseCommand { get; private set; }

        #endregion

        public FullScheduleViewModel()
        {
                
        }

        public FullScheduleViewModel(int schedNum)
        {
            Schedule = Core.Schedule.Find(schedNum);
            if(!PopulateData()) Schedule = new Schedule();
            NutrientsCount = WeeksAndDoses.Count();
        }

        #region Command Methods



        #endregion

        #region Private Methods

        private bool PopulateData()
        {
            if (Schedule == null) return false;
            WeeksAndDoses = new Dictionary<string, string[]>();
            int scheduleLength = Schedule.Weeks;

            //set the "header row"
            List<string> header = new List<string>();
            bool headerAdded = false;

            foreach (Nutrient nutrient in Schedule.Nutrients)
            {                
                List<string> nuteSched = new List<string>();
               
                foreach (KeyValuePair<int, double> item in nutrient.WeekAndDoseDict)
                {
                    if (header.Count() < scheduleLength && !headerAdded)
                    {
                        string value = item.Key.Equals(0) ? "Seedling" : item.Key.ToString();
                        header.Add(value);                            
                    } 
                    nuteSched.Add(item.Value.ToString());
                }
                if (!headerAdded && (header.Count() == scheduleLength))
                {
                    WeeksAndDoses.Add("", header.ToArray());
                    headerAdded = true;
                }
                WeeksAndDoses.Add(nutrient.Name, nuteSched.ToArray());
            }            
            WeekAndDosesCount = header.Count();
            return true;

        }
        #endregion
    }
    
}
