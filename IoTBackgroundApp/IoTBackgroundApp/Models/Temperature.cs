using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoTBackgroundApp.Models
{
    class Temperature
    {
        public static double Current
        {
            get
            {
                return GrovePi.DeviceFactory.Build
                    .TemperatureAndHumiditySensor(GrovePi.Pin.DigitalPin7, GrovePi.Sensors.Model.Dht11)
                    .TemperatureInCelsius();
            }
        }
    }
}
