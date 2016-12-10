using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAPI.Models
{
    public class SensorsData
    {
        public double Temperature { get; set; }
        public int Sound { get; set; }
        public int Light { get; set; }

        public SensorsData(double temperature, int sound, int light)
        {
            Temperature = temperature;
            Sound = sound;
            Light = light;
        }
    }
}