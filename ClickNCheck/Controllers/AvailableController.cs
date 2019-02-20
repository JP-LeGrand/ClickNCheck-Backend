using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using ClickNCheck.Data;
using ClickNCheck.Models;
using ClickNCheck.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
//using System.Xml;

namespace ClickNCheck.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AvailableController : ControllerBase
    {
        private ClickNCheckContext _context;
        private ConnectToAPI obje;

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
        public async Task<ActionResult<Vendor>> addVendor([FromBody] JObject input)
        {
            //Console.WriteLine("The available:");
            JToken checkCategories = input.SelectToken("categories");
            List<int> theCheckCategories = new List<int>();

            foreach ( var item in checkCategories)
            {
                theCheckCategories.Add((int)item);
            }

            Vendor v = new Vendor
            {
                Name = input.SelectToken("vendorName").ToString()
            };

            //find recruiters
            for (int i = 0; i < theCheckCategories.Count; i++)
            {
                var checkCategory = await _context.CheckCategory.FindAsync(theCheckCategories[i]);

                if (checkCategory == null)
                {
                    return NotFound("The check category " + checkCategory.Category + " does not exist");
                }
                //add recruiter to job profile
                v.Vendor_Category.Add(new Vendor_Category { Vendor = v, CheckCategory = checkCategory });
            }

            _context.Vendor.Add(v);
            await _context.SaveChangesAsync();

            return await _context.Vendor.LastAsync();

        }

        // POST: api/available
        [HttpPost]
        [Route("sendRequest")]
        public HttpWebResponse sendRequest([FromBody]Object obj)
        {
            int type = obj.ToString().ToLower().Contains("xml")? 0 : 1; // get from the form
            string message = type == 1 ? ((JObject) obj).ToString() : obj.ToString(); // get from the form
            string url = obj.ToString(); // get from the form

            obje = new ConnectToAPI(type, url, message);
            
            return obje.run();
        }

        // DELETE api/available/{id}
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
