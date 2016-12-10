using GrovePi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Http;
using Windows.ApplicationModel.Background;
using System.Threading.Tasks;

namespace IoTBackgroundApp
{
    public sealed class StartupTask : IBackgroundTask
    {
        private BackgroundTaskDeferral deferral;
        public void Run(IBackgroundTaskInstance taskInstance)
        {
            deferral = taskInstance.GetDeferral();
            DeviceFactory.Build.Led(Pin.DigitalPin2).ChangeState(GrovePi.Sensors.SensorStatus.On);
            Task.Delay(3000).Wait();
            DeviceFactory.Build.Led(Pin.DigitalPin2).ChangeState(GrovePi.Sensors.SensorStatus.Off);

            DeviceFactory.Build.RgbLcdDisplay().SetText("Hello World!");
            DeviceFactory.Build.RgbLcdDisplay().SetBacklightRgb(128, 128, 128);
            deferral.Complete();
        }
    }
}
