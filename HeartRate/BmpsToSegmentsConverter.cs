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
    public class BmpsToSegmentsConverter : IMultiValueConverter
    {
        
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            int maxValue = (int) values[0];
            int minValue = (int)values[1];
            double graphHeight = (double)values[2];
            double graphWidth = (double)values[3];
            IList<int> bpms = (List<int>)values[4];

            int valuesRange = maxValue - minValue;
            IList<Segment> segments = new List<Segment>();
            IList<Point> normalizedToGraphBpms = new List<Point>();
            for (int i = 0; i < bpms.Count; i++)
            {
                Point point = new Point();
                if (i == 0)
                {
                    point = new Point(0, (bpms[i] - minValue) * (graphHeight / valuesRange));
                }
                else
                {
                    point = new Point(normalizedToGraphBpms[i - 1].X + (graphWidth / (bpms.Count - 1)), (bpms[i] - minValue) * (graphHeight / valuesRange));
                }
                normalizedToGraphBpms.Add(point);
            }
            segments = new List<Segment>(normalizedToGraphBpms.Zip(normalizedToGraphBpms.Skip(1), (a, b) => new Segment { From = a, To = b }));
            return segments;
        }


        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
