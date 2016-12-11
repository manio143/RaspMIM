using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoTBackgroundApp.Models
{
    public sealed class SensorsData
    {
        public double Temperature { get; set; }
        public int Sound { get; set; }
        public int Light { get; set; }
        public double Humidity { get; set; }

        public SensorsData(double temperature, int sound, int light, double humidity)
        {
            Temperature = temperature;
            Sound = sound;
            Light = light;
            Humidity = humidity;
        }
    }
}
