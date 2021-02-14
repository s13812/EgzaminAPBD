using EgzaminAPBD.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EgzaminAPBD.Controllers
{
    [Route("api/patients")]
    [ApiController]
    public class PatientsController : ControllerBase
    {
        private readonly MedicineDbContext _dbContext;
        public PatientsController (MedicineDbContext context)
        {
            _dbContext = context;
        }

        [HttpDelete("{idPatient}")]
        public IActionResult RemovePatient(int idPatient)
        {
            var pat = _dbContext.Patients.FirstOrDefault(p => p.IdPatient == idPatient);

            if (pat == null)
            {
                return NotFound("Nie ma takiego pacjęta");
            }

            var pres = _dbContext.Prescriptions.Where(p => p.IdPatient == idPatient).Include(p => p.PrescriptionMedicaments);

            var presmed = new List<PrescriptionMedicament>();

            foreach (var p in pres)
            {
                _dbContext.PrescriptionMedicaments.RemoveRange(p.PrescriptionMedicaments);
            }

            _dbContext.Prescriptions.RemoveRange(pres);

            _dbContext.Patients.Remove(pat);

            _dbContext.SaveChanges();

            return Ok ("Usunięto pacjęta");
        }

    }
}
