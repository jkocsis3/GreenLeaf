using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using GreenLeaf.Core.ViewModels;
using GreenLeaf.Core.Utilities;
using System;
using GreenLeaf.Core;
using Windows.UI.Xaml;
using CoreImageHandler = GreenLeaf.Core.Utilities.ImageHandler;
using ImageHandler = GreenLeaf.UWP.Utilities.ImageHandler;
// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace GreenLeaf.UWP.Views.Editable
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AddProgressReportView : Page
    {
        public AddProgressReportView()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {            
            NavigationParameters parameters;
            try
            {
                parameters = e.Parameter as NavigationParameters;
                DataContext = (AddProgressReportViewModel)parameters.ViewModel;
            }
            catch (Exception)
            {

            }

        }

        private async void TakePhoto_OnClicked(object sender, RoutedEventArgs e)
        {
            AddProgressReportViewModel viewModel = (AddProgressReportViewModel)DataContext;

            TimeSpan span = DateTime.Now - viewModel.Plant.DatePlanted;
            int week = span.Days / 7;

            string imagePath = await ImageHandler.SaveProgressImage(viewModel.Plant.Id, week);

            if (string.IsNullOrEmpty(imagePath)) return;

            viewModel.ImagePath = imagePath;
        }
    }
}
