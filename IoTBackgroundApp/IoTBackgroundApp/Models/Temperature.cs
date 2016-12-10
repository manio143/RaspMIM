using GrovePi;
using GrovePi.Sensors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoTBackgroundApp.Models
{
    class Temperature
    {
        static IDHTTemperatureAndHumiditySensor sensor = DeviceFactory.Build
            .DHTTemperatureAndHumiditySensor(Pin.DigitalPin7, DHTModel.Dht11);
        public static double Current
        {
            get
            {
                sensor.Measure();
                return sensor.TemperatureInCelsius;
            }
        }

        public static double Humidity
        {
            get
            {
                sensor.Measure();
                return sensor.Humidity;
            }
        }
    }
}
