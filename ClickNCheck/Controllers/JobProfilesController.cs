using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ClickNCheck.Data;
using ClickNCheck.Models;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

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

        // GET: api/JobProfiles/GetAllJobProfiles
        [HttpGet]
        [Route("GetAllJobProfiles")]
        public async Task<ActionResult<IEnumerable<JobProfile>>> GetAllJobProfiles()
        {
            return await _context.JobProfile.ToListAsync();
        }

        // GET: api/JobProfiles/5
        [HttpGet]
        [Route("GetJobProfile/{id}")]
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
        [HttpPut]
        [Route("UpdateJobProfile/{id}")]
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


        // POST: api/CreateJobProfile
        [HttpPost]
        [Route("CreateJobProfile/{id}")]
        public async Task<ActionResult<JobProfile>> CreateJobProfile(int id, [FromBody] string jobProfile)
        {
            //convert result to JSON object
            JObject json = JObject.Parse(jobProfile);
            // convert JSON to JobProfile object
            JobProfile j = new JobProfile
            {
                Title = json["title"].ToString(),
                JobCode = json["code"].ToString(),
                isCompleted = (bool)json["isCompleted"],
                
            };
            string checksString = json["checks"].ToString();
            int[] checks = Array.ConvertAll(checksString.Split(','), int.Parse);
            // find related organisation
            var org = await _context.Organisation.FindAsync(id);
            j.Organisation = org;

            //find vendors
            for (int i = 0; i < checks.Length; i++)
            {
                var vendor = await _context.Vendor.FindAsync(checks[i]);

                if (vendor == null)
                {
                    return NotFound("The vendor " + vendor.Name + " does not exist");
                }
                //add vendor to job profile
                j.JobProfile_Vendor.Add(new JobProfile_Vendor { JobProfile = j, Vendor = vendor, Order = i+1 });
            }
            // save job profile
            _context.JobProfile.Add(j);

            await _context.SaveChangesAsync();

            return Ok(j);
        }

        // DELETE: api/JobProfiles/5
        [HttpDelete]
        [Route("DeleteJobProfile/{id}")]
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

        // POST: api/JobProfiles/5/AssignRecruiters
        [HttpPost]
        [Route("{id}/AssignRecruiters")]
        public async Task<IActionResult> AssignRecruiters(int id, [FromBody]int[] ids)
        {
            int jobId = id;

            //find job profile
            var jobProfile = await _context.JobProfile.FindAsync(jobId);

            if (jobProfile == null)
            {
                return NotFound("This Job Profile does not exist");
            }

            //find recruiters
            for (int i = 0; i < ids.Length; i++)
            {
                var recruiter = await _context.User.FindAsync(ids[i]);

                if (recruiter == null)
                {
                    return NotFound("The recruiter " + recruiter.Name + recruiter.Surname + " does not exist");
                }
                //add recruiter to job profile
                jobProfile.Recruiter_JobProfile.Add(new Recruiter_JobProfile { JobProfile = jobProfile, Recruiter = recruiter });

            }
            //save changes to job profile
            _context.Entry(jobProfile).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                var exists = _context.User.Any(e => e.ID == jobId);
                if (!JobProfileExists(jobId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(jobProfile);
        }

        private bool JobProfileExists(int id)
        {
            return _context.JobProfile.Any(e => e.ID == id);
        }


    }
}
