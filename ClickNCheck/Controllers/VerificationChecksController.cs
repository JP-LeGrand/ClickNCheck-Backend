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
        [Route("CreateVerificationCheck")]
        public async Task<ActionResult<VerificationCheck>> CreateVerificationCheck(VerificationCheck verificationCheck)
        {
            _context.VerificationCheck.Add(verificationCheck);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetVerificationCheck", new { id = verificationCheck.ID }, verificationCheck);
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
