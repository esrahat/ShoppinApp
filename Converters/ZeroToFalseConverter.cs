using System;
using System.Globalization;
using Microsoft.Maui.Controls;

namespace ShopNow.Converters
{
    public class ZeroToFalseConverter : IValueConverter
    {
        public object Convert(object? value, Type? targetType, object? parameter, CultureInfo? culture)
        {
            // Return false if amount is 0, otherwise return true


            int? amount = (int?)value;

            return amount > 0;
        }

        public object ConvertBack(object? value, Type? targetType, object? parameter, CultureInfo? culture)
        {
            throw new NotImplementedException();
        }
    }
}