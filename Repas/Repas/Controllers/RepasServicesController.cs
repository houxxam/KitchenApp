using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Repas.Data;
using Repas.Models;

namespace Repas.Controllers
{
    public class RepasServicesController : Controller
    {
        private readonly AppDbContext _context;

        public RepasServicesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: RepasServices
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.RepasServices.Include(r => r.Service)
                .Include(r => r.TypeRepas)
                .Include(d => d.dateForniture)
                .OrderByDescending(r => r.dateForniture.FornitureDate)
            .GroupBy(d => d.dateForniture.Id)
            .Select(g => g.First());

            return View(await appDbContext.ToListAsync());
        }

        // GET: RepasServices/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.RepasServices == null)
            {
                return NotFound();
            }

            var repasService = await _context.RepasServices
                .Include(r => r.Service)
                .Include(r => r.TypeRepas)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (repasService == null)
            {
                return NotFound();
            }

            return View(repasService);
        }

        // GET: RepasServices/Create
        public IActionResult Create()
        {


            if (TempData["FornitureDate"] is string dateFornitureJson)
            {

                DateForniture dateForniture = JsonConvert.DeserializeObject<DateForniture>(dateFornitureJson);


                ViewBag.DateForniture = dateForniture.FornitureDate.ToString("dd/MM/yyyy");
                ViewBag.IdDateforniture = dateForniture.Id;
            }



            var services = _context.Services.ToList();
            ViewBag.Services = services;

            var typeRepas = _context.TypeRepas.ToList();
            ViewBag.TypeRepas = typeRepas;

            ViewData["ServiceId"] = new SelectList(_context.Services, "Id", "ServiceName");
            ViewData["TypeRepasId"] = new SelectList(_context.TypeRepas, "Id", "Type");
            return View();
        }

        // POST: RepasServices/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,TotalRepas,TypeRepasId,ServiceId,destination,DateFornitureId")] RepasService repasService)
        {
            if (TempData["FornitureDate"] is DateTime fornitureDate)
            {
                // Pass fornitureDate value to the view using ViewBag
                ViewBag.FornitureDate = fornitureDate;
                ViewBag.FornitureDateId = repasService.DateFornitureId;


            }
            if (ModelState.IsValid)
            {
                _context.Add(repasService);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ServiceId"] = new SelectList(_context.Services, "Id", "Id", repasService.ServiceId);
            ViewData["TypeRepasId"] = new SelectList(_context.TypeRepas, "Id", "Id", repasService.TypeRepasId);
            return View(repasService);
        }

        // GET: RepasServices/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.RepasServices == null)
            {
                return NotFound();
            }

            var repasService = await _context.RepasServices
                .Include(i => i.dateForniture)
                .Include(s => s.Service)
                .Include(t => t.TypeRepas)
                .FirstOrDefaultAsync(i => i.Id == id);

            ViewBag.date = repasService.dateForniture.FornitureDate.ToString("dd/MM/yyyy");
            ViewBag.datId = repasService.DateFornitureId;

            // Your code to retrieve the repasService with the given id
            RepasService _repasService = _context.RepasServices.Find(id);

            




            var typeRepas = _context.TypeRepas.ToList();
            ViewBag.TypeRepas = typeRepas;
            var services = _context.Services.ToList();
            ViewBag.Services = services;



            if (repasService == null)
            {
                return NotFound();
            }

            var repasServices = await _context.RepasServices
                .Include(s => s.Service)
                .Include(t => t.TypeRepas)
                .Where(d => d.DateFornitureId == repasService.DateFornitureId)
                .ToListAsync();




            var list = new List<RepasServiceDTO>();
            foreach (var item in repasServices)
            {
                list.Add(new RepasServiceDTO()
                {
                    Id = item.Id,
                    Destination = item.destination,
                    TypeRepasName = item.TypeRepas?.Type,
                    TotalRapas = item.TotalRepas,
                    ServiceName = item.Service?.ServiceName
                });
            }
            ViewBag.RepasServiceList = list;
            return View(list);
        }




        // POST: RepasServices/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,TotalRepas,TypeRepasId,ServiceId,destination,DateFornitureId")] RepasService repasService)
        {
            if (id != repasService.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(repasService);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RepasServiceExists(repasService.Id))
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
            ViewData["ServiceId"] = new SelectList(_context.Services, "Id", "Id", repasService.ServiceId);
            ViewData["TypeRepasId"] = new SelectList(_context.TypeRepas, "Id", "Id", repasService.TypeRepasId);
            return View(repasService);
        }

        // GET: RepasServices/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.RepasServices == null)
            {
                return NotFound();
            }

            var repasService = await _context.RepasServices
                .Include(r => r.Service)
                .Include(r => r.TypeRepas)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (repasService == null)
            {
                return NotFound();
            }

            return View(repasService);
        }

        // POST: RepasServices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.RepasServices == null)
            {
                return Problem("Entity set 'AppDbContext.RepasServices'  is null.");
            }
            var repasService = await _context.RepasServices.FindAsync(id);
            if (repasService != null)
            {
                _context.RepasServices.Remove(repasService);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RepasServiceExists(int id)
        {
            return (_context.RepasServices?.Any(e => e.Id == id)).GetValueOrDefault();
        }



        public IActionResult getTpeRepasListbyDestination(Destination dest)
        {

            var typeRepas = _context.TypeRepas.Where(d => d.Destination == dest).ToList();


            ViewBag.TypeRepas = typeRepas;

            return View();
        }
    }
}
