using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace HeartRate
{
    public class IndexToMarginLeftConverter : IMultiValueConverter
    {
       public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            int index = (int)values[0];
            double graphWidth = (double)values[1];
            int numOfVisibleItems = (int)values[2];

            double leftMargin = (graphWidth / (numOfVisibleItems - 1)) * index;
            return new Thickness(leftMargin - 5, 0, 0, 0);
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
