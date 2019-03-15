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
using ClickNCheck.Services;

namespace ClickNCheck.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VerificationChecksController : ControllerBase
    {
        private readonly ClickNCheckContext _context;

        public VerificationChecksController(ClickNCheckContext context)
        {
            _context = context;
        }

        // GET: api/VerificationChecks
        [HttpGet]
        public async Task<ActionResult<IEnumerable<VerificationCheck>>> GetVerificationCheck()
        {
            return await _context.VerificationCheck.ToListAsync();
        }

        // GET: api/VerificationChecks/5
        [HttpGet("{id}")]
        public async Task<ActionResult<VerificationCheck>> GetVerificationCheck(int id)
        {
            var verificationCheck = await _context.VerificationCheck.FindAsync(id);

            if (verificationCheck == null)
            {
                return NotFound();
            }

            return verificationCheck;
        }
        [HttpGet("services/{id}")]
        public async Task<object> GetVerificationCheckServices(int id)
        {
            var verChecks =  _context.Candidate_Verification_Check.Find(id);
            
            var checks = new List<string>();
            var _lst = (from i in _context.VerificationCheck
                        join j in _context.Candidate_Verification on i.ID equals j.VerificationCheckID
                        join k in _context.Candidate_Verification_Check on j.ID equals k.Candidate_VerificationID
                        //group i by i.Order
                        select new
                        {
                            k.Services
                        }).ToList<object>().Distinct();
            
       
            return _lst;
        }

        // PUT: api/VerificationChecks/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVerificationCheck(int id, VerificationCheck verificationCheck)
        {
            if (id != verificationCheck.ID)
            {
                return BadRequest();
            }

            _context.Entry(verificationCheck).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VerificationCheckExists(id))
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

        // POST: api/VerificationChecks
        [HttpPost]
        [Route("CreateVerificationCheck/{jobprofileid}")]
        public async Task<ActionResult<VerificationCheck>> CreateVerificationCheck(int jobprofileid, [FromBody]JObject jObject)
        {
            //create VC first
            JobProfile jobProfile = null;
            try
            {
                jobProfile = _context.JobProfile.Find(jobprofileid);
            }
            catch { return NotFound("Job profile not found"); }

            var jpChecks = _context.JobProfile_Check.Where(x => x.JobProfileID == jobprofileid).ToArray();

            bool IsAuthorized = jobProfile.authorisationRequired ? false: true;

            JArray array = (JArray)jObject["checks"];
            int[] checks = array.Select(jv => (int)jv).ToArray();
            
            VerificationCheck vc = new VerificationCheck
            {
                Title = jobProfile.Title,
                JobProfileID = jobprofileid,
                IsAuthorize = IsAuthorized,
                RecruiterID = (int)jObject["recruiterID"],
                IsComplete = (bool)jObject["IsComplete"]

            };

            for (int i = 0; i < checks.Length; i++)
            {
                var services = await _context.Services.FindAsync(checks[i]);

                if (services == null)
                {
                    continue;
                }
                //add verificationCheckChecks table to VerificationCheck to link to services
                vc.VerificationCheckChecks.Add(new VerificationCheckChecks { VerificationCheck = vc, Services = services, Order = i + 1 });
            }
            _context.VerificationCheck.Add(vc);
            await _context.SaveChangesAsync();

            VerificationCheckAuth auth = new VerificationCheckAuth();
            //run authorization check
            if (vc.IsAuthorize == false)
            {
                if (!(jpChecks.Length == checks.Length))
                {
                    auth.changedChecks(_context, vc.RecruiterID, vc.ID);
                }
                else
                {
                    for (int m = 0; m <= jpChecks.Length; m++)
                    {
                        if (jpChecks[m].ServicesID == (int)array[m])
                        {
                            continue;
                        }
                        else
                        {
                            auth.changedChecks(_context, vc.RecruiterID, vc.ID);
                            break;
                        }
                    }
                }
            }
            //Add VC Checks
            return Ok(vc.ID);
        }

        // DELETE: api/VerificationChecks/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<VerificationCheck>> DeleteVerificationCheck(int id)
        {
            var verificationCheck = await _context.VerificationCheck.FindAsync(id);
            if (verificationCheck == null)
            {
                return NotFound();
            }

            _context.VerificationCheck.Remove(verificationCheck);
            await _context.SaveChangesAsync();

            return verificationCheck;
        }

        private bool VerificationCheckExists(int id)
        {
            return _context.VerificationCheck.Any(e => e.ID == id);
        }
    }
}
