using WebProgramming.Data;
using WebProgramming.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace WebProgramming.Controllers
{
    public class AppointmentsController : Controller
    {
        private readonly AppDbContext _context;

        public AppointmentsController(AppDbContext context)
        {
            _context = context;
        }

        // Listeleme
        public IActionResult Index()
        {
            var appointments = _context.Appointments
                .Include(a => a.User)
                .Include(a => a.Service)
                .ToList();
            return View(appointments);
        }

        // Ekleme Formu
        public IActionResult Create()
        {
            ViewData["Users"] = new SelectList(_context.Users, "Id", "FirstName");
            ViewData["Services"] = new SelectList(_context.Services, "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Appointment appointment)
        {
            if (ModelState.IsValid)
            {
                _context.Appointments.Add(appointment);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Users"] = new SelectList(_context.Users, "Id", "FirstName", appointment.UserId);
            ViewData["Services"] = new SelectList(_context.Services, "Id", "Name", appointment.ServiceId);
            return View(appointment);
        }

        // Düzenleme Formu
        public IActionResult Edit(int id)
        {
            var appointment = _context.Appointments.Find(id);
            if (appointment == null)
            {
                return NotFound();
            }
            ViewData["Users"] = new SelectList(_context.Users, "Id", "FirstName", appointment.UserId);
            ViewData["Services"] = new SelectList(_context.Services, "Id", "Name", appointment.ServiceId);
            return View(appointment);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Appointment appointment)
        {
            if (id != appointment.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _context.Appointments.Update(appointment);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Users"] = new SelectList(_context.Users, "Id", "FirstName", appointment.UserId);
            ViewData["Services"] = new SelectList(_context.Services, "Id", "Name", appointment.ServiceId);
            return View(appointment);
        }

        // Silme Formu
        public IActionResult Delete(int id)
        {
            var appointment = _context.Appointments
                .Include(a => a.User)
                .Include(a => a.Service)
                .FirstOrDefault(m => m.Id == id);
            if (appointment == null)
            {
                return NotFound();
            }
            return View(appointment);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var appointment = _context.Appointments.Find(id);
            if (appointment != null)
            {
                _context.Appointments.Remove(appointment);
                _context.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
