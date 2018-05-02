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
    public class IndexToTopMarginConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            int numOfVisibleItems = (int)values[0];
            int maxValue = (int)values[1];
            int minValue = (int)values[2];
            int index = (int)values[3];
            double graphHeight = (double)values[4];
            int valuesSpan = maxValue - minValue;

            double itemValue = ((valuesSpan / numOfVisibleItems) * index) + minValue;
            double normalizedValue = graphHeight - ((itemValue - minValue) * (graphHeight / valuesSpan));

            return new Thickness(-30, normalizedValue - 15, 0, 0);
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
