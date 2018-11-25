using System;
using System.Windows;
using System.Windows.Data;

namespace FileRacks.Resources
{
    /// <summary>
    /// A converter class to use when binding Grid column-width or row-height.
    /// </summary>
    /// <remarks>
    /// This is just a thin repackaging of the built-in GridLengthConverter functionality
    /// to get the IValueConverter interface support.
    /// </remarks>
    public class GridValueConverter : IValueConverter
    {
        //--------------------------------------------------------------------------------------
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return _converter.ConvertFrom(value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return _converter.ConvertTo(value, targetType);
        }

        private static GridLengthConverter _converter = new GridLengthConverter();
    }
}
