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
using System.Threading.Tasks;

namespace AeroVisualizerRedux
{
    public partial class Main : Form
    {
        //Some global stuffs
        WinAPI.DWM_COLORIZATION_PARAMS Backup; //Var for storing their current color scheme

        private const int FFT_PRECISION = 2048;
        private const BASSData BASS_FFT_PRECISION = BASSData.BASS_DATA_FFT2048;
        private int AverageSampleAmount = 1;
        private int SampleInterval = 1;
        private bool ShouldSmoothSample = true;

        private long nextSampleTime = 0;
        
        private long ElapsedTime;

        private bool IsClosing = false;
        private Stopwatch stopWatch = new Stopwatch();
        private float[] fft = new float[FFT_PRECISION];
        private float[] newfft = new float[FFT_PRECISION];
        List<float[]> averagedFFT = new List<float[]>(5);      
        public RuleCollection AudioRules = new RuleCollection();

        public Main()
        {
            InitializeComponent();

            //Store their colors so we can restore them when we close
            BackupColorScheme();

            //Set some inputs/outputs for our rule collection
            AudioRules.AddOutput(new Output("Saturation"));
            AudioRules.AddOutput(new Output("Hue"));
            AudioRules.AddOutput(new Output("Value"));

            AudioRules.AddOutput(new Output("Intensity"));

            //Create the built-in helper methods for the rules
            CreateHelperFunctions();

            //Set some initial values
            AverageSampleAmount = trackSampleNum.Value;
            SampleInterval = (int)numSampleInterval.Value;

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
            Func<float> CurTime = () =>
            {
                return (float)ElapsedTime;
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
            Parallel.For(min, max, i => average += fft[i]);
            
            average = average / (max-min);
            return average;
             
        }

        //perform FFT math
        private int WasapiCallback(IntPtr buffer, int length, IntPtr user)
        {
            //Do these calculations as fast as possible

            if (ShouldSmoothSample && ElapsedTime > nextSampleTime)
            {
                //Retrieve fft data from the wasapi device
                BassWasapi.BASS_WASAPI_GetData(newfft, (int)BASS_FFT_PRECISION);

                //Push it into a list of the last N samples
                if (averagedFFT.Count < AverageSampleAmount)
                    averagedFFT.Add(newfft);

                if (averagedFFT.Count > AverageSampleAmount)
                    averagedFFT.RemoveAt(AverageSampleAmount - 1);

                //Reset our 'average' array
                Array.Clear(fft, 0, fft.Length);

                //Add up all the values
                for (int i = 0; i < averagedFFT.Count; i++)
                {
                    Parallel.For(0, FFT_PRECISION, n => fft[n] = fft[n] + averagedFFT[i][n]);
                }

                //Average all the samples together
                Parallel.For(0, FFT_PRECISION, i => fft[i] = fft[i] / averagedFFT.Count);

                nextSampleTime = ElapsedTime + SampleInterval;
            }


            //Limit how often we actually set the color, this function is run A LOT
            if (stopWatch.ElapsedMilliseconds > numUpdateInterval.Value)
            {
                //Keep tabs on the current time
                ElapsedTime += stopWatch.ElapsedMilliseconds;

                stopWatch.Reset();
                stopWatch.Start();

                //Use the old method if smoothing is disabled
                if (!ShouldSmoothSample)
                {
                    //Retrieve fft data from the wasapi device
                    BassWasapi.BASS_WASAPI_GetData(fft, (int)BASS_FFT_PRECISION);
                }

                //IT'S TIME TO LAY DOWN SOME GROUND RULES
                float saturation = AudioRules.GetRulesOutput("Saturation", 1);
                float hue = AudioRules.GetRulesOutput("Hue");
                float value = AudioRules.GetRulesOutput("Value", 1);

                int r, g, b = 0;

                Utils.HsvToRgb(hue, saturation, value, out r, out g, out b);

                Utils.SetDwmColor(Color.FromArgb((int)Utils.Clamp(0, 255, (saturation * 255)), r, g, b));
                Utils.SetDwmAlpha((long)AudioRules.GetRulesOutput("Intensity", 1)*100);
            }

            //always always return length, we don't want to halt anything
            return length;
        }

        private void OnApplicationExit(object sender, EventArgs e)
        {
            Utils.SetDwmColor(System.Drawing.Color.FromArgb((int)Backup.Color));
            WinAPI.DwmSetColorizationParameters(ref Backup, false);
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

        private void checkSmooth_CheckedChanged(object sender, EventArgs e)
        {
            trackSampleNum.Enabled      = checkSmooth.Checked;
            labelSampleAmount.Enabled   = checkSmooth.Checked;
            labelSampleInterval.Enabled = checkSmooth.Checked;
            numSampleInterval.Enabled   = checkSmooth.Checked;
        }

        private void trackSampleNum_Scroll(object sender, EventArgs e)
        {
            labelSampleAmount.Text = string.Format("Sample Amount ({0})", trackSampleNum.Value);
            AverageSampleAmount = trackSampleNum.Value;
        }

        private void numSampleInterval_ValueChanged(object sender, EventArgs e)
        {
            SampleInterval = (int)numSampleInterval.Value;
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
