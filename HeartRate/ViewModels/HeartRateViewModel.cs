using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Collections.ObjectModel;
using System.Reactive.Linq;

namespace HeartRate
{
    public class HeartRateViewModel : Caliburn.Micro.PropertyChangedBase, IHeartRate
    {
      
        public HeartRateViewModel(IHeartRateListener p_heartRateListener)
        {
            m_uiConfig = ConfigReader.GetUIConfig();
            p_heartRateListener.HrDataReceived.Subscribe(onHrDataReceived);
            p_heartRateListener.Start();

            for(int i = 0; i < m_uiConfig.NumOfVisibleItems; i++)
            {
                m_totalIndicesList.Add(i);
            }
        }

        public ObservableCollection<HRData> HRData
        {
            get
            {
                return m_hrDataCollection;
            }
            set
            {
                m_hrDataCollection = value;
                NotifyOfPropertyChange(() => HRData);
            }
        }

        public IList<int> TotalIndicesList
        {
            get { return m_totalIndicesList; }
        }


        public int MaxValue
        {
            get { return m_uiConfig.MaxValue; }
        }

        public int MinValue
        {
            get { return m_uiConfig.MinValue; }
        }

        public int NumOfVisibleItems
        {
            get { return m_uiConfig.NumOfVisibleItems; }
        }

        private void onHrDataReceived(HRData p_hrData)
        {
            Application.Current.Dispatcher.Invoke((Action)delegate 
            {
                processHrData(p_hrData);
            });
        }

        private void processHrData(HRData p_hrData)
        {
            ObservableCollection<HRData> hrDataList = new ObservableCollection<HRData>();
            int index = 0;
            int firstCollectionIndex = 0;

            if (HRData.Count == 10)
            {
                firstCollectionIndex = 1;
            }

            for (int i = firstCollectionIndex; i < HRData.Count; i++)
            {
                hrDataList.Add(new HRData(HRData[i].Bpm, HRData[i].Time, index++));
            }

            hrDataList.Add(new HRData(p_hrData.Bpm, p_hrData.Time, index));

            HRData.Clear();

            HRData = hrDataList;
        }

        private ObservableCollection<HRData> m_hrDataCollection = new ObservableCollection<HeartRate.HRData>();
        private UIConfig m_uiConfig;
        private IList<int> m_totalIndicesList = new List<int>();
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