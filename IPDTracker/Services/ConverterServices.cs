using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace IPDTracker.Services.ConverterServices
{
    class ConvertDateOffset : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var d = (DateTimeOffset)value;
            return d.ToString("dd MMM yyyy");
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }

    class ConvertTimeSpan : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var d = (TimeSpan)value;
            return new DateTime(d.Ticks).ToString("H:mm");
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
