using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    public class SensorsController : ApiController
    {
        private readonly List<SensorsData> _db = new List<SensorsData>();

        public void PostData(SensorsData data) => _db.Add(data);

        public IHttpActionResult IsOk()
        {
            if (!_db.Any()) return BadRequest("Database is empty");

            return Ok(_db.Last().Temperature > 20 && _db.Last().Sound < 200 && _db.Last().Light < 200);
        }
    }
}