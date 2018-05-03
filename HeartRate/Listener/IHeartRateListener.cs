using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeartRate
{
    public interface IHeartRateListener
    {
        void Start();
        void Stop();
        IObservable<HRData> HrDataReceived { get; }
    }
}
