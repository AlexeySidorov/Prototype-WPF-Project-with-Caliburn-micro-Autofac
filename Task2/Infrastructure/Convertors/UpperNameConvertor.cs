using System;
using System.Globalization;
using System.Text;
using System.Windows.Data;
using static System.Char;

namespace Task2.Infrastructure.Convertors
{
    public class UpperNameConvertor : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return null;

            var str = value.ToString();
            var sb = new StringBuilder(str);
            sb[0] = ToUpper(sb[0]);
            str = sb.ToString();

            return str;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
