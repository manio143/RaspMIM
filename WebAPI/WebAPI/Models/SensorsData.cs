using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace WebAPI.Models
{
    [DataContract]
    public struct SensorsData
    {
        [DataMember]
        public double Temperature { get; set; }
        [DataMember]
        public int Sound { get; set; }
        [DataMember]
        public int Light { get; set; }
        [DataMember]
        public double Humidity { get; set; }
        [DataMember]
        public DateTime Date { get; set; }

        public SensorsData(double temperature, int sound, int light, double humidity)
        {
            Temperature = temperature;
            Sound = sound;
            Light = light;
            Humidity = humidity;
            Date = DateTime.Today;
        }
    }
}