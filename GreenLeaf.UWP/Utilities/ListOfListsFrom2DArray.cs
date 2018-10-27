using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace GreenLeaf.UWP.Utilities
{
    public class ListOfListsFrom2DArray : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            string[] values = value as string[];
            var valueArray = values.ToArray();
            //  https://stackoverflow.com/a/37458182/424129
            var result = valueArray.Cast<string>()
                // Use overloaded 'Select' and calculate row index.
                .Select((x, i) => new { x, index = i / values.Count() })
                //// Group on Row index
                .GroupBy(x => x.index)
                //// Create List for each group.  
                .Select(x => x.Select(s => s.x).ToList())
                .ToList();

            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
