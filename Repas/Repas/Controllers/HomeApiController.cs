using Humanizer;
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
        public async Task<ActionResult<DateForniture?>> GetDate(string DateForniture)
        {

            if (!DateTime.TryParseExact(DateForniture, "yyyy-MM-ddTHH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime date))
            {
                return BadRequest("Invalid date format. Please provide the date in the format yyyy-MM-ddTHH:mm:ss.");
            }

            return await _context.DateFornitures
             .Where(d => d.FornitureDate.Date == date.Date) // Compare only date portion
             .FirstOrDefaultAsync();

            //if (dateForniture == null || dateForniture.Count == 0)
            //{
            //    return NotFound();
            //}

            
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

        [HttpPost("date/create")]
        public async Task<ActionResult<DateForniture>> CreateDate([FromBody] DateForniture dateForniture)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                
                var _dateForniture = new DateForniture();
              
                _context.DateFornitures.Add(dateForniture);

                await _context.SaveChangesAsync();



                //return RedirectToAction("Create", "RepasServices", new { date = dateForniture.FornitureDate, id = dateForniture.Id });
                return Ok(new DateForniture
                {
                    FornitureDate = dateForniture.FornitureDate,
                    Id = dateForniture.Id
                });

            }
            catch (Exception ex)
            {
                // Log the error
                Console.WriteLine($"Error occurred while creating DateForniture: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }


     

        

    }
}
