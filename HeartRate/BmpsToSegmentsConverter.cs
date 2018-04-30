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
    public class BmpsToSegmentsConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            IList<int> bpms = (IList<int>)value;
            IList<Segment> segments = new List<Segment>();
            IList<Point> normalizedToGraphBpms = new List<Point>();
            for(int i = 0; i < bpms.Count; i++)
            {
                Point point = new Point();
                if(i == 0)
                {
                     point = new Point(0, bpms[i] * (300 / 150));  
                }
                else
                {
                    point = new Point(normalizedToGraphBpms[i-1].X + 500 / bpms.Count, bpms[i] * (300 / 150));
                }
                normalizedToGraphBpms.Add(point);
            }
            segments = new List<Segment>(normalizedToGraphBpms.Zip(normalizedToGraphBpms.Skip(1), (a, b) => new Segment { From = a, To = b }));
            return segments;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
