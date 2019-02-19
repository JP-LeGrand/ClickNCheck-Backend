using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using ClickNCheck.Data;
using ClickNCheck.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;

namespace ClickNCheck.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AvailableController : ControllerBase
    {
        private ClickNCheckContext _context;

        public AvailableController(ClickNCheckContext context)
        {
            _context = context;
        }

        // GET api/available
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Vendor>>> GetVendor()
        {
            return await _context.Vendor.ToListAsync();
        }

        // GET: api/available/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Vendor>> GetVendor(int id)
        {
            var Vendor = await _context.Vendor.FindAsync(id);

            if (Vendor == null)
            {
                return NotFound();
            }

            return Vendor;
        }
        // POST: api/available
        [HttpPost]
        [Route("addVendor")]
        public async Task<ActionResult<Vendor>> PostUser(Vendor Vendor)
        {
            _context.Vendor.Add(Vendor);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetVendor", new { id = Vendor.ID }, Vendor);
        }

        // PUT api/available/{id}
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
            //Add new service for vendor with id = {id}
        }

        // DELETE api/available/{id}
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
