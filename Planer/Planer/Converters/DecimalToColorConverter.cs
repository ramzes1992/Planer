using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows.Data;
using System.Windows.Media;

namespace Planer.Converters
{
    public class DecimalToColorConverter : IValueConverter
    {
        private readonly Color GreaterThanZeroColor = Colors.Green;
        private readonly Color LessThanZeroColor = Colors.Red;
        private readonly Color EqualsZeroColor = Colors.Black;

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value != null)
            {
                var _value = (decimal)value;

                if (_value > 0)
                {
                    return new SolidColorBrush(GreaterThanZeroColor);
                }
                else if (_value < 0)
                {
                    return new SolidColorBrush(LessThanZeroColor);
                }
            }

            return new SolidColorBrush(EqualsZeroColor);
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
