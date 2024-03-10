﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Repas.Data;
using Repas.Models;

namespace Repas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RepasServicesApiController : ControllerBase
    {
        private readonly AppDbContext _context;

        public RepasServicesApiController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/RepasServicesApi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RepasService>>> GetRepasServices()
        {
            if (_context.RepasServices == null)
            {
                return NotFound();
            }
            return await _context.RepasServices.ToListAsync();
        }

        // GET: api/RepasServicesApi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RepasService>> GetRepasService(int id)
        {
            if (_context.RepasServices == null)
            {
                return NotFound();
            }
            var repasService = await _context.RepasServices.FindAsync(id);

            if (repasService == null)
            {
                return NotFound();
            }

            return repasService;
        }

        // PUT: api/RepasServicesApi/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRepasService(int id, RepasService repasService)
        {
            if (id != repasService.Id)
            {
                return BadRequest();
            }

            _context.Entry(repasService).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RepasServiceExists(id))
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

        // POST: api/RepasServicesApi
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<RepasService>> PostRepasService(RepasService repasService)
        {
            if (_context.RepasServices == null)
            {
                return Problem("Entity set 'AppDbContext.RepasServices'  is null.");
            }

            _context.RepasServices.Add(repasService);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRepasService", new { id = repasService.Id }, repasService);
        }

        // DELETE: api/RepasServicesApi/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRepasService(int id)
        {
            if (_context.RepasServices == null)
            {
                return NotFound();
            }
            var repasService = await _context.RepasServices.FindAsync(id);
            if (repasService == null)
            {
                return NotFound();
            }

            _context.RepasServices.Remove(repasService);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RepasServiceExists(int id)
        {
            return (_context.RepasServices?.Any(e => e.Id == id)).GetValueOrDefault();
        }


        // GET: api/RepasServicesApi/date/{DateFornitureId}
        [HttpGet("date/{DateFornitureId}")]
        public async Task<ActionResult<List<RepasService>>> GetRepasServiceByDateId(int DateFornitureId)
        {
            var repasServices = await _context.RepasServices
                .Where(d => d.DateFornitureId == DateFornitureId)
                .ToListAsync();
            var list = new List<RepasServiceDTO>();
            foreach (var item in repasServices)
            {
                list.Add(new RepasServiceDTO()
                {
                    Destination = item.destination,
                    TypeRepasName = item.TypeRepas?.Type,
                    TotalRapas = item.TotalRepas,
                    ServiceName = item.Service?.ServiceName
                });
            }

            if (repasServices == null || repasServices.Count == 0)
            {
                return NotFound();
            }

            return repasServices;
        }


    }
}
