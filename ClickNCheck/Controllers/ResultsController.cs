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
using ClickNCheck.Services.ResponseService;

namespace ClickNCheck.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResultsController : ControllerBase
    {
        private readonly ClickNCheckContext _context;

        public ResultsController(ClickNCheckContext context)
        {
            _context = context;
        }

        // GET: api/Results
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Results>>> GetResult()
        {
            return await _context.Result.ToListAsync();
        }

        // GET: api/Results/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Results>> GetResults(int id)
        {
            var results = await _context.Result.FindAsync(id);

            if (results == null)
            {
                return NotFound();
            }

            return results;
        }

        // PUT: api/Results/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutResults(int id, Results results)
        {
            if (id != results.ID)
            {
                return BadRequest();
            }

            _context.Entry(results).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ResultsExists(id))
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

        // POST: api/Results
        [HttpPost]
        public async Task<ActionResult<Results>> PostResults(Results results)
        {
            _context.Result.Add(results);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetResults", new { id = results.ID }, results);
        }

        // DELETE: api/Results/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Results>> DeleteResults(int id)
        {
            var results = await _context.Result.FindAsync(id);
            if (results == null)
            {
                return NotFound();
            }

            _context.Result.Remove(results);
            await _context.SaveChangesAsync();

            return results;
        }

        [HttpPost]
        [Route("LongRunningEndPoint")]
        public void LongRunningEndPoint([FromBody] JObject result)
        {
            IResponseService response = new LongRunningEndPointResponseService();
            response.Process(result);
        }

        private bool ResultsExists(int id)
        {
            return _context.Result.Any(e => e.ID == id);
        }
    }
}
