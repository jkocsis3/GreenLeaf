using GreenLeaf.Core.Common;
using GreenLeaf.Core.Models;
using GreenLeaf.Core.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace GreenLeaf.Core.ViewModels
{
    public class PlantsCollectionViewModel : ViewModelBase
    {
        #region Properties
        private IEnumerable<Plant> _plants;

        public IEnumerable<Plant> Plants
        {
            get { return _plants; }
            set
            {
                if (_plants == value) return;
                _plants = value;
                RaisePropertyChanged(nameof(Plants));
            }
        }
        #endregion
        #region Commands

        public ICommand ViewPlantCommand { get; private set; }
        public ICommand AddPlantCommand { get; private set; }

        #endregion

        public PlantsCollectionViewModel()
        {
            ViewPlantCommand = new Command(ViewPlant, CanViewPlant);
            AddPlantCommand = new Command(AddPlant, AllowAlways);
            Plants = BuildCollections();
        }
        #region Private methods

        private static IEnumerable<Plant> BuildCollections()
        {
            List<Plant> plants = new List<Plant>();

            PlantsCollectionModel model = new PlantsCollectionModel(false);
            foreach (Plant plant in model.Plants)
            {
                TimeSpan span = DateTime.Now.Subtract(plant.DatePlanted);
                plant.Week = span.Days / 7;
                if (ImageHandler.GetMostRecentSmallImage(plant.Id, out string imagePath))
                {
                    plant.ImagePath = imagePath;
                }
                else
                {
                    plant.ImagePath = string.Empty;
                }
            }
            IOrderedEnumerable<Plant> orderedPlants = model.Plants.OrderBy(x => x.DatePlanted).OrderBy(x => x.Name);
            plants.AddRange(orderedPlants);
            plants.Add(new Plant() { Id = -1 });
            return plants;
        }

        private bool AllowAlways()
        {
            return true;
        }

        private void AddPlant(object obj)
        {
            EditPlantViewModel viewModel = new EditPlantViewModel();
            NavigationEvents.RequestPage(Pages.EditablePlant, viewModel);
        }
        private bool CanViewPlant()
        {
            return Plants.Any();
        }

        private void ViewPlant(object obj)
        {

        } 
        #endregion
    }
}
