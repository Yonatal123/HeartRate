using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reactive.Subjects;
using System.Reactive.Concurrency;
using System.Timers;

namespace HeartRate
{
    public class HeartRateListener : IHeartRateListener
    {
        public void Start()
        {
            m_timer.Interval = 10000;
            m_timer.Elapsed += onTimerTick;
            m_timer.Enabled = true;
            Task.Factory.StartNew(createHrData);
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


        private ISubject<HRData> m_hrSubject;
        private Timer m_timer = new Timer();
    }
}
