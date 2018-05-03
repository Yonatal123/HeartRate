using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeartRate
{
    public class UIConfig
    {
        public int MaxValue { get; set; }
        public int MinValue { get; set; }
        public int NumOfVisibleItems { get; set; }
        public string SerialPortName { get; set; }
        public int SamplingFrequencyInSeconds { get; set; }
        public bool UseSerialPortSimulator { get; set; }
        public int StopListeningTimeInMinutes { get; set; }
    }
}
