using GrovePi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Background;

namespace IoTApp
{
    class StartupTask : IBackgroundTask
    {
        public void Run(IBackgroundTaskInstance taskInstance)
        {
            DeviceFactory.Build.Led(Pin.DigitalPin2).ChangeState(GrovePi.Sensors.SensorStatus.On);
            Task.Delay(3000).Wait();
            DeviceFactory.Build.Led(Pin.DigitalPin2).ChangeState(GrovePi.Sensors.SensorStatus.Off);
        }
    }
}
