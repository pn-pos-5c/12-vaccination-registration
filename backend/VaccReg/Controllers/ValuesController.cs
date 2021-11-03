using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using VaccRegDb;

namespace VaccReg.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly VaccRegContext db;
        public ValuesController(VaccRegContext db)
        {
            this.db = db;
        }

        [HttpGet("GetVaccinations")]
        public object GetVaccinations()
        {
            try
            {
                int nr = db.Vaccinations.Count();
                return new { IsOk = true, Nr = nr };
            }
            catch (Exception exc)
            {
                return new { IsOk = false, Nr = -1, Error = exc.Message };
            }
        }

    }
}
