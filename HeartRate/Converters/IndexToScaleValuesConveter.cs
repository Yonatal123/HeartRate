using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace HeartRate
{
    public class IndexToScaleValuesConveter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            int numOfVisibleItems = (int)values[0];
            int maxValue = (int)values[1];
            int minValue = (int)values[2];
            int index = (int)values[3];
            int valuesSpan = maxValue - minValue;

            double scaleValue = ((valuesSpan / numOfVisibleItems) * index) + minValue;
            return scaleValue.ToString();
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
