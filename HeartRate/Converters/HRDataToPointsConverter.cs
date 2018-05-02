using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Collections.ObjectModel;

namespace HeartRate
{
    public class HRDataToPointsConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            int maxValue = (int)values[0];
            int minValue = (int)values[1];
            double graphHeight = (double)values[2];
            double graphWidth = (double)values[3];
            ObservableCollection<HRData> hrData = (ObservableCollection<HRData>)values[4];
            int numOfVisibleItems = (int)values[5];
        
            IList<int> bpms = new List<int>();
            for (int hrDataIndex = 0; hrDataIndex < hrData.Count; hrDataIndex++)
            {
                bpms.Add(hrData[hrDataIndex].Bpm);
            }

            int valuesRange = maxValue - minValue;
            IList<Point> normalizedToGraphBpms = new List<Point>();
            for (int i = 0; i < bpms.Count; i++)
            {
                Point point = new Point();
                if (i == 0)
                {
                    point = new Point(0, graphHeight - ((bpms[i] - minValue) * (graphHeight / valuesRange)));
                }
                else
                {
                    point = new Point(normalizedToGraphBpms[i - 1].X + (graphWidth / (numOfVisibleItems - 1)), 
                        graphHeight - ((bpms[i] - minValue) * (graphHeight / valuesRange)));
                }
                normalizedToGraphBpms.Add(point);
            }
            return normalizedToGraphBpms;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
