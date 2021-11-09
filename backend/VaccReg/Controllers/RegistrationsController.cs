using Microsoft.AspNetCore.Mvc;
using System;
using VaccReg.DTOs;
using VaccReg.Services;

namespace VaccReg.Controllers
{
    [Route("api/Registrations")]
    [ApiController]
    public class RegistrationsController : ControllerBase
    {
        private readonly RegistrationsService registrationsService;

        public RegistrationsController(RegistrationsService registrationsService)
        {
            this.registrationsService = registrationsService;
        }

        [HttpGet]
        [Route("timeslots")]
        public IActionResult GetTimeslots(DateTime date)
        {
            return Ok(registrationsService.GetTimeslots(date));
        }

        [HttpGet]
        [Route("validate")]
        public IActionResult ValidateUser(long ssn, int pin)
        {
            return Ok(registrationsService.ValiateUser(ssn, pin));
        }

        [HttpPost]
        [Route("/api/Vaccinations")]
        public IActionResult AddVaccination([FromBody] VaccinationDto vaccination)
        {
            return Ok(registrationsService.AddRegistration(vaccination));
        }
    }
}
