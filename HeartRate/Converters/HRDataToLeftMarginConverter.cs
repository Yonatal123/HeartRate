﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace HeartRate
{
    public class HRDataToLeftMarginConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            int itemsCount = (int)values[0];
            var item = (HRData)values[1];
            int itemIndex = item.Index;
            double graphWidth = (double)values[2];
            double graphHeight = (double)values[3];

            if(itemIndex == 0)
            {
                return new Thickness(-15, graphHeight, 0, 0);
            }
            double leftMargin = (ScaleConverterUtility.GetLeftOffset(graphWidth, itemsCount)) * itemIndex;
            return new Thickness(leftMargin - 15, graphHeight, 0, 0);
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
