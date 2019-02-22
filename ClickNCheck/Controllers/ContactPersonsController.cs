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
    public class ContactPersonsController : ControllerBase
    {
        private readonly ClickNCheckContext _context;

        public ContactPersonsController(ClickNCheckContext context)
        {
            _context = context;
        }

        // GET: api/ContactPersons
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ContactPerson>>> GetContactPerson()
        {
            return await _context.ContactPerson.ToListAsync();
        }

        // GET: api/ContactPersons/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ContactPerson>> GetContactPerson(int id)
        {
            var contactPerson = await _context.ContactPerson.FindAsync(id);

            if (contactPerson == null)
            {
                return NotFound();
            }

            return contactPerson;
        }

        // PUT: api/ContactPersons/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutContactPerson(int id, ContactPerson contactPerson)
        {
            if (id != contactPerson.ID)
            {
                return BadRequest();
            }

            _context.Entry(contactPerson).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ContactPersonExists(id))
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

        // POST: api/ContactPersons
        [HttpPost]
        public async Task<ActionResult<ContactPerson>> PostContactPerson(ContactPerson contactPerson)
        {
            _context.ContactPerson.Add(contactPerson);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetContactPerson", new { id = contactPerson.ID }, contactPerson);
        }

        // DELETE: api/ContactPersons/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ContactPerson>> DeleteContactPerson(int id)
        {
            var contactPerson = await _context.ContactPerson.FindAsync(id);
            if (contactPerson == null)
            {
                return NotFound();
            }

            _context.ContactPerson.Remove(contactPerson);
            await _context.SaveChangesAsync();

            return contactPerson;
        }

        private bool ContactPersonExists(int id)
        {
            return _context.ContactPerson.Any(e => e.ID == id);
        }
    }
}
