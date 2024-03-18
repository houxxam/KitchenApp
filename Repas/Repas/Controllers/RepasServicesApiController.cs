using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Repas.Data;
using Repas.Models;
using System.Globalization;

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
            return await _context.RepasServices.Include(d=>d.dateForniture).ToListAsync();
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
            if (repasService == null || id != repasService.Id || id == null)
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
            return Ok();

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

        [HttpGet("test/{dateId:int}")]
        public async Task<ActionResult> getdataByDateId(int dateId)
        {
            var repas = await _context.RepasServices.Include(r => r.Service)
                .Include(r => r.TypeRepas)
                .Include(d => d.dateForniture)
                .Where(d=>d.DateFornitureId == dateId)
                .ToListAsync();

            var typerepas = await _context.TypeRepas.ToListAsync();
            

            var TotalRepasByType = new List<TypeRepasDTO>();
            
            
            if(repas != null)
            {
                foreach (var _typeRepas in typerepas)
                {
                    if(!checkTypeRepas(TotalRepasByType, _typeRepas.Type)) {
                        TotalRepasByType.Add(new TypeRepasDTO(_typeRepas.Type, 0));
                    }
                    
                }

                foreach(var repa in repas)
                {
                    var item = getTypByName(TotalRepasByType, repa.TypeRepas.Type);
                    if(item != null)
                    {
                        item.Count = item.Count+ repa.TotalRepas.Value;
                    }
                }
            }

            return Ok(TotalRepasByType);
        }

        public bool checkTypeRepas(List<TypeRepasDTO> typeList, string type)
        {
            foreach (var item in typeList)
            {
                if (item.Name.ToLower() == type.ToLower())
                {
                    return true;

                }
            }
            return false;
        }
        public TypeRepasDTO getTypByName(List<TypeRepasDTO> typeList, string type)
        {
            foreach (var item in typeList)
            {
                if (item.Name.ToLower() == type.ToLower())
                {
                    return item;

                }
            }
            return null;
        }

    }
}
