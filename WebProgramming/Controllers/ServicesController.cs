using Microsoft.AspNetCore.Mvc;
using WebProgramming.Models;

namespace WebProgramming.Controllers
{
    [Route("api/services")]
    [ApiController]
    public class ServicesController : ControllerBase
    {
        private readonly BarberContext _context;

        public ServicesController(BarberContext context)
        {
            _context = context;
        }

        // Tüm hizmetleri listeleme
        [HttpGet]
        public async Task<IActionResult> GetServices()
        {
            var services = await Task.FromResult(_context.Services.ToList());
            return Ok(services);
        }

        // Belirli bir hizmeti ID ile alma
        [HttpGet("{id}")]
        public async Task<IActionResult> GetService(int id)
        {
            var service = await Task.FromResult(_context.Services.Find(id));

            if (service == null)
                return NotFound(new { message = "Hizmet bulunamadı." });

            return Ok(service);
        }

        // Yeni bir hizmet ekleme
        [HttpPost]
        public async Task<IActionResult> AddService([FromBody] Service service)
        {
            if (service == null)
                return BadRequest(new { message = "Geçersiz hizmet verisi." });

            _context.Services.Add(service);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetService), new { id = service.ServiceID }, service);
        }

        // Mevcut bir hizmeti güncelleme
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateService(int id, [FromBody] Service updatedService)
        {
            if (id != updatedService.ServiceID)
                return BadRequest(new { message = "ID uyuşmazlığı." });

            var service = _context.Services.Find(id);
            if (service == null)
                return NotFound(new { message = "Hizmet bulunamadı." });

            service.ServiceName = updatedService.ServiceName;
            service.Duration = updatedService.Duration;
            service.Price = updatedService.Price;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        // Bir hizmeti silme
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteService(int id)
        {
            var service = _context.Services.Find(id);
            if (service == null)
                return NotFound(new { message = "Hizmet bulunamadı." });

            _context.Services.Remove(service);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }

}
