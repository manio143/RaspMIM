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
        const string Server = "http://raspmim.azurewebsites.net/";
        private BackgroundTaskDeferral deferral;
        public void Run(IBackgroundTaskInstance taskInstance)
        {
            deferral = taskInstance.GetDeferral();
            SetBackground(128, 128, 128);
            //DeviceFactory.Build.Led(Pin.DigitalPin2).ChangeState(GrovePi.Sensors.SensorStatus.On);
            for (; ;)
            {
                double temp = Temperature.Current;
                double humid = Temperature.Humidity;
                int sound = Sound.Current;
                int light = Light.Current;

                float volts = sound * 5 / 1024;

                if (sound > 110 || humid > 45)
                {
                    SetLedOn(Pin.DigitalPin2);
                    DeviceFactory.Build.Buzzer(Pin.DigitalPin3).ChangeState(GrovePi.Sensors.SensorStatus.On);
                }
                else
                {
                    SetLedOff(Pin.DigitalPin2);
                }

                using (var client = new HttpClient())
                {
                    var content = new FormUrlEncodedContent(new[]
                    {
                        new KeyValuePair<string, string>("temperature", temp.ToString()),
                        new KeyValuePair<string, string>("sound", sound.ToString()),
                        new KeyValuePair<string, string>("light", light.ToString()),
                        new KeyValuePair<string, string>("humidity", humid.ToString())
                    });
                    client.PostAsync(Server + "api/data", content).Wait();
                    var result = client.GetAsync(Server + "api/ok").Result;
                    if (result.IsSuccessStatusCode)
                    {
                        var resultMessage = result.Content.ReadAsStringAsync().Result;
                        if (resultMessage == "false")
                            SetText("Nie sprzyjające warunki");
                        else
                            SetText("Fajnie jest!");
                    }
                    else
                    {
                        SetText("Error occured.");
                    }
                }

                //System.Diagnostics.Debug.WriteLine(sound);
                //SetText($"T: {temp:0.0}  S: {sound}\nH: {humid}    L: {light}");

                    DeviceFactory.Build.Buzzer(Pin.DigitalPin3).ChangeState(GrovePi.Sensors.SensorStatus.Off);
            }
            deferral.Complete();
        }

        string text;
        void SetText(string text)
        {
            if (text != this.text)
            {
                DeviceFactory.Build.RgbLcdDisplay().SetText(text);
                this.text = text;
            }
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
