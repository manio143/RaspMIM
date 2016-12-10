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
            for (int i = 0; i < 500; i++)
            {
                SetLedOn(Pin.DigitalPin5);
                Task.Delay(500).Wait();
                SetLedOff(Pin.DigitalPin5);

                System.Diagnostics.Debug.WriteLine(Temperature.Current);
                SetText($"T: {Temperature.Current:0.0}  S: {Sound.Current}\nH: {Temperature.Humidity}   L: {Light.Current}");
                SetBackground(128, 128, 128);

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

        void SetLedOn(Pin p)
        {
            DeviceFactory.Build.Led(p).ChangeState(GrovePi.Sensors.SensorStatus.On);
        }
        void SetLedOff(Pin p)
        {
            DeviceFactory.Build.Led(p).ChangeState(GrovePi.Sensors.SensorStatus.Off);
        }
    }
}
