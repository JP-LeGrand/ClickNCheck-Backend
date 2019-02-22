using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ClickNCheck.Data;
using ClickNCheck.Models;
using ClickNCheck.Services;

namespace ClickNCheck.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CandidatesController : ControllerBase
    {
        private readonly ClickNCheckContext _context;
        CodeGenerator codeGenerator = new CodeGenerator();
        EmailService service = new EmailService();

        public CandidatesController(ClickNCheckContext context)
        {
            _context = context;
        }

        // GET: api/Candidates
        [HttpGet]
        [Route("GetAllCandidates")]
        public async Task<ActionResult<IEnumerable<Candidate>>> GetAllCandidates()
        {
            return await _context.Candidate.ToListAsync();
        }

        // GET: api/Candidates/5
        [HttpGet]
        [Route("GetCandidate/{id}")]
        public async Task<ActionResult<Candidate>> GetCandidate(int id)
        {
            var candidate = await _context.Candidate.FindAsync(id);

            if (candidate == null)
            {
                return NotFound();
            }

            return candidate;
        }

        // PUT: api/Candidates/5
        [HttpPut]
        [Route("UpdateCandidate/{id}")]
        public async Task<IActionResult> PutCandidate(int id, Candidate candidate)
        {
            if (id != candidate.ID)
            {
                return BadRequest();
            }

            _context.Entry(candidate).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CandidateExists(id))
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

        // POST: api/Candidates
        [HttpPost]
        [Route("CreateCandidate")]
        public async Task<ActionResult<Candidate>> CreateCandidate(Candidate candidate)
        {
            candidate.Password = codeGenerator.ReferenceNumber();
            var org = _context.Organisation.FirstOrDefault(o => o.ID == candidate.Organisation.ID);
            var mailBody = service.CandidateMail();
            //reformat email content
            mailBody.Replace("{CandidateName}",candidate.Name);
            mailBody.Replace("{OrganisationName}", org.Name);
            mailBody.Replace("{referenceNumber}", candidate.Password);
            try
            {
                service.SendMail(candidate.Email, "New Verificaiton Request", mailBody);
                _context.Candidate.Add(candidate);
                await _context.SaveChangesAsync();
            }
            catch(Exception e)
            {

            }
            return CreatedAtAction("GetCandidate", new { id = candidate.ID }, candidate);
        }

        [HttpPost]
        [Route("CreateBulkCandidate")]
        public async Task<ActionResult<Candidate>> CreateBulkCandidate([FromBody] List<Candidate> candidates)
        {

            await _context.Candidate.AddRangeAsync(candidates);
            await _context.SaveChangesAsync();

            return Ok();
        }

        // DELETE: api/Candidates/5
        [HttpDelete]
        [Route("DeleteCandidate/{id}")]
        public async Task<ActionResult<Candidate>> DeleteCandidate(int id)
        {
            var candidate = await _context.Candidate.FindAsync(id);
            if (candidate == null)
            {
                return NotFound();
            }

            _context.Candidate.Remove(candidate);
            await _context.SaveChangesAsync();

            return candidate;
        }

        private bool CandidateExists(int id)
        {
            return _context.Candidate.Any(e => e.ID == id);
        }

        // POST: api/Candidates/5/AssignCandidates
        [HttpPost]
        [Route("{id}/AssignCandidates")]
        public async Task<IActionResult> AssignCandidates(int id, [FromBody]int[] ids)
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
                var candidate = await _context.Candidate.FindAsync(ids[i]);

                if (candidate == null)
                {
                    return NotFound("The recruiter " + candidate.Name + candidate.Surname + " does not exist");
                }
                //add recruiter to job profile
                jobProfile.Candidate_JobProfile.Add(new Candidate_JobProfile { JobProfile = jobProfile, Candidate = candidate });

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
