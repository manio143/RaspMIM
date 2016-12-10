using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace WebAPI.Controllers
{
    public class SensorsController : ApiController
    {
        private double temperature;
        private int sound;
        private int light;

        public void PostTemperature(double value)
        {
            temperature = value;
        }

        public void PostSound(int value)
        {
            sound = value;
        }

        public void PostLight(int value)
        {
            light = value;
        }

        public IHttpActionResult IsOk()
        {
            return Ok(temperature > 20 && sound < 200 && light < 200);
        }
    }
}