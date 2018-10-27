using GreenLeaf.Core.Common;
using System;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Data;
using Windows.UI;

namespace GreenLeaf.UWP.Utilities
{
    public class ValueToColorConvertor : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            switch(value)
            {
                case Genetics.Hybrid:
                case "Hybrid":
                    return new SolidColorBrush(Colors.Green);
                case Genetics.Indica:
                case "Indica":
                    return new SolidColorBrush(Colors.Purple);
                case Genetics.Sativa:
                case "Sativa":
                    return new SolidColorBrush(Colors.Red);
                default:
                    return new SolidColorBrush(Colors.Blue);
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
