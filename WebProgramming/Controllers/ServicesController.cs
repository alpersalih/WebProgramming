using Microsoft.AspNetCore.Mvc;
using WebProgramming.Models;

namespace WebProgramming.Controllers
{
    public class ServicesController : Controller
    {
        private readonly BarberContext _context;

        public ServicesController(BarberContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var services = _context.Services.ToList();
            return View(services);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Service service)
        {
            if (ModelState.IsValid)
            {
                _context.Services.Add(service);
                _context.SaveChanges();
                return RedirectToAction("Index ");
            }
            return View(service);
        }

        [Route("api/[controller]")]
        [ApiController]
        public class ServicesApiController : ControllerBase
        {
            private readonly BarberContext _context;

            public ServicesApiController(BarberContext context)
            {
                _context = context;
            }

            // Tüm hizmetleri listeleme
            [HttpGet]
            public IActionResult GetAllServices()
            {
                var services = _context.Services.ToList();
                return Ok(services);
            }

            // Yeni hizmet ekleme
            [HttpPost]
            public IActionResult CreateService([FromBody] Service service)
            {
                if (ModelState.IsValid)
                {
                    _context.Services.Add(service);
                    _context.SaveChanges();
                    return CreatedAtAction(nameof(GetAllServices), new { id = service.ServiceID }, service);
                }
                return BadRequest(ModelState);
            }
        }


    }

}
