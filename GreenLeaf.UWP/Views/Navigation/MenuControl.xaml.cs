using GreenLeaf.Core.Utilities;
using GreenLeaf.Core.ViewModels;
using System;
using Windows.UI.Xaml.Controls;

namespace GreenLeaf.UWP.Views
{
    public partial class MenuControl : UserControl
    {
        

        public MenuControl()
        {
            this.InitializeComponent();
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var pageName = ((sender as ListView).SelectedItem).ToString();
            Pages requestPage = (Pages)Enum.Parse(typeof(Pages), pageName);
            NavigationEvents.RequestPage(requestPage);
        }

        
    }
}
