using GreenLeaf.Core.Utilities;
using GreenLeaf.Core.ViewModels;
using Microsoft.Toolkit.Uwp.UI.Controls;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;

using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Navigation;
using Windows.UI;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace GreenLeaf.UWP.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class FullScheduleView : Page
    {
        
        public FullScheduleView()
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
            BuildGrid(id);
        }

        private bool BuildGrid(int schedNum)
        {
            if (schedNum < 1) return false;
            FullScheduleViewModel viewModel = new FullScheduleViewModel(schedNum);
            try
            {
                DataContext = viewModel;
            }
            catch (Exception)
            {
                throw;
            }
            int maxWeeks = ((FullScheduleViewModel)DataContext).Schedule.Weeks;
            Dictionary<string, string[]> nutes = viewModel.WeeksAndDoses;

            Style TextBlockStyle = (Style)Application.Current.Resources["SmallGreenTextBlock"];
            Style frameStyle = (Style)Application.Current.Resources["ScheduleFrame"];
            Style TextBlockTitleStyle = (Style)Application.Current.Resources["HeaderGreenTextBlock"];

            UniformGrid grid = new UniformGrid
            {
                Rows = viewModel.WeeksAndDoses.Count(),
                Columns = viewModel.WeekAndDosesCount + 1,
                FlowDirection = FlowDirection.LeftToRight,
                Height = (viewModel.WeeksAndDoses.Count() * 40)
            };

            Grid.SetColumn(grid, 1);

            foreach (var item in viewModel.WeeksAndDoses)
            {
                
                TextBlock nuteNameTextBlock = new TextBlock()
                {
                    Text = item.Key,
                    Style = TextBlockStyle,
                    TextAlignment = TextAlignment.Center,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center,
                    TextWrapping = TextWrapping.WrapWholeWords
                };

                grid.Children.Add( new Border()
                {
                    Child = nuteNameTextBlock,
                    BorderBrush = new SolidColorBrush(Colors.Black),
                    BorderThickness = new Thickness(1)
                });

                foreach (string dose in item.Value)
                {
                    TextBlock doseNameTextBlock = new TextBlock()
                    {
                        Text = dose,
                        Style = TextBlockStyle,
                        TextAlignment = TextAlignment.Center,
                        HorizontalAlignment = HorizontalAlignment.Center,
                        VerticalAlignment = VerticalAlignment.Center
                    };
                    grid.Children.Add(new Border()
                    {
                        Child = doseNameTextBlock,
                        BorderBrush = new SolidColorBrush(Colors.Black),
                        BorderThickness = new Thickness(1)
                    });
                }
            }

            ContentGrid.Children.Add(grid);
            
            return true;
        }

    }
   
}
