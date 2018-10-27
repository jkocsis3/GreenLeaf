using GreenLeaf.Core;
using GreenLeaf.Core.Utilities;
using GreenLeaf.Core.ViewModels;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace GreenLeaf.UWP.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class GardenCollectionView : Page
    {
        public GardenCollectionView()
        {
            this.InitializeComponent();
        }

        private void OnPlantSelected(object sender, SelectionChangedEventArgs e)
        {
            Plant selectedPlant = (Plant)(sender as ListView).SelectedItem;
            NavigationEvents.RequestPage(Pages.Plant, selectedPlant.Id);
        }

        private void AddGardenCLick(object sender, RoutedEventArgs e)
        {
            AddGardenStackPanel.Visibility = Visibility.Visible;
        }

        private void AddNameCLick(object sender, RoutedEventArgs e)
        {
            GardenCollectionViewModel viewModel = (GardenCollectionViewModel)DataContext;
            if (!viewModel.AddNewGarden(NameTextBlock.Text))
            {
                NameTextBlock.BorderBrush = new SolidColorBrush(Colors.Red);
                NameTextBlock.BorderThickness = new Thickness(2);
                return;
            }
            NameTextBlock.BorderThickness = new Thickness(0);
            AddGardenStackPanel.Visibility = Visibility.Collapsed;
        }
    }
}
