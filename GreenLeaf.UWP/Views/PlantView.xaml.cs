using GreenLeaf.Core.Utilities;
using GreenLeaf.Core.ViewModels;
using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using CoreImageHandler = GreenLeaf.Core.Utilities.ImageHandler;
using ImageHandler = GreenLeaf.UWP.Utilities.ImageHandler;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace GreenLeaf.UWP.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class PlantView : Page
    {
        private double _currentImageSize;

        public PlantView()
        {
            this.InitializeComponent();
        }        

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            int id = 0;
            NavigationParameters parameters;
            try
            {
                parameters = e.Parameter as NavigationParameters;
                id = parameters.Id;
            }
            catch (Exception)
            {
                
            }

            DataContext = new PlantViewModel(id);
            
        }

        private void ViewSchedule_OnClicked(object sender, RoutedEventArgs e)
        {
            PlantViewModel viewModel = (PlantViewModel)DataContext;
            Pages requestpage = (Pages)Enum.Parse(typeof(Pages), "FullScheduleView");
            NavigationEvents.RequestPage(requestpage, viewModel.Plant.Id);
        }

        private async void TakePhoto_OnClicked(object sender, RoutedEventArgs e)
        {
            PlantViewModel viewModel = (PlantViewModel)DataContext;

            TimeSpan span = DateTime.Now - viewModel.Plant.DatePlanted;
            int week = span.Days / 7;

            bool imagePath = await ImageHandler.SaveImage(viewModel.Plant.Id, week);
            if (!imagePath) return;

            viewModel.ImagePath = CoreImageHandler.GetDefaultImagePath(viewModel.Plant.Id, week);
        }

        private void PlantImage_ImageOpened(object sender, RoutedEventArgs e)
        {
            ResizeImage((Image)sender);
        }

        private void ResizeImage(Image image)
        {
            _currentImageSize = this.ActualWidth / 3;

            image.Width = _currentImageSize;
        }
    }
}
