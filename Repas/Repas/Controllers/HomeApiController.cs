using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Repas.Data;
using Repas.Models;
using System.Globalization;

namespace Repas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeApiController : ControllerBase
    {
        private readonly AppDbContext _context;

        public HomeApiController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DateForniture>>> GetDateForniture()
        {
            if (_context.DateFornitures == null)
            {
                return NotFound();
            }
            return await _context.DateFornitures.ToListAsync();
        }

        // GET: api/HomeApi/date/{DateForniture}
        [HttpGet("date/{DateForniture}")]
        public async Task<ActionResult<List<DateForniture>>> GetDate(string DateForniture)
        {

            if (!DateTime.TryParseExact(DateForniture, "yyyy-MM-ddTHH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime date))
            {
                return BadRequest("Invalid date format. Please provide the date in the format yyyy-MM-ddTHH:mm:ss.");
            }

            var dateForniture = await _context.DateFornitures
             .Where(d => d.FornitureDate.Date == date.Date) // Compare only date portion
             .ToListAsync();

            //if (dateForniture == null || dateForniture.Count == 0)
            //{
            //    return NotFound();
            //}

            return dateForniture;
        }

        // GET: api/HomeApi/date/{DateFornitureId}
        [HttpGet("dateid/{DateFornitureId}")]
        public async Task<ActionResult<List<RepasService>>> GetRepasServiceByDateId(int DateFornitureId)
        {
            var repasServices = await _context.RepasServices
                .Where(d => d.DateFornitureId == DateFornitureId)
                .ToListAsync();
            
            if (repasServices == null || repasServices.Count == 0)
            {
                return NotFound();
            }

            return repasServices;
        }

    }
}
