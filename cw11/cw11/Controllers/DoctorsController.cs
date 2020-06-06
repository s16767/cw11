using cw11.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cw11.Controllers
{
    [ApiController]
    [Route("/api/doctors")]
    public class DoctorsController : ControllerBase
    {
        private readonly HospitalContext _context;

        public DoctorsController(HospitalContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Doctor>>> GetDoctors()
        {
            return await _context.Doctors.ToListAsync();
        }

        [HttpPut]
        public async Task<ActionResult<Doctor>> AddDoctor(Doctor doctor)
        {
            await _context.Doctors.AddAsync(doctor);
            await _context.SaveChangesAsync();

            return doctor;
        }

        [HttpPost]
        public async Task<ActionResult<Doctor>> UpdateDoctor(Doctor doctor)
        {
            var d = await _context.Doctors.SingleOrDefaultAsync(d => d.IdDoctor == doctor.IdDoctor);
            if (d != null)
            {
                _context.Entry(d).CurrentValues.SetValues(doctor);
                await _context.SaveChangesAsync();
                return Ok(d);
            } else
            {
                return BadRequest();
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Doctor>> DeleteDoctor(int id)
        {
            var doctor = await _context.Doctors.FindAsync(id);
            if (doctor == null)
            {
                return NotFound();
            }

            _context.Doctors.Remove(doctor);
            await _context.SaveChangesAsync();

            return doctor;
        }
    }
}
