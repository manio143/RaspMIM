using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoTBackgroundApp
{
    class Light
    {
        public static int Current
        {
            get
            {
                return GrovePi.DeviceFactory.Build.LightSensor(GrovePi.Pin.DigitalPin3).SensorValue();
            }
        }
    }
}
