using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [RoutePrefix("api")]
    public class SensorsController : ApiController
    {
        private static readonly List<SensorsData> _db = new List<SensorsData>();

        [Route("data")]
        public IHttpActionResult PostData(SensorsData data)
        {
            data.Date = DateTime.Today;
            _db.Add(data);
            return Created("api/data", data);
        }

        [Route("ok")]
        [HttpGet]
        public IHttpActionResult IsOk()
        {
            if (!_db.Any()) return BadRequest("Database is empty");
            var latest = _db.Last();
            return Ok(latest.Temperature > 20 && latest.Humidity < 45);
        }

        [Route("data")]
        public IHttpActionResult GetLastData()
        {
            if (!_db.Any()) return BadRequest("Database is empty");

            return Ok(_db.Last());
        }

        [Route("dataAll")]
        public IHttpActionResult GetAllData() => Ok(_db);

        [Route("dataAll/{begin},{end}")]
        public IHttpActionResult GetAllData(DateTime begin, DateTime end)
        {
            if (!_db.Any()) return BadRequest("Database is empty");

            return Ok(
                from data in _db
                where data.Date >= begin && data.Date <= end
                select data);
        }
    }
}