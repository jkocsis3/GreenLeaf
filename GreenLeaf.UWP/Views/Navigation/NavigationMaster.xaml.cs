using GreenLeaf.Core.Utilities;
using GreenLeaf.Core.ViewModels;
using GreenLeaf.UWP.Views.Editable;
using MvvmCross.Platforms.Uap.Views;
using MvvmCross.ViewModels;
using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace GreenLeaf.UWP.Views
{

    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    [MvxViewFor(typeof(NavigationViewModel))]
    public partial class NavigationMaster : MvxWindowsPage
    {  
        public NavigationMaster()
        {
            this.InitializeComponent();
            NavigationEvents.RequestNewPage += OnRequestNewPage;
            BackButton.Visibility = Visibility.Collapsed;
            if (App.IsMobile)
            {
                MenuFlyout.Visibility = Visibility.Collapsed;
            }
        }        
        private void OnRequestNewPage(Pages pageName, params object[] parameters)
        {
            try
            {
                int id;
                object obj;
                NavigationParameters values;

                switch (pageName)
                {
                    case Pages.Back:
                        BackButton_Click(null, null);
                        break;
                    case Pages.CreateProgressReport:
                        obj = parameters[0];
                        values = new NavigationParameters(obj);
                        MainFrame.Navigate(typeof(AddProgressReportView), values);
                        break;
                    case Pages.EditablePlant:
                        obj = parameters[0];
                        values = new NavigationParameters(obj);
                        MainFrame.Navigate(typeof(EditablePlantView), values);
                        break;
                    case Pages.FullScheduleView:
                        id = int.Parse(parameters[0].ToString());
                        values = new NavigationParameters(id);
                        MainFrame.Navigate(typeof(FullScheduleView), values);
                        break;
                    case Pages.Gardens:
                        MainFrame.Navigate(typeof(GardenCollectionView));
                        break;
                    case Pages.Plants:
                        MainFrame.Navigate(typeof(PlantsCollectionView));
                        break;
                    case Pages.Plant:
                        id = int.Parse(parameters[0].ToString());
                        values = new NavigationParameters(id);                        
                        MainFrame.Navigate(typeof(PlantView), values);
                        break;
                    case Pages.ProgressReportCollection:
                        id = int.Parse(parameters[0].ToString());
                        values = new NavigationParameters(id);
                        MainFrame.Navigate(typeof(ProgressCollectionView), values);
                        break;
                    case Pages.Strains:
                        MainFrame.Navigate(typeof(StrainsCollectionView));
                        break;
                    default:
                        break;
                }
            }
            catch (Exception)
            {

                throw;
            }
            if (App.IsMobile) MenuFlyout.Visibility = Visibility.Collapsed;
            SetBackButtonVisibility(MainFrame.CanGoBack);
        }

        private void BackButton_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            if (MainFrame.CanGoBack)
            {
                MainFrame.GoBack();
                SetBackButtonVisibility(MainFrame.CanGoBack);
            }
        }

        private void SetBackButtonVisibility(bool canGoBack)
        {
            if (canGoBack)
            {
                BackButton.Visibility = Visibility.Visible;
                return;
            }
            BackButton.Visibility = Visibility.Collapsed;
        }

        private void MenuClick(object sender, RoutedEventArgs e)
        {
            if (MenuFlyout.Visibility == Visibility.Visible)
            {
                MenuFlyout.Visibility = Visibility.Collapsed;
                //If MenuFlyout is collaped, we want to align the frame with the BackButton to prevent overlapping of elements
                RelativePanel.SetRightOf(MainFrame, BackButton);
                return;
            }
            MenuFlyout.Visibility = Visibility.Visible;
            //If MenuFlyout is visible, we want to align the frame with the MenuFlyout to prevent overlapping of elements
            RelativePanel.SetRightOf(MainFrame, MenuFlyout);
        }
    }
}
