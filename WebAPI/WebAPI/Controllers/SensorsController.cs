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
            _db.Add(data);
            return Created("api/data", data);
        }

        [Route("ok")]
        public IHttpActionResult IsOk()
        {
            if (!_db.Any()) return BadRequest("Database is empty");

            return Ok(_db.Last().Temperature > 20 && _db.Last().Sound < 200 && _db.Last().Light < 200);
        }

        [Route("data")]
        public IHttpActionResult GetLastData()
        {
            if (!_db.Any()) return BadRequest("Database is empty");

            return Ok(_db.Last());
        }

        [Route("dataAll")]
        public IHttpActionResult GetAllData() => Ok(_db);
    }
}