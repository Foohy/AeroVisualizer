using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AeroVisualizerRedux
{
    class Utils
    {
        public static float Clamp(float min, float max, float value)
        {
            if (value > max) { return max; }
            if (value < min) { return min; }
            return value;
        }

        public static int Clamp(int min, int max, int value)
        {
            if (value > max) { return max; }
            if (value < min) { return min; }
            return value;
        }

        /// <summary>
        /// Convert HSV to RGB
        /// h is from 0-360
        /// s,v values are 0-1
        /// r,g,b values are 0-255
        /// Based upon http://ilab.usc.edu/wiki/index.php/HSV_And_H2SV_Color_Space#HSV_Transformation_C_.2F_C.2B.2B_Code_2
        /// </summary>
        public static void HsvToRgb(double h, double S, double V, out int r, out int g, out int b)
        {
            // ######################################################################
            // T. Nathan Mundhenk
            // mundhenk@usc.edu
            // C/C++ Macro HSV to RGB

            double H = h;
            while (H < 0) { H += 360; };
            while (H >= 360) { H -= 360; };
            double R, G, B;
            if (V <= 0)
            { R = G = B = 0; }
            else if (S <= 0)
            {
                R = G = B = V;
            }
            else
            {
                double hf = H / 60.0;
                int i = (int)Math.Floor(hf);
                double f = hf - i;
                double pv = V * (1 - S);
                double qv = V * (1 - S * f);
                double tv = V * (1 - S * (1 - f));
                switch (i)
                {

                    // Red is the dominant color

                    case 0:
                        R = V;
                        G = tv;
                        B = pv;
                        break;

                    // Green is the dominant color

                    case 1:
                        R = qv;
                        G = V;
                        B = pv;
                        break;
                    case 2:
                        R = pv;
                        G = V;
                        B = tv;
                        break;

                    // Blue is the dominant color

                    case 3:
                        R = pv;
                        G = qv;
                        B = V;
                        break;
                    case 4:
                        R = tv;
                        G = pv;
                        B = V;
                        break;

                    // Red is the dominant color

                    case 5:
                        R = V;
                        G = pv;
                        B = qv;
                        break;

                    // Just in case we overshoot on our math by a little, we put these here. Since its a switch it won't slow us down at all to put these here.

                    case 6:
                        R = V;
                        G = tv;
                        B = pv;
                        break;
                    case -1:
                        R = V;
                        G = pv;
                        B = qv;
                        break;

                    // The color is not defined, we should throw an error.

                    default:
                        //LFATAL("i Value error in Pixel conversion, Value is %d", i);
                        R = G = B = V; // Just pretend its black/white
                        break;
                }
            }
            r = Clamp((int)(R * 255.0));
            g = Clamp((int)(G * 255.0));
            b = Clamp((int)(B * 255.0));
        }

        /// <summary>
        /// Clamp a value to 0-255
        /// </summary>
        static int Clamp(int i)
        {
            if (i < 0) return 0;
            if (i > 255) return 255;
            return i;
        }

        public static bool IsGlassAvailable()
        {
            return (System.Environment.OSVersion.Version.Major >= 6 && System.Environment.OSVersion.Version.Build >= 5600) && System.IO.File.Exists(System.Environment.SystemDirectory + @"\dwmapi.dll");
        }

        public static bool IsGlassEnabled()
        {
            bool result;
            WinAPI.DwmIsCompositionEnabled(out result);
            return result;
        }

        public static void RemoveFromAeroPeek(IntPtr hwnd)
        {
            if (IsGlassAvailable() && System.Environment.OSVersion.Version.Major == 6 &&
                System.Environment.OSVersion.Version.Minor == 1)
            {
                var attrValue = 1; // True
                WinAPI.DwmSetWindowAttribute(hwnd, 12, ref attrValue, sizeof(int));
            }
        }


        private void ExtendGlassFrame(IntPtr hwnd, ref WinAPI.Margins margins)
        {
            if (IsGlassAvailable())
                WinAPI.DwmExtendFrameIntoClientArea(hwnd, ref margins);
        }

        public static void MakeGlassRegion(ref IntPtr handle, IntPtr rgn)
        {
            if (IsGlassAvailable() && rgn != IntPtr.Zero)
            {
                var bb = new WinAPI.BbStruct
                {
                    Enable = true,
                    Flags = WinAPI.BbFlags.DwmBbEnable | WinAPI.BbFlags.DwmBbBlurregion,
                    Region = rgn
                };
                WinAPI.DwmEnableBlurBehindWindow(handle, ref bb);
            }
        }

        public static void RemoveGlassRegion(ref IntPtr handle)
        {
            if (IsGlassAvailable())
            {
                var bb = new WinAPI.BbStruct
                {
                    Enable = false,
                    Flags = WinAPI.BbFlags.DwmBbEnable | WinAPI.BbFlags.DwmBbBlurregion,
                    Region = IntPtr.Zero
                };
                WinAPI.DwmEnableBlurBehindWindow(handle, ref bb);
            }
        }

        public static void SetDwmAlpha(long alpha)
        {
            WinAPI.DWM_COLORIZATION_PARAMS color;
            WinAPI.DwmGetColorizationParameters(out color);

            color.AfterglowColor = alpha;

            WinAPI.DwmSetColorizationParameters(ref color, false);
        }

        public static void SetDwmColor(System.Drawing.Color newColor)
        {
            if (WinAPI.DwmIsCompositionEnabled())
            {
                WinAPI.DWM_COLORIZATION_PARAMS color;
                WinAPI.DwmGetColorizationParameters(out color);

                color.Color = System.Drawing.Color.FromArgb(255, newColor.R, newColor.G, newColor.B).ToArgb();

                WinAPI.DwmSetColorizationParameters(ref color, false);
            }
        }
    }
}
