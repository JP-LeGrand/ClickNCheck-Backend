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
    public class JobProfile_ChecksController : ControllerBase
    {
        private readonly ClickNCheckContext _context;

        public JobProfile_ChecksController(ClickNCheckContext context)
        {
            _context = context;
        }

        // GET: api/JobProfile_Checks
        [HttpGet]
        [Route("GetJobProfile_Checks")]
        public async Task<ActionResult<IEnumerable<JobProfile_Checks>>> GetJobProfile_Checks()
        {
            return await _context.JobProfile_Check.ToListAsync();
        }

        // GET: api/JobProfile_Checks/5
        [HttpGet]
        [Route("GetJobProfile_Check/{id}")]
        public async Task<ActionResult<JobProfile_Checks>> GetJobProfile_Checks(int id)
        {
            var jobProfile_Checks = await _context.JobProfile_Check.FindAsync(id);

            if (jobProfile_Checks == null)
            {
                return NotFound();
            }

            return jobProfile_Checks;
        }

        // PUT: api/JobProfile_Checks/5
        [HttpPut]
        [Route("UpdateJobProfile_Check/{id}")]
        public async Task<IActionResult> PutJobProfile_Checks(int id, JobProfile_Checks jobProfile_Checks)
        {
            if (id != jobProfile_Checks.ServicesID)
            {
                return BadRequest();
            }

            _context.Entry(jobProfile_Checks).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!JobProfile_ChecksExists(id))
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

        // POST: api/JobProfile_Checks
        [HttpPost]
        [Route("CreateJobProfile_Check")]
        public async Task<ActionResult<JobProfile_Checks>> PostJobProfile_Checks(JobProfile_Checks jobProfile_Checks)
        {
            _context.JobProfile_Check.Add(jobProfile_Checks);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (JobProfile_ChecksExists(jobProfile_Checks.ServicesID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetJobProfile_Checks", new { id = jobProfile_Checks.ServicesID }, jobProfile_Checks);
        }

        // DELETE: api/JobProfile_Checks/5
        [HttpDelete]
        [Route("DeleteJobProfile_Check/{id}")]
        public async Task<ActionResult<JobProfile_Checks>> DeleteJobProfile_Checks(int id)
        {
            var jobProfile_Checks = await _context.JobProfile_Check.FindAsync(id);
            if (jobProfile_Checks == null)
            {
                return NotFound();
            }

            _context.JobProfile_Check.Remove(jobProfile_Checks);
            await _context.SaveChangesAsync();

            return jobProfile_Checks;
        }

        private bool JobProfile_ChecksExists(int id)
        {
            return _context.JobProfile_Check.Any(e => e.ServicesID == id);
        }
    }
}
