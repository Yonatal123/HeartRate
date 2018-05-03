using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerialPortDeviceSimulator
{
    public class CommConfig
    {
        public string SerialPortName { get; set; }
        public int SendFrequencyInSeconds { get; set; }
    }
}
