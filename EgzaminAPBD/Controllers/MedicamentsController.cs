using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        //private readonly MedicineDbContext _context;
        //public MedicamentsController(MedicineDbContext context)
        //{
        //    _context = context;
        //}

        [HttpGet("{IdMedicament}")]
        public IActionResult GetMedicament(int idMedicament)
        {
            return Ok(idMedicament);
        }

    }
}
