using MvvmCross.ViewModels;
using GreenLeaf.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using MvvmCross.Platforms.Uap.Views;
using GreenLeaf.Core;
using GreenLeaf.Core.Utilities;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace GreenLeaf.UWP.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class PlantsCollectionView : Page
    {
        
        public PlantsCollectionView()
        {
            this.InitializeComponent();
        }

        private void OnPlantSelected(object sender, SelectionChangedEventArgs e)
        {
            if((sender as ListView).SelectedIndex == (sender as ListView).Items.Count -1)
            {
                return;
            }
            Plant selectedPlant = (Plant)(sender as ListView).SelectedItem;
            NavigationEvents.RequestPage(Pages.Plant, selectedPlant.Id);
        }
    }
}
