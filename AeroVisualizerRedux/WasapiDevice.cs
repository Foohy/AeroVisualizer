﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Un4seen.Bass;
using Un4seen.BassWasapi;

namespace AeroVisualizerRedux
{
    class WasapiDevice
    {
        public static bool BassInit { get; private set; }
        public static BASS_WASAPI_DEVICEINFO CurrentDeviceInfo { get; private set; }
        public static int CurrentDevice { get; private set; }
        public delegate int FFTThink(IntPtr buffer, int length, IntPtr user);

        private static WASAPIPROC wasProc;
        private static FFTThink wasapiThink;

        public static void Start()
        {
            BassWasapi.BASS_WASAPI_SetDevice(CurrentDevice);
            BassWasapi.BASS_WASAPI_Start();
        }

        public static void Stop()
        {

            BassWasapi.BASS_WASAPI_SetDevice(CurrentDevice);
            BassWasapi.BASS_WASAPI_Stop(false);
        }

        public static void SetDelegate(FFTThink think)
        {
            wasapiThink = think;
        }

        public static void SetDevice(int device)
        {
            //Initialize bass.net if we need to
            if (!BassInit)
            {
                //Initialize bass with a 'no sound' device
                Bass.BASS_SetConfig(BASSConfig.BASS_CONFIG_UPDATEPERIOD, 0);
                BassInit = Bass.BASS_Init(0, 48000, BASSInit.BASS_DEVICE_DEFAULT, IntPtr.Zero);
            }

            //Get some info about their selected device
            var deviceinfo = BassWasapi.BASS_WASAPI_GetDeviceInfo(device);

            //Store it up
            CurrentDevice = device;
            CurrentDeviceInfo = deviceinfo;

            //Set the device so subsequent calls are on it
            BassWasapi.BASS_WASAPI_SetDevice(CurrentDevice);

            wasProc = new WASAPIPROC(WasapiCallback);

            BassWasapi.BASS_WASAPI_Init(device, deviceinfo.mixfreq, deviceinfo.mixchans, BASSWASAPIInit.BASS_WASAPI_BUFFER | BASSWASAPIInit.BASS_WASAPI_SHARED, 1, 0, wasProc, IntPtr.Zero);
        }

        private static int WasapiCallback(IntPtr buffer, int length, IntPtr user)
        {
            if (wasapiThink != null)
                return wasapiThink(buffer, length, user);

            return length;
        }

        public static int RetrieveDefaultDevice()
        {
            BASS_WASAPI_DEVICEINFO[] wasapiDevices = BassWasapi.BASS_WASAPI_GetDeviceInfos();
            int devnum = 1;
            for (int i = 0; i < wasapiDevices.Length; i++)
            {
                BASS_WASAPI_DEVICEINFO info = wasapiDevices[i];

                if (!info.IsInput && info.IsDefault)
                {
                    devnum = i + 1;
                    break;
                }
            }

            return devnum;
        }
    }
}
