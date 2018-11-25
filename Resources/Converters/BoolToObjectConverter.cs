using System;
using System.Collections;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace FileRacks.Resources
{
    public class BoolToObjectConverter : DependencyObject, IValueConverter
    {
        //--------------------------------------------------------------------------------------
        /// <summary>
        /// Default constructor.
        /// </summary>
        public BoolToObjectConverter()
        {
        }

        public static readonly DependencyProperty TrueValueProperty =
            DependencyProperty.Register("TrueValue", typeof(object), typeof(BoolToObjectConverter), new UIPropertyMetadata());
        public object TrueValue
        {
            get { return (object)GetValue(TrueValueProperty); }
            set { SetValue(TrueValueProperty, value); }
        }

        public static readonly DependencyProperty FalseValueProperty =
            DependencyProperty.Register("FalseValue", typeof(object), typeof(BoolToObjectConverter), new UIPropertyMetadata());
        public object FalseValue
        {
            get { return (object)GetValue(FalseValueProperty); }
            set { SetValue(FalseValueProperty, value); }
        }

        //--------------------------------------------------------------------------------------
        /// <summary>
        /// Constructor accepting OnTrue and OnFalse.
        /// </summary>
        /// <param name="onTrue">Value to convert to if bool is true.</param>
        /// <param name="onFalse">Value to convert to if bool is false.</param>
        public BoolToObjectConverter(object onTrue, object onFalse)
        {
            _onTrue = onTrue;
            _onFalse = onFalse;
        }

        //--------------------------------------------------------------------------------------
        /// <summary>
        /// Gets or sets object to return when the boolean parameter is true.
        /// </summary>
        public object OnTrue
        {
            get { return _onTrue; }
            set { _onTrue = value; }
        }

        //--------------------------------------------------------------------------------------
        /// <summary>
        /// Gets or sets object to return when the boolean parameter is false.
        /// </summary>
        public object OnFalse
        {
            get { return _onFalse; }
            set { _onFalse = value; }
        }

        //--------------------------------------------------------------------------------------
        /// <summary>
        /// Allows you to change the operation applied to determine the true/false value.
        /// </summary>
        public string Op
        {
            get { return _op; }
            set
            {
                _op = value.ToUpper().Trim();
                //Ensure.Cond(_op == "IS" || _op == "ISNOT");
            }
        }

        //--------------------------------------------------------------------------------------
        /// <summary>
        /// Allows you to set the operand used with the Op to determine the true/false value.
        /// </summary>
        public string Operand
        {
            get { return _operand; }
            set { _operand = value; }
        }

        //--------------------------------------------------------------------------------------
        /// <summary>
        /// Converts a boolean value to an object, applying an optional boolean operation.
        /// If the value passed in is not a boolean, or is not compatible with the operation
        /// specified, OnFalse will be returned.
        /// </summary>
        /// <param name="value">The value to convert. May be null.</param>
        /// <returns>An object based on OnTrue or OnFalse properties.</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (_op == "IS" || _op == "ISNOT")
            {
                _operand = _operand.ToUpper().Trim();

                // default to the "IS" condition
                bool val = value == null;

                if ((_operand == "VISIBLE") && (!val))
                {
                    Visibility viz = (Visibility)value;
                    val = viz == Visibility.Visible;
                }
                else if (_operand == "ZERO" && !val)
                {
                    if (value.GetType() == typeof(string))
                    {
                        string s = (string)value;
                        val = s.Length <= 0;
                    }
                    else if (value.GetType() == typeof(int))
                    {
                        int i = (int)value;
                        val = i == 0;
                    }
                    else if (value.GetType() == typeof(double))
                    {
                        double d = (double)value;
                        val = Math.Abs(d) < 0.01;
                    }
                    else if (value.GetType() == typeof(Thickness))
                    {
                        Thickness thick = (Thickness)value;
                        if (thick.Top < 1 && thick.Bottom < 1 &&
                            thick.Left < 1 && thick.Right < 1)
                        {
                            val = true;
                        }
                    }
                    else if (value.GetType() == typeof(CornerRadius))
                    {
                        CornerRadius corner = (CornerRadius)value;
                        if (corner.TopLeft < 1 && corner.TopRight < 1 &&
                            corner.BottomLeft < 1 && corner.BottomRight < 1)
                        {
                            val = true;
                        }
                    }
                    else if (value is ICollection)
                    {
                        ICollection list = value as ICollection;
                        if (list == null || list.Count <= 0)
                        {
                            val = true;
                        }
                    }
                    // else, unknown type for value; val is already false
                }
                if (_op == "ISNOT")
                {
                    val = !val;
                }
                return val ? OnTrue : OnFalse;
            }

            // default handling of simple case
            else if ((value is bool) && ((bool)value == true))
            {
                return OnTrue;
            }

            return OnFalse;
        }

        //--------------------------------------------------------------------------------------
        /// <summary>
        /// This method is not implemented and will throw an exception.
        /// </summary>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #region Private stuff
        private object _onTrue = null;
        private object _onFalse = null;
        private string _op = null;
        private string _operand = null;
        #endregion
    }
}
