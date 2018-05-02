using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace HeartRate
{
    public class HeartRateViewModel : Caliburn.Micro.PropertyChangedBase, IHeartRate
    {
        public HeartRateViewModel(IHeartRateListener p_heartRateListener)
        {
            HRData = new List<HRData>();
            HRData.Add(new HRData(200, DateTime.Now, 0));
            HRData.Add(new HRData(58, DateTime.Now, 1));
            HRData.Add(new HRData(67, DateTime.Now, 2));
            HRData.Add(new HRData(83, DateTime.Now, 3));
            HRData.Add(new HRData(94, DateTime.Now, 4));
            HRData.Add(new HRData(76, DateTime.Now, 5));
            HRData.Add(new HRData(125, DateTime.Now, 6));
            HRData.Add(new HRData(50, DateTime.Now, 7));
        }

        public IList<HRData> HRData { get; set; }

        public int MaxValue
        {
            get { return 200; }
        }

        public int MinValue
        {
            get { return 50; }
        }
    }

    public class Segment
    {
        public Point From { get; set; }
        public Point To { get; set; }
    }

    public class HRData
    {
        public HRData(int p_bpm, DateTime p_dateTime, int p_index)
        {
            Bpm = p_bpm;
            Time = p_dateTime;
            Index = p_index;
        }
        public int Bpm { get; set; }
        public DateTime Time { get; set; }
        public int Index { get; set; }
    }
}