using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;

namespace WebProgramming.Controllers
{
    public class SalonsController : Controller
    {
        
            private readonly AppDbContext _context;

            public SalonsController(AppDbContext context)
            {
                _context = context;
            }

            public async Task<IActionResult> Index()
            {
                return View(await _context.Salons.ToListAsync());
            }

            public IActionResult Create()
            {
                return View();
            }

            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> Create(Salon salon)
            {
                if (ModelState.IsValid)
                {
                    _context.Add(salon);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                return View(salon);
            }

            public async Task<IActionResult> Edit(int id)
            {
                var salon = await _context.Salons.FindAsync(id);
                if (salon == null)
                {
                    return NotFound();
                }
                return View(salon);
            }

            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> Edit(int id, Salon salon)
            {
                if (id != salon.Id)
                {
                    return NotFound();
                }

                if (ModelState.IsValid)
                {
                    try
                    {
                        _context.Update(salon);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!SalonExists(salon.Id))
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
                return View(salon);
            }

            public async Task<IActionResult> Delete(int id)
            {
                var salon = await _context.Salons.FindAsync(id);
                if (salon == null)
                {
                    return NotFound();
                }
                return View(salon);
            }

            [HttpPost, ActionName("Delete")]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> DeleteConfirmed(int id)
            {
                var salon = await _context.Salons.FindAsync(id);
                if (salon != null)
                {
                    _context.Salons.Remove(salon);
                    await _context.SaveChangesAsync();
                }
                return RedirectToAction(nameof(Index));
            }

            private bool SalonExists(int id)
            {
                return _context.Salons.Any(e => e.Id == id);
            }
        
    }
}
