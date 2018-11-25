using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace FileRacks.UI.Converters
{
    public class TitleTextConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null && value.GetType() == typeof(TextBlock))
            {
                TitleText txt = new TitleText();
                //txt.Text = value.ToString();
                txt.Text = (value as TextBlock).Text;
                (value as TextBlock).Text = txt.Text;
                txt.TextAlignment = TextAlignment.Center;
                txt.TextWrapping = TextWrapping.Wrap;

                return value;
            }
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }

    public class TitleText : TextBlock
    {
        public TitleText()
        {
        }

        static TitleText()
        {
            TextBlock.TextProperty.OverrideMetadata(typeof(TitleText),
                    new FrameworkPropertyMetadata("", default(PropertyChangedCallback),
                                                    (CoerceValueCallback)CoerceToUpper));
        }

        private static object CoerceToUpper(DependencyObject d, object baseValue)
        {
            if (baseValue == null)
            {
                return "";
            }
            else if (baseValue is string)
            {
                return ((string)baseValue).ToUpper();
            }
            return baseValue;
        }
    }
}
