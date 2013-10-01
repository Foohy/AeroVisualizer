using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Un4seen.Bass;
using Un4seen.BassWasapi;
using System.Diagnostics;
using System.Dynamic;
using Microsoft.CSharp;
using RuleManager;

namespace AeroVisualizerRedux
{
    public partial class Main : Form
    {
        //Some global stuffs
        WinAPI.DWM_COLORIZATION_PARAMS Backup; //Var for storing their current color scheme

        private const int FFT_PRECISION = 2048;
        private const BASSData BASS_FFT_PRECISION = BASSData.BASS_DATA_FFT2048;

        private long ElapsedTime;

        private float BASS_CUTTOFF = 64; //When to stop sampling the bass
        private float HUE = 0;
        private bool IsClosing = false;
        private Stopwatch stopWatch = new Stopwatch();
        private float[] fft = new float[FFT_PRECISION];
        public RuleCollection AudioRules = new RuleCollection();

        private Output outIntensity;
        private Output outHue;

        public Main()
        {
            InitializeComponent();

            //Store their colors so we can restore them when we close
            BackupColorScheme();

            //Set some inputs/outputs for our rule collection
            outIntensity = AudioRules.AddOutput(new Output("Intensity"));
            outHue = AudioRules.AddOutput(new Output("Hue"));

            //Create the built-in helper methods for the rules
            CreateHelperFunctions();

            //This is so you don't see the dumb HERPADERP BASS IS STARTING spash screen
            BassNet.Registration("swkauker@yahoo.com", "2X2832371834322");

            //Create our initial wasapi device
            WasapiDevice.SetDevice(WasapiDevice.RetrieveDefaultDevice());
            WasapiDevice.SetDelegate(new WasapiDevice.FFTThink(WasapiCallback));
            WasapiDevice.Start();

            //Enumerate through all available devices and list em as possibilities
            BASS_WASAPI_DEVICEINFO[] wasapiDevices = BassWasapi.BASS_WASAPI_GetDeviceInfos();
            int devnum = 1;
            for (int i = 0; i < wasapiDevices.Length; i++)
            {
                BASS_WASAPI_DEVICEINFO info = wasapiDevices[i];

                if (!info.IsInput && info.IsEnabled )
                {
                    devnum = i + 1;
                    int index = comboDeviceSelect.Items.Add(new DeviceInfo(info, devnum));

                    if (devnum == WasapiDevice.CurrentDevice)
                    {
                        comboDeviceSelect.SelectedIndex = index;
                    }
                }
            }

            stopWatch.Start();
        }

        /// <summary>
        /// Create the helper functions for the customizable rules
        /// </summary>
        private void CreateHelperFunctions()
        {
            Func<long> CurTime = () =>
            {
                return ElapsedTime;
            };
            AudioRules.Util.CurTime = CurTime;

            Func<int, int, float> RangedAverage = (min, max) =>
            {
                return GetRangedAverage(min, max);
            };
            AudioRules.Util.GetRangedAverage = RangedAverage;

            Func<float> GetAverage = () =>
            {
                return GetRangedAverage(0, WasapiDevice.CurrentDeviceInfo.mixfreq);
            };
            AudioRules.Util.GetAverage = GetAverage;
        }

        private void BackupColorScheme()
        {
            WinAPI.DWM_COLORIZATION_PARAMS Temp;
            //Store their Original color settings, so they don't get angry!
            WinAPI.DwmGetColorizationParameters(out Backup);

            Temp.Color = Backup.Color;
            Temp.AfterglowColor = Backup.AfterglowColor;
            Temp.Intensity = Backup.Intensity;
            Temp.AfterGlowBalance = Backup.AfterGlowBalance;
            Temp.BlurBalance = Backup.BlurBalance;
            Temp.GlassReflInt = Backup.GlassReflInt;
            Temp.Opaque = Backup.Opaque;

            WinAPI.DwmSetColorizationParameters(ref Temp, false);

            // Handle the ApplicationExit event to know when the application is exiting.
            Application.ApplicationExit += new EventHandler(this.OnApplicationExit);
        }

