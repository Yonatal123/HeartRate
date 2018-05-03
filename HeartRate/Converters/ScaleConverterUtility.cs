using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeartRate
{
    public static class ScaleConverterUtility
    {
        public static double GetLeftOffset(double p_width, int p_numOfItems)
        {
            return p_width / (p_numOfItems - 1);
        }

        public static double GetTopOffset(double p_graphHeight, int p_bpm, int p_minValue, int p_maxValue)
        {
            int valuesRange = p_maxValue - p_minValue;
            return p_graphHeight - ((p_bpm - p_minValue) * (p_graphHeight / valuesRange));
        }
    }
}
