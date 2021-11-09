using System;
using System.Collections.Generic;
using System.Linq;
using VaccReg.DTOs;
using VaccRegDb;

namespace VaccReg.Services
{
    public class RegistrationsService
    {
        private readonly VaccRegContext db;

        public RegistrationsService(VaccRegContext db)
        {
            this.db = db;
        }

        public List<DateTime> GetTimeslots(DateTime day)
        {
            List<DateTime> result = new();
            DateTime date = day.Date.AddHours(8);

            for (int i = 0; i < 12; i++)
            {
                if (!db.Vaccinations.Any(v => v.VaccinationDate.Equals(date)))
                {
                    result.Add(date);
                }

                date = date.AddMinutes(15);
            }

            return result;
        }

        public Registration ValiateUser(long ssn, int pin)
        {
            var registration = db.Registrations.First(r => r.SocialSecurityNumber == ssn && r.PinCode == pin);

            if (registration != null)
            {
                return registration;
            }

            return null;
        }

        public Vaccination AddRegistration(VaccinationDto vaccination)
        {
            var entity = db.Vaccinations.Add(new Vaccination
            {
                RegistrationId = vaccination.RegistrationId,
                VaccinationDate = vaccination.VaccinationDate
            });
            db.SaveChanges();

            return entity.Entity;
        }
    }
}
