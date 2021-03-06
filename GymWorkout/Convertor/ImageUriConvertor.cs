﻿using System;
using System.Globalization;
using System.Windows.Data;

namespace GymWorkout.Convertor
{
    class ImageUriConvertor : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (parameter != null)
                if (parameter.ToString().ToLower() == "image")
                    return AppDomain.CurrentDomain.BaseDirectory + @"Images\ProductImages\Image\" + value;
                else
                    return AppDomain.CurrentDomain.BaseDirectory + @"Images\ProductImages\Thumb\" + value;

            return AppDomain.CurrentDomain.BaseDirectory + @"Images\ProductImages\Thumb\" + value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
