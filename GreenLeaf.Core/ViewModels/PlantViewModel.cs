using GreenLeaf.Core.Models;
using GreenLeaf.Core.Utilities;
using System;
using System.Collections.Generic;
using System.Windows.Input;

namespace GreenLeaf.Core.ViewModels
{
    public class PlantViewModel : ViewModelBase
    {

        #region Public Properties

        public IPlant _plant;

        /// <summary>
        /// The <see cref="Plant"/>
        /// </summary>
        public IPlant Plant
        {
            get { return _plant; }
            set
            {
                if (_plant == value) return;
                _plant = value;
                RaisePropertyChanged(nameof(Plant));
            }
        }

        /// <summary>
        /// The date the plant was planted
        /// </summary>
        public string Planted
        {
            get
            {
                return $"Planted: {Plant?.DatePlanted.ToShortDateString()}";
            }
        }

        /// <summary>
        /// Number of weeks since planted.
        /// </summary>
        public string Week
        {
            get
            {
                TimeSpan span = DateTime.Now - Plant.DatePlanted;
                return $"Week: {span.Days / 7}";
            }
        }

        /// <summary>
        /// The date the Veg cycle starts
        /// </summary>
        public string VegStart
        {
            get
            {
                return $"Veg: {DetermineCurrentStage(Plant.VegCycleStarted, Plant.ProjectedVegCycle)}";
            }
        }

        /// <summary>
        /// The date the Flower cycle starts
        /// </summary>
        public string FlowerStart
        {
            get
            {
                return $"Flower: {DetermineCurrentStage(Plant.FlowerCycleStarted, Plant.ProjectedFlowerCycle)}";
            }
        }

        /// <summary>
        /// the date to Harvest the plant.
        /// </summary>
        public string Harvest
        {
            get
            {
                return $"Harvest: {Plant.ProjectedHarvest.ToShortDateString()}";
            }
        }

        public string ImagePath
        {
            get { return Plant.ImagePath; }
            set
            {
                if (Plant.ImagePath == value) return;
                Plant.ImagePath = value;
                RaisePropertyChanged(nameof(ImagePath));
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

        public string ScheduleName { get; private set; }        

        #endregion

        #region Private Properties
        
        private readonly DateTime checkDate = new DateTime(2018, 01, 01);

        #endregion


        #region Commands

        /// <summary>
        /// View the selected Feed Schedule
        /// </summary>
        public ICommand ViewScheduleCommand { get; private set; }

        /// <summary>
        /// View the plant's progress
        /// </summary>
        public ICommand ViewProgressCommand { get; private set; }

        /// <summary>
        /// Allows the current plant to be edited.
        /// </summary>
        public ICommand EditPlantCommand { get; private set; }

        /// <summary>
        /// Creates a progress report for the current date.
        /// </summary>
        public ICommand CreateProgressReportCommand { get; private set; }

        #endregion

        public PlantViewModel()
        {
        }

        public PlantViewModel(int id)
        {
            Setup(id);
            ViewScheduleCommand = new Common.Command(ViewSchedule, CanExecute);
            ViewProgressCommand = new Common.Command(ViewProgress, CanExecute);
            EditPlantCommand = new Common.Command(EditPlant, CanExecute);
            CreateProgressReportCommand = new Common.Command(CreateProgressReport, CanExecute);
        }

        private void Setup(int id)
        {
            /*NOTES
             * For the cycles, if the date is in the future, use projected, change color
             * if the date is in the past, use the actual times, standard color.
             */

            if (id < 1) Plant = new Plant();           
            Plant = Core.Plant.Find(id);

            Schedule schedule = Schedule.Find(Plant.ScheduleId);
            ScheduleName = schedule.Name;

            WeekScheduleModel model = new WeekScheduleModel(Plant.ScheduleId, Plant.Week);
            NameAndDose = model.NameAndDose;
        }

        #region Command Methods

        private void ViewSchedule(object obj)
        {
            NavigationEvents.RequestPage(Pages.FullScheduleView, Plant.ScheduleId);
        }

        private bool CanExecute()
        {
            return Plant != null;
        }

        private void ViewProgress(object obj)
        {
            NavigationEvents.RequestPage(Pages.ProgressReportCollection, Plant.Id);
        }

        private void EditPlant(object obj)
        {
            EditPlantViewModel viewModel = new EditPlantViewModel(Plant);
            NavigationEvents.RequestPage(Pages.EditablePlant, viewModel);
        }
        #endregion

        #region PrivateMethods

        private string DetermineCurrentStage(DateTime firstDateTime, DateTime seconDateTime)
        {
            DateTime date = firstDateTime > checkDate ? firstDateTime : seconDateTime;
            return date.ToShortDateString();
        }

        private void CreateProgressReport(object obj)
        {
            AddProgressReportViewModel viewModel = new AddProgressReportViewModel(Plant);
            NavigationEvents.RequestPage(Pages.CreateProgressReport, viewModel);
        }

        #endregion
    }
}
