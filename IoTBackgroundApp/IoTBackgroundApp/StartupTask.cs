using GrovePi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Http;
using Windows.ApplicationModel.Background;
using System.Threading.Tasks;
using IoTBackgroundApp.Models;

namespace IoTBackgroundApp
{
    public sealed class StartupTask : IBackgroundTask
    {
        private BackgroundTaskDeferral deferral;
        public void Run(IBackgroundTaskInstance taskInstance)
        {
            deferral = taskInstance.GetDeferral();
            for(int i = 0; i < 500; i++)
            {
                //SetLedOn(1);
                Task.Delay(500).Wait();

                SetText($"Temp: {Temperature.Current:0.000}\nS: {Sound.Current},L: {Light.Current}");
                SetBackground(128, 128, 128);

                //SetLedOff(1);
            }
            deferral.Complete();
        }

        void SetText(string text)
        {
            DeviceFactory.Build.RgbLcdDisplay().SetText(text);
        }

        void SetBackground(byte red, byte green, byte blue)
        {
            DeviceFactory.Build.RgbLcdDisplay().SetBacklightRgb(red, green, blue);
        }

        void SetLedOn(byte num)
        {
            switch(num)
            {
                case 1:
                    DeviceFactory.Build.Led(Pin.DigitalPin3).ChangeState(GrovePi.Sensors.SensorStatus.On);
                    break;
                case 2:
                    DeviceFactory.Build.Led(Pin.DigitalPin4).ChangeState(GrovePi.Sensors.SensorStatus.On);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        void SetLedOff(byte num)
        {
            switch (num)
            {
                case 1:
                    DeviceFactory.Build.Led(Pin.DigitalPin3).ChangeState(GrovePi.Sensors.SensorStatus.Off);
                    break;
                case 2:
                    DeviceFactory.Build.Led(Pin.DigitalPin4).ChangeState(GrovePi.Sensors.SensorStatus.Off);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
