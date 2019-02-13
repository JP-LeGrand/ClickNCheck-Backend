﻿using System;
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
    public class JobProfilesController : ControllerBase
    {
        private readonly ClickNCheckContext _context;

        public JobProfilesController(ClickNCheckContext context)
        {
            _context = context;
        }

        // GET: api/JobProfiles
        [HttpGet]
        public async Task<ActionResult<IEnumerable<JobProfile>>> GetJobProfile()
        {
            return await _context.JobProfile.ToListAsync();
        }

        // GET: api/JobProfiles/5
        [HttpGet("{id}")]
        public async Task<ActionResult<JobProfile>> GetJobProfile(int id)
        {
            var jobProfile = await _context.JobProfile.FindAsync(id);

            if (jobProfile == null)
            {
                return NotFound();
            }

            return jobProfile;
        }

        // PUT: api/JobProfiles/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutJobProfile(int id, JobProfile jobProfile)
        {
            if (id != jobProfile.ID)
            {
                return BadRequest();
            }

            _context.Entry(jobProfile).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!JobProfileExists(id))
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

        // POST: api/JobProfiles
        [HttpPost]
        public async Task<ActionResult<JobProfile>> PostJobProfile(JobProfile jobProfile)
        {
            _context.JobProfile.Add(jobProfile);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetJobProfile", new { id = jobProfile.ID }, jobProfile);
        }

        // POST: api/CreateJobProfile
        [HttpPost]
        [Route("CreateJobProfile/{id}")]
        public async Task<ActionResult<JobProfile>> CreateJobProfile(int id, [FromBody] JobProfile jobProfile)
        {
            // find related organisation
            var org = await _context.Organisation.FindAsync(id);
            jobProfile.Organisation = org;

            // save job profile
            _context.JobProfile.Add(jobProfile);
            await _context.SaveChangesAsync();

            return Ok(jobProfile);
        }

        // DELETE: api/JobProfiles/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<JobProfile>> DeleteJobProfile(int id)
        {
            var jobProfile = await _context.JobProfile.FindAsync(id);
            if (jobProfile == null)
            {
                return NotFound();
            }

            _context.JobProfile.Remove(jobProfile);
            await _context.SaveChangesAsync();

            return jobProfile;
        }

        private bool JobProfileExists(int id)
        {
            return _context.JobProfile.Any(e => e.ID == id);
        }


    }
}
