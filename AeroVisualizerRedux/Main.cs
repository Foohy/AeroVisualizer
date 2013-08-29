﻿using System;
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

namespace AeroVisualizerRedux
{
    public partial class Main : Form
    {
        //Some global stuffs
        WinAPI.DWM_COLORIZATION_PARAMS Backup; //Var for storing their current color scheme

        private DSPPROC dspProc;
        private WASAPIPROC wasProc;
        private BassWasapiHandler _wasapi;
        private float[] fft = new float[2048];
        private float BASS_CUTTOFF = 64; //When to stop sampling the bass
        private float HUE = 0;
        private bool IsClosing = false;
        private Stopwatch stopWatch = new Stopwatch();

        public Main()
        {
            InitializeComponent();
            BackupColorScheme();

            //This is so you don't see the dumb HERPADERP BASS IS STARTING spash screen
            BassNet.Registration("swkauker@yahoo.com", "2X2832371834322");
            Bass.BASS_SetConfig(BASSConfig.BASS_CONFIG_UPDATEPERIOD, 0);

            //Initialize bass with a 'no sound' device
            Bass.BASS_Init(0, 48000, BASSInit.BASS_DEVICE_DEFAULT, IntPtr.Zero);

            BASS_WASAPI_DEVICEINFO[] wasapiDevices = BassWasapi.BASS_WASAPI_GetDeviceInfos();
            int devnum = 1;
            BASS_WASAPI_DEVICEINFO deviceinfo = null;
            for (int i = 0; i < wasapiDevices.Length; i++)
            {
                BASS_WASAPI_DEVICEINFO info = wasapiDevices[i];

                if (!info.IsInput && info.IsDefault)
                {
                    devnum = i + 1;
                    deviceinfo = wasapiDevices[devnum];
                    break;
                }

            }
            labelDevice.Text = deviceinfo.name;
            wasProc = new WASAPIPROC(WasapiCallback);
            Console.WriteLine(BassWasapi.BASS_WASAPI_CheckFormat(devnum, deviceinfo.mixfreq, deviceinfo.mixchans, BASSWASAPIFormat.BASS_WASAPI_FORMAT_32BIT));
            BassWasapi.BASS_WASAPI_Init(devnum, deviceinfo.mixfreq, deviceinfo.mixchans, BASSWASAPIInit.BASS_WASAPI_BUFFER | BASSWASAPIInit.BASS_WASAPI_SHARED, 1, 0, wasProc, IntPtr.Zero);
            BassWasapi.BASS_WASAPI_Start();

            stopWatch.Start();
        }

        private void BackupColorScheme()
        {
            WinAPI.DWM_COLORIZATION_PARAMS Temp;
            //Store their Original color settings, so they don't get angry!
            WinAPI.DwmGetColorizationParameters(out Backup);

            Temp.Color1 = Backup.Color1;
            Temp.Color2 = 0; //seems to be 0 to 100 2147483647
            Temp.Intensity = Backup.Intensity;
            Temp.Unknown1 = Backup.Unknown1;
            Temp.Unknown2 = Backup.Unknown2;
            Temp.Unknown3 = Backup.Unknown3;
            Temp.Opaque = Backup.Opaque;

            WinAPI.DwmSetColorizationParameters(ref Temp, 4);

            // Handle the ApplicationExit event to know when the application is exiting.
            Application.ApplicationExit += new EventHandler(this.OnApplicationExit);
        }

        //perform FFT math
        private int WasapiCallback(IntPtr buffer, int length, IntPtr user)
        {
            if (stopWatch.ElapsedMilliseconds > numUpdateInterval.Value)
            {
                stopWatch.Reset();
                stopWatch.Start();

                BassWasapi.BASS_WASAPI_GetData(fft, (int)BASSData.BASS_DATA_FFT2048);

                int r, g, b = 0;
                float average = 0;

                for (int i = 0; i < BASS_CUTTOFF; i++)
                {
                    average += fft[i];
                }
                average = average / BASS_CUTTOFF;
                average *= (float)numMultiplier.Value;

                Utils.HsvToRgb(HUE, average, 1, out r, out g, out b);

                Utils.SetDwmColor(Color.FromArgb((int)Utils.Clamp(0, 255, (average * 255)), r, g, b));
                Utils.SetDwmAlpha((int)Utils.Clamp(0, 100, (average * 100)));
            }
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
            //WinAPI.DwmSetColorizationParameters(ref Backup, 3);
            Utils.SetDwmColor(System.Drawing.Color.FromArgb((int)Backup.Color1));

            Backup.Color2 = 0;
            WinAPI.DwmSetColorizationParameters(ref Backup, 4);
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

        private void Main_FormClosed(object sender, FormClosedEventArgs e)
        {
            Bass.BASS_Free();
        }

        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.IsClosing = true;
        }
    }
}
