using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace FileRacks.Resources
{
    public static class Constants
    {
        /// <summary>
        /// Enum to denote what the current resolution of the system is.
        /// </summary>
        public enum Resolution
        {
            Low,
            Medium,
            High,
            ExtraHigh
        }

        #region Private Members
        private static bool _initialized;
        private static Resolution _currentResolution;
        private static double _scaleOverride;
        private static bool _isTouch;
        private static Thickness _bigGap;
        private static double _bigGapPixels;
        private static double _appMinWidth;
        private static double _appMinHeight;
        private static double _baseFactor = 1.0;
        private static double _appHeaderWOverflow;
        private static double _appHeaderHeight;
        private static double _userInfoWidth;
        private static double _navigationLogoHeaderHeight;
        private static Thickness _navButtonMargin;
        private static Thickness _gap;
        private static double _gapPixels;
        private static double _thinPixels;
        private static Thickness _bottomThin;
        private static Thickness _topThin;
        private static Thickness _rightThin;
        private static Thickness _leftThin;
        private static Thickness _horizontalThin;
        private static Thickness _verticalThin;
        private static CornerRadius _cornerRadius;
        private static double _cornerRadiusPixels;
        private static Thickness _borderThickness;
        private static CornerRadius _squareRight;
        private static CornerRadius _square;
        private static double _controlMediumWidth;
        private static double _controlMediumHeight;
        private static double _standardColumnWidth;
        private static double _mediumColumnWidth;        
        private static double _largeColumnWidth;
        private static double _smallColumnWidth;
        private static double _wideColumnWidth;
        private static double _extraWideColumnWidth;
        private static double _bodyTextFontSize = 30;
        private static double _touchSize;
        private static double _buttonMinHeight;
        private static double _buttonMinWidth;
        #endregion Private Members

        #region Public Properties
        static public FontFamily FontFamily
        { get { return new FontFamily("Comic Sans MS"); } }

        public static double BodyTextFontSize
        { get { Initialize(); return _bodyTextFontSize; } }

        public static double TouchSize
        { get { Initialize(); return _touchSize; } }

        public static double ButtonMinHeight
        { get { Initialize(); return _buttonMinHeight; } }

        public static double ButtonMinWidth
        { get { Initialize(); return _buttonMinWidth; } }

        public static Resolution CurrentResolution
        { get { Initialize(); return _currentResolution; } }

        public static Thickness BigGap
        { get { Initialize(); return _bigGap; } }

        public static double BigGapPixels
        { get { Initialize(); return _bigGapPixels; } }

        public static double AppHeaderHeight
        { get { Initialize(); return _appHeaderHeight; } }

        public static double AppHeaderWOverflow
        { get { Initialize(); return _appHeaderWOverflow; } }

        public static double UserInfoWidth
        { get { Initialize(); return _userInfoWidth; } }

        public static double NavigationLogoHeaderHeight
        { get { Initialize(); return _navigationLogoHeaderHeight; } }

        public static Thickness NavButtonMargin
        { get { Initialize(); return _navButtonMargin; } }

        public static double GapPixels
        { get { Initialize(); return _gapPixels; } }

        public static Thickness Gap
        { get { Initialize(); return _gap; } }

        public static Thickness BottomThin
        { get { Initialize(); return _bottomThin; } }

        public static Thickness RightThin
        { get { Initialize(); return _rightThin; } }

        public static Thickness LeftThin
        { get { Initialize(); return _leftThin; } }

        public static Thickness HorizontalThin
        { get { Initialize(); return _horizontalThin; } }

        public static Thickness VerticalThin
        { get { Initialize(); return _verticalThin; } }

        public static double ThinPixels
        { get { Initialize(); return _thinPixels; } }

        public static double CornerRadiusPixels
        { get { Initialize(); return _cornerRadiusPixels; } }

        public static CornerRadius CornerRadius
        { get { Initialize(); return _cornerRadius; } }

        public static Thickness BorderThickness
        { get { Initialize(); return _borderThickness; } }

        public static CornerRadius Square
        { get { Initialize(); return _square; } }

        public static CornerRadius SquareRight
        { get { Initialize(); return _squareRight; } }

        public static double ControlMediumWidth
        { get { Initialize(); return _controlMediumWidth; } }

        public static double ControlMediumHeight
        { get { Initialize(); return _controlMediumHeight; } }

        public static double LargeColumnWidth
        { get { Initialize(); return _largeColumnWidth; } }

        public static double StandardColumnWidth
        { get { Initialize(); return _standardColumnWidth; } }

        public static double MediumColumnWidth
        { get { Initialize(); return _mediumColumnWidth; } }

        public static double SmallColumnWidth
        { get { Initialize(); return _smallColumnWidth; } }

        public static double WideColumnWidth
        { get { Initialize(); return _wideColumnWidth; } }

        public static double ExtraWideColumnWidth
        { get { Initialize(); return _extraWideColumnWidth; } }
        #endregion Public Properties

        public static void Initialize(Resolution? resOverride = null, bool noTouch = false,
                                        bool forceTouch = false, double scaleOverride = 0.0)
        {
            if (_initialized)
            {
                if (resOverride.HasValue)
                {
                    //Log.Warning("Call to Constants.Initialize() made after initialization!");
                }
                return;
            }

            _initialized = true;
            _currentResolution = Resolution.Low;
            float dpi = 0;
            _isTouch = forceTouch || (!noTouch && HasTouchInput());
            _scaleOverride = scaleOverride;

            _appMinWidth = (_scaleOverride > 0) ? _scaleOverride * 1280 : 1280;
            _appMinHeight = (_scaleOverride > 0) ? _scaleOverride * 720 : 720;

            // Check condition for the application minimum width
            if (SystemParameters.WorkArea.Width <= _appMinWidth)
            {
                _appMinWidth = SystemParameters.WorkArea.Width;
            }
            // Check condition for the application minimum height 	                
            if (SystemParameters.WorkArea.Height <= _appMinHeight)
            {
                _appMinHeight = SystemParameters.WorkArea.Height;
            }

            using (Graphics gfx = Graphics.FromHwnd(IntPtr.Zero))
            {
                dpi = Math.Min(gfx.DpiX, gfx.DpiY);
            }

            // use dpi to figure out effective resolution: for non-touch, scale
            // the resolution down one level
            if (dpi > 240)
            {
                _currentResolution = _isTouch ? Resolution.ExtraHigh : Resolution.High;
            }
            else if (dpi > 160)
            {
                _currentResolution = _isTouch ? Resolution.High : Resolution.Medium;
            }
            else if (dpi > 120)
            {
                _currentResolution = _isTouch ? Resolution.Medium : Resolution.Low;
            }

            // need to compensate for Windows' ugly Smaller/Medium/Larger scaling
            // nonsense -- note that when anything but Smaller is used there is bitmap-
            // level scaling involved that "fuzzes" out the display and distorts things
            _baseFactor = 96 / dpi;

            string res = string.Empty;
            if (resOverride != null)
            {
                //Log.Info("Using override resolution: {0}", _currentResolution);
                _currentResolution = resOverride.Value;
            }
            else if (res != "")
            {
                //Log.Info("Using spoofing resolution: {0}", res);
                _currentResolution = (Resolution)Enum.Parse(typeof(Resolution), res);
            }
            else
            {
                //Log.Info("Min DPi {0}, resolution {1}", dpi, _currentResolution);
            }
            //Log.Info("UI scaling: res {0}  x  windows {1} = {2}", Scale(1) / _baseFactor,
            //         _baseFactor, Scale(1));

            _buttonMinHeight = Scale(20);
            _buttonMinWidth = Scale(48);
            _bodyTextFontSize = Scale(20);
            _touchSize = Scale(28);
            _gapPixels = Scale(3);
            _gap = new Thickness(_gapPixels);
            _bigGapPixels = Scale(8);
            _bigGap = new Thickness(_bigGapPixels);
            _appHeaderHeight = Scale(65);
            _appHeaderWOverflow = _appHeaderHeight + _bigGapPixels;
            _userInfoWidth = Scale(175);
            _navigationLogoHeaderHeight = Scale(60);
            _navButtonMargin = new Thickness(_gapPixels, 0, 0, 0);

            double border = Scale(2);
            double thin = border / 2;
            double narrow = border / 4;
            _thinPixels = thin;
            _bottomThin = new Thickness(border, border, border, thin);
            _topThin = new Thickness(border, thin, border, border);
            _rightThin = new Thickness(border, border, thin, border);
            _leftThin = new Thickness(thin, border, border, border);
            _horizontalThin = new Thickness(border, thin, border, thin);
            _verticalThin = new Thickness(thin, border, thin, border);

            double rad = Scale(8);
            _cornerRadiusPixels = rad;
            _cornerRadius = new CornerRadius(rad);

            _borderThickness = new Thickness(border);
            _square = new CornerRadius(0);
            _squareRight = new CornerRadius(rad, 0, 0, rad);
            _controlMediumWidth = Scale(133);
            _controlMediumHeight = Scale(850);
            _largeColumnWidth = Scale(300);
            _standardColumnWidth = Scale(200);
            _mediumColumnWidth = Scale(150);
            _smallColumnWidth = Scale(100);
            _wideColumnWidth = Scale(700);
            _extraWideColumnWidth = Scale(850);
        }

        private static bool HasTouchInput()
        {
            foreach (TabletDevice tabletDevice in Tablet.TabletDevices)
            {
                if (tabletDevice.Type == TabletDeviceType.Touch)
                    return true;
            }

            return false;
        }

        public static double Scale(double standard, bool round = true)
        {
            double factor = 1.0;
            switch (CurrentResolution)
            {
                case Resolution.Low:
                    factor = 0.75;
                    break;
                case Resolution.Medium:
                    factor = 1.0;
                    break;
                case Resolution.High:
                    factor = 1.5;
                    break;
                default:
                    factor = 2.0;
                    break;
            }

            double scale = (_scaleOverride > 0) ? standard * _scaleOverride : standard * factor * _baseFactor;

            if (round)
            {
                scale = Math.Round(scale);
            }

            if (scale < 0)
            {
                return Math.Min(-1, scale);
            }
            else
            {
                return Math.Max(1, scale);
            }
        }
    }
}
