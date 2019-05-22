using System;
using System.Globalization;
using Xamarin.Forms;

namespace FoodLog.Converters
{
    public class DoubleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return "0";

            double decimalValue = (double)value;
            return string.Format("{0:N}", decimalValue);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string strValue = value as string;
            if (string.IsNullOrEmpty(strValue))
                return 0;

            double resultDecimal;
            if (double.TryParse(strValue, out resultDecimal))
            {
                return resultDecimal;
            }

            return 0;
        }
    }
}
