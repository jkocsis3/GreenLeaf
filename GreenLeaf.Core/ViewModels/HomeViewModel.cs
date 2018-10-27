namespace GreenLeaf.Core.ViewModels
{
    public class HomeViewModel : ViewModelBase
    {
        private string _collectionName = "Deafult Collection";

        public string CollectionName
        {
            get { return _collectionName; }
            set
            {
                if (_collectionName == value) return;
                _collectionName = value;
                RaisePropertyChanged(nameof(CollectionName));
            }
        }

        private bool _collectionNameVisible = false;

        public bool CollectionNameVisible
        {
            get { return _collectionNameVisible; }
            set
            {
                if (_collectionNameVisible == value) return;
                _collectionNameVisible = value;
                RaisePropertyChanged(nameof(CollectionNameVisible));
            }
        }

        private string _plantName = "Default Plant";

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

        private bool _plantNameVisible = false;

        public bool PlantNameVisible
        {
            get { return _plantNameVisible; }
            set
            {
                if (_plantNameVisible == value) return;
                _plantNameVisible = value;
                RaisePropertyChanged(nameof(PlantNameVisible));
            }
        }
    }
}