        /// <summary>
        /// Get the ranged average of the FFT between the minimum and maximum frequencies
        /// </summary>
        /// <param name="min">The lowest frequency to include in averaging</param>
        /// <param name="max">The highest frequency to include in averaging</param>
        /// <returns>The average magnitude of the frequencies between min and max</returns>
        private float GetRangedAverage(int min, int max)
        {
            //Convert min and max to be from frequncies down to numbers within the fft array
            min = (int)((min / (float)WasapiDevice.CurrentDeviceInfo.mixfreq) * (float)FFT_PRECISION);
            max = (int)((max / (float)WasapiDevice.CurrentDeviceInfo.mixfreq) * (float)FFT_PRECISION);
            float average = 0;
            for (int i = min; i < max; i++)
            {
                average += fft[i];
            }
            average = average / (max-min);
            return average;
        }

        //perform FFT math
        private int WasapiCallback(IntPtr buffer, int length, IntPtr user)
        {
            //Limit how often we set the color, this function is run A LOT
            if (stopWatch.ElapsedMilliseconds > numUpdateInterval.Value)
            {
                //Keep tabs on the current time
                ElapsedTime += stopWatch.ElapsedMilliseconds;

                stopWatch.Reset();
                stopWatch.Start();

                //Retrieve fft data from the wasapi device
                BassWasapi.BASS_WASAPI_GetData(fft, (int)BASS_FFT_PRECISION);

                //IT'S TIME TO LAY DOWN SOME GROUND RULES
                float average = AudioRules.GetRulesOutput(outIntensity);


                int r, g, b = 0;
                average *= (float)numMultiplier.Value;

                Utils.HsvToRgb(HUE, average, 1, out r, out g, out b);

                Utils.SetDwmColor(Color.FromArgb((int)Utils.Clamp(0, 255, (average * 255)), r, g, b));
                Utils.SetDwmAlpha((int)Utils.Clamp(0, 100, (average * 100)));
            }

            //always always return length, we don't want to halt anything
            return length;
        }

        private void numSampleCutoff_ValueChanged(object sender, EventArgs e)
        {
            BASS_CUTTOFF = (float)numSampleCutoff.Value;
        }

        private void sliderHue_Scroll(object sender, EventArgs e)
        {
            HUE = sliderHue.Value;
            labelHue.Text = HUE.ToString();
        }

        private void OnApplicationExit(object sender, EventArgs e)
        {
            Utils.SetDwmColor(System.Drawing.Color.FromArgb((int)Backup.Color));
            WinAPI.DwmSetColorizationParameters(ref Backup, false);
        }

        private void SlideTimer_Tick(object sender, EventArgs e)
        {
            HUE += (float)slideScrollSpeed.Value / (float)100;

            labelHue.Text = HUE.ToString();

            if ( HUE > 358)
            {
                HUE = 0;
            }

            sliderHue.Value = (int)HUE;
        }

        private void slideScrollSpeed_Scroll(object sender, EventArgs e)
        {
            labelColor.Text = slideScrollSpeed.Value.ToString();
        }

        private void comboDeviceSelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Get the device index from the selected device
            DeviceInfo info = (DeviceInfo)comboDeviceSelect.Items[comboDeviceSelect.SelectedIndex];
            if (info == null) return;

            WasapiDevice.Stop();

            WasapiDevice.SetDevice(info.WasapiDeviceNum);
            WasapiDevice.SetDelegate(new WasapiDevice.FFTThink(WasapiCallback));
            WasapiDevice.Start();
        }

        private void Main_FormClosed(object sender, FormClosedEventArgs e)
        {
            WasapiDevice.Stop();
            Bass.BASS_Free();
        }

        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.IsClosing = true;
        }

        private void btnAdvanced_Click(object sender, EventArgs e)
        {
            RulesEditor edit = new RulesEditor(this);
            edit.ShowDialog();
        }
    }

    class DeviceInfo
    {
        public BASS_WASAPI_DEVICEINFO WasapiDeviceInfo { get; private set; }
        public int WasapiDeviceNum { get; private set; }

        public DeviceInfo(BASS_WASAPI_DEVICEINFO info, int num )
        {
            WasapiDeviceInfo = info;
            WasapiDeviceNum = num;
        }

        public override string ToString()
        {
            return WasapiDeviceInfo.name;
        }
    }
}
