using System;
using System.Collections.Generic;
using System.Text;

namespace GreenLeaf.Core.ViewModels
{
    class PlantProgressViewModel : ViewModelBase
    {
        #region Properties
        private int _id;

        public int Id
        {
            get { return _id; }
            set
            {
                if (_id == value) return;
                _id = value;
                RaisePropertyChanged(nameof(Id));
            }
        }

        private int _plantId;

        public int PlantId
        {
            get { return _plantId; }
            set
            {
                if (_plantId == value) return;
                _plantId = value;
                RaisePropertyChanged(nameof(PlantId));
            }
        }

        private string _image;

        public string Image
        {
            get { return _image; }
            set
            {
                if (_image == value) return;
                _image = value;
                RaisePropertyChanged(nameof(Image));
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

        private DateTime dateTime;

        public DateTime Date
        {
            get { return dateTime; }
            set
            {
                if (dateTime == value) return;
                dateTime = value;
                RaisePropertyChanged(nameof(Date));
            }
        }

        private Dictionary<string, string> _nutrients;

        public Dictionary<string, string> Nutrients
        {
            get { return _nutrients; }
            set
            {
                if (_nutrients == value) return;
                _nutrients = value;
                RaisePropertyChanged(nameof(Nutrients));
            }
        }
        #endregion

        public PlantProgressViewModel()
        {

        }

        public PlantProgressViewModel(int id)
        {

        }


        #region Private Methods

        private bool PopulateData(int id)
        {
            return false;
        }

        #endregion

    }
}
