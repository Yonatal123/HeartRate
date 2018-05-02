using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace HeartRate
{
    public class HeartRateViewModel : Caliburn.Micro.PropertyChangedBase, IHeartRate
    {
        private IObserver<HRData> onHrDataRecived;

        public HeartRateViewModel(IHeartRateListener p_heartRateListener)
        {
            HRData = new List<HRData>();
            p_heartRateListener.HrDataReceived.Subscribe(onHrDataRecived);
            //HRData.Add(new HRData(200, DateTime.Now));

            //HRData.Add(new HRData(58, DateTime.Now));
            //HRData.Add(new HRData(67, DateTime.Now));
            //HRData.Add(new HRData(83, DateTime.Now));
            //HRData.Add(new HRData(94, DateTime.Now));
            //HRData.Add(new HRData(76, DateTime.Now));
            //HRData.Add(new HRData(125, DateTime.Now));
            //HRData.Add(new HRData(50, DateTime.Now));
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

        private void onHrDataReceived(HRData p_hrData)
        {

            IList<HRData> hrDataList = new List<HRData>();
            int index = 0;
            if (m_counter == 9)
            {
                index = 1;
            }



            HRData hrData = new HRData(p_hrData.Bpm, p_hrData.Time);
            hrData.Index = m_counter;
            HRData.Add(hrData);
            m_counter++;
        }

        private int m_counter;
    }

    public class Segment
    {
        public Point From { get; set; }
        public Point To { get; set; }
    }

    public class HRData
    {
        public HRData(int p_bpm, DateTime p_dateTime)
        {
            Bpm = p_bpm;
            Time = p_dateTime;
        }
        public int Bpm { get; set; }
        public DateTime Time { get; set; }
        public int Index { get; set; }
    }
}