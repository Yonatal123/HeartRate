using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HeartRate
{
    public class HeartRateMonitorViewModel
    {
        public HeartRateMonitorViewModel(IHeartRateListener p_heartRateListener)
        {
            var Points = new Point[]
            {
                new Point { X= 0, Y = 10 },
                new Point { X = 10, Y = 30 },
                new Point { X = 20, Y = 20 },
            };

            Segments = new List<Segment>(Points.Zip(Points.Skip(1), (a, b) => new Segment { From = a, To = b }));
        }

        public IList<Segment> Segments { get; set; }
    }

    public class Segment
    {
        public Point From { get; set; }
        public Point To { get; set; }
    }


}
