using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Collections.ObjectModel;
using System.Reactive.Linq;
using System.Windows.Input;
using System.Timers;

namespace HeartRate
{
    public class HeartRateViewModel : Caliburn.Micro.PropertyChangedBase, IHeartRate
    {
        #region Constructors

        public HeartRateViewModel(IHeartRateListener p_heartRateListener)
        {
            m_uiConfig = ConfigReader.GetUIConfig();
            m_heartRateListener = p_heartRateListener;
            m_heartRateListener.HrDataReceived.Sample(TimeSpan.FromSeconds(m_uiConfig.SamplingFrequencyInSeconds))
                .Subscribe(onHrDataReceived);
       
            for(int i = 0; i < m_uiConfig.NumOfVisibleItems; i++)
            {
                m_totalIndicesList.Add(i);
            }

            m_timer.Enabled = true;
            m_timer.Interval = m_uiConfig.StopListeningTimeInMinutes * 60000;
            m_timer.Elapsed += onListeningTimeEnd;
        }

        #endregion

        #region Properties

        public int LastBpmValue
        {
            get
            {
                return m_lastBpmValue;
            }
            set
            {
                m_lastBpmValue = value;
                NotifyOfPropertyChange(() => LastBpmValue);
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

        public bool CanStartExecute
        {
            get
            {
                return m_canStartExecute;
            }
            set
            {
                m_canStartExecute = value;
            }
        }

        public bool CanStopExecute
        {
            get
            {
                return m_canStopExecute;
            }
            set
            {
                m_canStopExecute = value;
            }
        }

        #endregion

        #region Commands

        public ICommand StartListeningCommand
        {
            get
            {
                if(m_startListeningCommand == null)
                {
                    m_startListeningCommand = new RelayCommand(p => CanStartExecute, p => startListening());
                }
                return m_startListeningCommand;
            }
        }

        public ICommand StopListeningCommand
        {
            get
            {
                if(m_stopListeningCommand == null)
                {
                    m_stopListeningCommand = new RelayCommand(p => CanStopExecute, p => stopListening());
                }
                return m_stopListeningCommand;
            }
        }

        #endregion

        #region Public Methods

        #endregion

        #region Non Public Methods

        private void startListening()
        {
            if(HRData.Count > 0)
            {
                HRData.Clear();
                HRData = new ObservableCollection<HRData>();
            }
            CanStartExecute = false;
            CanStopExecute = true;
            m_heartRateListener.Start();
            m_timer.Start();
        }

        private void stopListening()
        {
            CanStopExecute = false;
            CanStartExecute = true;
            m_heartRateListener.Stop();
            m_timer.Stop();
        }

        private void processHrData(HRData p_hrData)
        {
            ObservableCollection<HRData> hrDataList = new ObservableCollection<HRData>();
            int index = 0;
            int firstCollectionIndex = 0;

            if (HRData.Count == m_uiConfig.NumOfVisibleItems)
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
            LastBpmValue = HRData.Last().Bpm;
        }

        #endregion

        #region Event Handlers

        private void onListeningTimeEnd(object sender, ElapsedEventArgs e)
        {
            CanStopExecute = false;
            CanStartExecute = true;
            m_heartRateListener.Stop();
            m_timer.Stop();
        }

        private void onHrDataReceived(HRData p_hrData)
        {
            Application.Current.Dispatcher.Invoke((Action)delegate 
            {
                processHrData(p_hrData);
            });
        }

        #endregion

        #region Members

        private ObservableCollection<HRData> m_hrDataCollection = new ObservableCollection<HeartRate.HRData>();
        private UIConfig m_uiConfig;
        private IList<int> m_totalIndicesList = new List<int>();
        private int m_lastBpmValue;
        private ICommand m_startListeningCommand;
        private ICommand m_stopListeningCommand;
        private bool m_canStartExecute = true;
        private bool m_canStopExecute;
        private IHeartRateListener m_heartRateListener;
        private Timer m_timer = new Timer();

        #endregion
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