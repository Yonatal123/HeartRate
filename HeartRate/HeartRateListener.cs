using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeartRate
{
    public class HeartRateListener : IHeartRateListener
    {
        public int Bmp
        {
            get { return 55; }
            set
            {
                Bmp = value;
            }
        }
    }
}
