using GreenLeaf.Core.Common;
using GreenLeaf.Core.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

namespace GreenLeaf.Core.ViewModels
{
    public class StrainCollectionViewModel : ViewModelBase
    {
        #region Fields

        private IEnumerable<Strain> _strains;

        public IEnumerable<Strain> Strains
        {
            get { return _strains; }
            set
            {
                if (_strains == value) return;
                _strains = value;
                RaisePropertyChanged(nameof(Strains));
            }
        }

        private List<string> _geneticsCollection;

        public List<string> GeneticsCollection
        {
            get { return _geneticsCollection; }
            set
            {
                if (_geneticsCollection == value) return;
                _geneticsCollection = value;
                RaisePropertyChanged(nameof(GeneticsCollection));
            }
        }


        private string _selectedGenetics;

        public string SelectedGenetics
        {
            get { return _selectedGenetics; }
            set
            {
                if (_selectedGenetics == value) return;
                _selectedGenetics = value;
                NewStrain.Genetics = NewStrain.Genetics.FromString(SelectedGenetics);
                RaisePropertyChanged(nameof(SelectedGenetics));
            }
        }


        private Strain _selectedStrain;

        public Strain SelectedStrain
        {
            get { return _selectedStrain; }
            set
            {
                if (_selectedStrain == value) return;
                _selectedStrain = value;
                RaisePropertyChanged(nameof(SelectedStrain));
            }
        }

        private Strain _newStrain;

        public Strain NewStrain
        {
            get { return _newStrain; }
            set
            {
                if (_newStrain == value) return;
                _newStrain = value;
                RaisePropertyChanged(nameof(NewStrain));
            }
        }

        private bool _editing;

        public bool Editing
        {
            get { return _editing; }
            set
            {
                if (_editing == value) return;
                _editing = value;
                RaisePropertyChanged(nameof(Editing));
                RaisePropertyChanged(nameof(Viewing));
            }
        }

        public bool Viewing
        {
            get { return !_editing; }            
        }


        #endregion
        #region Commands

        public ICommand AddCommand { get; private set; }
        public ICommand DoneCommand { get; private set; }
        public ICommand SaveCommand { get; private set; }
        public ICommand CancelCommand { get; private set; }

        #endregion

        public StrainCollectionViewModel()
        {
            DoneCommand = new Command(GoBack, () => true);
            SaveCommand = new Command(SaveStrain, CanSaveStrain);
            AddCommand = new Command(Add, () => !Editing);
            CancelCommand = new Command(Cancel, () => Editing);
            PopulateData();
        }

        #region Command Methods

        private void SaveStrain(object obj)
        {
            NewStrain.Save();
            PopulateData();
            Editing = false;
        }

        private void GoBack(object obj)
        {
            NavigationEvents.RequestPage(Pages.Back);
        }

        private void Cancel(object obj)
        {
            NewStrain = new Strain() { Id = -1 };
            Editing = false;
        }

        private void Add(object obj)
        {
            Editing = true;
            NewStrain = new Strain() { Id = 0 };
        }

        private bool CanSaveStrain()
        {
            var blarg = !string.IsNullOrEmpty(SelectedStrain.Name);
            return !string.IsNullOrEmpty(NewStrain.Name) && !string.IsNullOrEmpty(NewStrain.Genetics.ToString()) && !string.IsNullOrEmpty(NewStrain.About) && NewStrain.FlowerPeriod > 0 && NewStrain.Height > 0 && NewStrain.Yield > 0;
        }

        #endregion
        #region Public Methods

        #endregion
        #region Private Methods

        private bool PopulateData()
        {
            List<Strain> workingList = Strain.FindAll().ToList();
            //Adding a blank item in there as a placeholder for the add button.
            workingList.Add(new Strain() { Id = -1 });
            Strains = workingList;
            SelectedStrain = Strains.FirstOrDefault();
            NewStrain = new Strain();
            GeneticsCollection = Enum.GetNames(typeof(Genetics)).ToList();
            return Strains.Any(); 
        }
        #endregion

    }
}
