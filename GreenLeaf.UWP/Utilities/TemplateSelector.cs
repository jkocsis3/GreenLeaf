using GreenLeaf.Core;
using GreenLeaf.Core.Models;
using GreenLeaf.Core.ViewModels;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace GreenLeaf.UWP.Utilities
{
    public class TemplateSelector : DataTemplateSelector
    {
        protected override DataTemplate SelectTemplateCore(object item, DependencyObject container)
        {
            if (item.GetType() == typeof(Strain))
            {
                if ((item as Strain).Id == -1) return AddTemplate;
            }

            if (item.GetType() == typeof(Plant))
            {
                if ((item as Plant).Id == -1) return AddTemplate;
            }
            return ItemTemplate;
        }

        public DataTemplate AddTemplate { get; set; }
        public DataTemplate ItemTemplate { get; set; }
    }
}
