using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Repas.Data;
using Repas.Models;

namespace Repas.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AppDbContext _context;



        public HomeController(ILogger<HomeController> logger, AppDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        // GET: DateFornitures
        public async Task<IActionResult> Index()
        {
            return _context.DateFornitures != null ?
                        View(await _context.DateFornitures.ToListAsync()) :
                        Problem("Entity set 'AppDbContext.DateFornitures'  is null.");
        }

        // GET: DateFornitures/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.DateFornitures == null)
            {
                return NotFound();
            }

            var dateForniture = await _context.DateFornitures
                .FirstOrDefaultAsync(m => m.Id == id);
            if (dateForniture == null)
            {
                return NotFound();
            }

            return View(dateForniture);
        }

        // GET: DateFornitures/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: DateFornitures/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FornitureDate")] DateForniture dateForniture)
        {
            var existingDate = await _context.DateFornitures.FirstOrDefaultAsync(d => d.FornitureDate == dateForniture.FornitureDate);

            if (existingDate != null)
            {
                ModelState.AddModelError("FornitureDate", "Date already exists.");
            }

            if (ModelState.IsValid)
            {

                _context.Add(dateForniture);
                await _context.SaveChangesAsync();

                // Serialize the dateForniture object to JSON string
                string dateFornitureJson = JsonConvert.SerializeObject(dateForniture);

                // Store the serialized object in TempData
                TempData["FornitureDate"] = dateFornitureJson;

                // Redirect to the Create action of RepasServices controller and pass dateForniture
                return RedirectToAction("Create", "RepasServices");

            }

            return View(dateForniture);
        }


        // GET: DateFornitures/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.DateFornitures == null)
            {
                return NotFound();
            }

            var dateForniture = await _context.DateFornitures.FindAsync(id);
            if (dateForniture == null)
            {
                return NotFound();
            }
            return View(dateForniture);
        }

        // POST: DateFornitures/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FornitureDate")] DateForniture dateForniture)
        {
            if (id != dateForniture.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(dateForniture);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DateFornitureExists(dateForniture.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(dateForniture);
        }

        // GET: DateFornitures/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.DateFornitures == null)
            {
                return NotFound();
            }

            var dateForniture = await _context.DateFornitures
                .FirstOrDefaultAsync(m => m.Id == id);
            if (dateForniture == null)
            {
                return NotFound();
            }

            return View(dateForniture);
        }

        // POST: DateFornitures/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.DateFornitures == null)
            {
                return Problem("Entity set 'AppDbContext.DateFornitures'  is null.");
            }
            var dateForniture = await _context.DateFornitures.FindAsync(id);
            if (dateForniture != null)
            {
                _context.DateFornitures.Remove(dateForniture);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DateFornitureExists(int id)
        {
            return (_context.DateFornitures?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
