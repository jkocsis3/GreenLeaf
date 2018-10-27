using GreenLeaf.Core.Models;
using System.Collections.Generic;

namespace GreenLeaf.Core.ViewModels
{
    public class WeekScheduleViewModel : ViewModelBase
    {
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

        private string _name;

        public string Name
        {
            get { return _name; }
            set
            {
                if (_name == value) return;
                _name = value;
                RaisePropertyChanged(nameof(Name));
            }
        }

        public WeekScheduleViewModel(int schedNum, int weekNum)
        {
            if (BuildSchedule(schedNum, weekNum)) return;
            Name = string.Empty;
            NameAndDose = new Dictionary<string, string>();
        }


        #region Private Methods

        private bool BuildSchedule(int schedNum, int weekNum)
        {
            WeekScheduleModel model = new WeekScheduleModel(schedNum, weekNum);
            Name = model.Name;
            NameAndDose = model.NameAndDose;
            return !string.IsNullOrEmpty(Name);
        }
        
        #endregion
        
    }
}
