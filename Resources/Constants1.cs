using System;
using static FileRacks.Resources.Constants;

namespace FileRacks.Resources
{
    public static class Constants1
    {
        public static Resolution CurrentResolution = Constants.Resolution.Low;
        public static double ScaleOverride = 0.0;
        public static double DPI = 120;
        public static double BaseFactor = 96 / DPI;
        public static double MinTextBoxHeight = Scale(25);
        public static double MinTextBoxWidth = Scale(200);
        public static double MinButtonHeight = Scale(25);
        public static double MinButtonWidth = Scale(75);

        public static void Initialize()
        {

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

            double scale = (ScaleOverride > 0) ? standard * ScaleOverride : standard * factor * BaseFactor;

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
