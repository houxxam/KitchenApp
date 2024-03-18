using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Repas.Data;
using Repas.Models;

namespace Repas.Controllers
{
    public class TypeRepasController : Controller
    {
        private readonly AppDbContext _context;

        public TypeRepasController(AppDbContext context)
        {
            _context = context;
        }

        // GET: TypeRepas
        public async Task<IActionResult> Index()
        {
            return _context.TypeRepas != null ?
                        View(await _context.TypeRepas.ToListAsync()) :
                        Problem("Entity set 'AppDbContext.TypeRepas'  is null.");
        }

        // GET: TypeRepas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TypeRepas == null)
            {
                return NotFound();
            }

            var typeRepas = await _context.TypeRepas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (typeRepas == null)
            {
                return NotFound();
            }

            return View(typeRepas);
        }

        // GET: TypeRepas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TypeRepas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Type,Destination")] TypeRepas typeRepas)
        {
            if (ModelState.IsValid)
            {

                _context.Add(typeRepas);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(typeRepas);
        }

        // GET: TypeRepas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.TypeRepas == null)
            {
                return NotFound();
            }

            var typeRepas = await _context.TypeRepas.FindAsync(id);
            if (typeRepas == null)
            {
                return NotFound();
            }
            return View(typeRepas);
        }

        // POST: TypeRepas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Type,Destination")] TypeRepas typeRepas)
        {
            if (id != typeRepas.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(typeRepas);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TypeRepasExists(typeRepas.Id))
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
            return View(typeRepas);
        }

        // GET: TypeRepas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.TypeRepas == null)
            {
                return NotFound();
            }

            var typeRepas = await _context.TypeRepas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (typeRepas == null)
            {
                return NotFound();
            }

            return View(typeRepas);
        }

        // POST: TypeRepas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.TypeRepas == null)
            {
                return Problem("Entity set 'AppDbContext.TypeRepas'  is null.");
            }
            var typeRepas = await _context.TypeRepas.FindAsync(id);
            if (typeRepas != null)
            {
                _context.TypeRepas.Remove(typeRepas);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TypeRepasExists(int id)
        {
            return (_context.TypeRepas?.Any(e => e.Id == id)).GetValueOrDefault();
        }


    }
}
