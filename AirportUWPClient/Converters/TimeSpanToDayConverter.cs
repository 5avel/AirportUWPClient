using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace AirportUWPClient.Converters
{
    class TimeSpanToDayConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            try
            {
                TimeSpan date = (TimeSpan)value;
                return date.Days;
            }
            catch (Exception ex)
            {
                return TimeSpan.MinValue;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            try
            {
                int dto = (int)value;
                return new TimeSpan(dto, 0, 0, 0);
            }
            catch (Exception ex)
            {
                return TimeSpan.MinValue;
            }
        }
    }
}
