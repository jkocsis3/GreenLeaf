using GreenLeaf.Core.Utilities;
using GreenLeaf.Core.ViewModels;
using System;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace GreenLeaf.UWP.Views.Editable
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class EditablePlantView : Page
    {
        public EditablePlantView()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            EditPlantViewModel viewModel;
            NavigationParameters parameters;
            try
            {
                parameters = e.Parameter as NavigationParameters;
                viewModel = (EditPlantViewModel)parameters.ViewModel;
                DataContext = viewModel;
            }
            catch (Exception)
            {

            }
        }
    }
}
