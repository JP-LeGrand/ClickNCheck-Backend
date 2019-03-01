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
    public class VendorsController : ControllerBase
    {
        private readonly ClickNCheckContext _context;

        public VendorsController(ClickNCheckContext context)
        {
            _context = context;
        }


        // GET: api/Vendors
        [HttpGet]
        [Route("GetAllVendors")]
        public async Task<ActionResult<IEnumerable<object>>> GetAllVendors()
        {
            var services = await (from s in _context.Services
                            select new
                            {
                                ID = s.ID,
                                Name = s.Name,
                                TurnaroundTime = s.TurnaroundTime,
                                CheckCategoryID = s.CheckCategoryID,
                            }
                               ).ToListAsync();

            var checkCategories = await _context.CheckCategory.ToListAsync();

            JObject json = new JObject
            {
                ["services"] = JToken.FromObject(services),
                ["checkCategories"] = JToken.FromObject(checkCategories)
            };

            return Ok(json);
        }

        // GET: api/Vendors
        [HttpGet]
        [Route("GetCheckCategories")]
        public async Task<ActionResult<IEnumerable<object>>> GetCheckCategories()
        {

            var categories = await _context.CheckCategory.ToListAsync();


            return Ok(categories);
        }

        // GET: api/Vendors/5
        [HttpGet]
        [Route("GetVendor/{id}")]
        public async Task<ActionResult<Vendor>> GetVendor(int id)
        {
            var vendor = await _context.Checks.FindAsync(id);

            if (vendor == null)
            {
                return NotFound();
            }

            return vendor;
        }

        // PUT: api/Vendors/5
        [HttpPut]
        [Route("UpdateVendor/{id}")]
        public async Task<IActionResult> UpdateVendor(int id, Vendor vendor)
        {
            if (id != vendor.ID)
            {
                return BadRequest();
            }

            _context.Entry(vendor).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VendorExists(id))
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

        // POST: api/Vendors/CreateVendor
        [HttpPost]
        [Route("CreateVendor")]
        public async Task<ActionResult<Vendor>> PostVendor(Vendor vendor)
        {
            _context.Checks.Add(vendor);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetVendor", new { id = vendor.ID }, vendor);
        }

        //[HttpGet]
        //[Route("CreateVendorTest")]
        //public async Task<ActionResult<Vendor>> CreateVendorTest()
        //{
        //    Vendor vendor = new Vendor();
        //    vendor.Name = "Lexis Nexis";

        //    var services = await _context.Services.FindAsync(1);
        //    var categories = await _context.CheckCategory.ToListAsync();

        //    vendor.Services.Add(services);


        //    for (int i = 0; i < categories.Capacity; i++)
        //    {
        //        vendor.Vendor_Category.Add(new Vendor_Category { Vendor = vendor, CheckCategory = categories[i] });
        //    }

        //    _context.Checks.Add(vendor);
        //    await _context.SaveChangesAsync();

        //    return Ok(vendor);
        //}


        // DELETE: api/Vendors/5
        [HttpDelete]
        [Route("DeleteVendor/{id}")]
        public async Task<ActionResult<Vendor>> DeleteVendor(int id)
        {
            var vendor = await _context.Checks.FindAsync(id);
            if (vendor == null)
            {
                return NotFound();
            }

            _context.Checks.Remove(vendor);
            await _context.SaveChangesAsync();

            return vendor;
        }

        private bool VendorExists(int id)
        {
            return _context.Checks.Any(e => e.ID == id);
        }
    }
}
