using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EgzaminAPBD.Models
{
    [Route("api/medicaments")]
    [ApiController]
    public class MedicamentsController : ControllerBase
    {
        private readonly MedicineDbContext _dbContext;
        public MedicamentsController(MedicineDbContext context)
        {
            _dbContext = context;
        }

        [HttpGet("{idMedicament}")]
        public IActionResult GetMedicament(int idMedicament)
        {
            var med = _dbContext.Medicaments.Where(m => m.IdMedicament == idMedicament).Include(m => m.PrescriptionMedicaments).FirstOrDefault();

            if (med != null)
            {
                var pres = new List<Prescription>();

                foreach (var pm in med.PrescriptionMedicaments)
                {
                    pres.Add(_dbContext.Prescriptions.First(p => p.IdPrescription == pm.IdPrescription));
                }
                return Ok(new
                {
                    Lek = new
                    {
                        med.IdMedicament,
                        med.Name,
                        med.Description,
                        med.Type
                    },
                    Recepty = pres.OrderByDescending(p => p.Date).Select(p => new
                    {
                        p.IdPrescription,
                        p.Date,
                        p.DueDate,
                        p.IdPatient,
                        p.IdDoctor                        
                    })
                }) ;
            }

            return NotFound("Nie ma takiego leku");
        }

    }
}
