using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ClickNCheck.Data;
using ClickNCheck.Models;

namespace ClickNCheck.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecruitersController : ControllerBase
    {
        private readonly ClickNCheckContext _context;

        public RecruitersController(ClickNCheckContext context)
        {
            _context = context;
        }

        // GET: api/Recruiters
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Recruiter>>> GetRecruiter()
        {
            return await _context.Recruiter.ToListAsync();
        }

        // GET: api/Recruiters/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Recruiter>> GetRecruiter(int id)
        {
            var recruiter = await _context.Recruiter.FindAsync(id);
            

            if (recruiter == null)
            {
                return NotFound();
            }

            return recruiter;
        }

        // GET: api/Recruiters/GetAllRecruiters/5
        [HttpGet("GetAllRecruiters/{id}")]
        public IEnumerable<Recruiter> GetAllRecruiters(int id)
        {
            var recruiters = _context.Recruiter.Where(r => r.Organisation.ID == id);

            return recruiters;
        }

        // PUT: api/Recruiters/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRecruiter(int id, Recruiter recruiter)
        {
            if (id != recruiter.ID)
            {
                return BadRequest();
            }

            _context.Entry(recruiter).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RecruiterExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Recruiters
        [HttpPost]
        public async Task<ActionResult<Recruiter>> PostRecruiter(Recruiter recruiter)
        {
            _context.Recruiter.Add(recruiter);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRecruiter", new { id = recruiter.ID }, recruiter);
        }

        // DELETE: api/Recruiters/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Recruiter>> DeleteRecruiter(int id)
        {
            var recruiter = await _context.Recruiter.FindAsync(id);
            if (recruiter == null)
            {
                return NotFound();
            }

            _context.Recruiter.Remove(recruiter);
            await _context.SaveChangesAsync();

            return recruiter;
        }

        private bool RecruiterExists(int id)
        {
            return _context.Recruiter.Any(e => e.ID == id);
        }
    }
}
