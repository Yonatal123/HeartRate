using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reactive.Subjects;
using System.Reactive.Concurrency;
using System.Timers;
using System.IO.Ports;

namespace HeartRate
{
    public class HeartRateListener : IHeartRateListener
    {
        public HeartRateListener()
        {
            m_uiConfig = ConfigReader.GetUIConfig();
            m_serialPort = new SerialPort(m_uiConfig.SerialPortName);
            m_serialPort.DataReceived += onDataRecieved;
        }
        public void Start()
        {
            m_serialPort.Open();
            //m_timer.Interval = 3000;
            //m_timer.Elapsed += onTimerTick;
            //m_timer.Enabled = true;
            //Task.Factory.StartNew(createHrData);
        }

        public void Stop()
        {
            m_serialPort.Close();
        }

        public IObservable<HRData> HrDataReceived
        {
            get { return m_hrSubject; }
        }

        private void createHrData()
        {
            m_timer.Start();
        }

        private void onTimerTick(object sender, ElapsedEventArgs e)
        {
            int bpm = (int)getRandomNumber(50, 200);
            HRData hrData = new HRData(bpm, DateTime.Now);
            m_hrSubject.OnNext(hrData);
        }

        private double getRandomNumber(double minimum, double maximum)
        {
            Random random = new Random();
            return random.NextDouble() * (maximum - minimum) + minimum;
        }

        private void onDataRecieved(object p_sender, SerialDataReceivedEventArgs p_e)
        {
            byte[] totalMessageBytes = new byte[12];
            byte[] timeBytes = new byte[8];
            byte[] valueBytes = new byte[4];
            (p_sender as SerialPort).Read(totalMessageBytes, 0, 12);

            for (int i = 0; i < 8; i++)
            {
                timeBytes[i] = totalMessageBytes[i];
            }

            int index = 0;
            for (int i = 8; i < 12; i++)
            {
                valueBytes[index] = totalMessageBytes[i];
                index++;
            }

            int value = BitConverter.ToInt16(valueBytes, 0);
            long timeLong = BitConverter.ToInt64(timeBytes, 0);
            DateTime dateTime = DateTime.FromBinary(timeLong);

            HRData hrData = new HRData(value, dateTime);
            m_hrSubject.OnNext(hrData);
        }

        private ISubject<HRData> m_hrSubject = new Subject<HRData>();
        private Timer m_timer = new Timer();
        private UIConfig m_uiConfig;
        private SerialPort m_serialPort;
    }
}
