using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoTBackgroundApp.Models
{
    class Sound
    {
        public static int Current
        {
            get
            {
                return GrovePi.DeviceFactory.Build.SoundSensor(GrovePi.Pin.DigitalPin8).SensorValue();
            }
        }
    }
}
