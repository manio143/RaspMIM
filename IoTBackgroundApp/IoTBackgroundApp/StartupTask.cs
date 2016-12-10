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
            SetBackground(128, 128, 128);
            //DeviceFactory.Build.Led(Pin.DigitalPin2).ChangeState(GrovePi.Sensors.SensorStatus.On);
            for (int i = 0; i < 500; i++)
            {
                double temp = Temperature.Current;
                double humid = Temperature.Humidity;
                int sound = Sound.Current;
                int light = Light.Current;

                if (temp > 25 || humid > 45)
                    SetLedOn(Pin.DigitalPin2);
                else
                    SetLedOff(Pin.DigitalPin2);

                //System.Diagnostics.Debug.WriteLine(Temperature.Current);
                SetText($"T: {temp:0.0}  S: {sound}\nH: {humid}    L: {light}");

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
