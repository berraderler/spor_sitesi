using Microsoft.AspNetCore.Mvc;
using spor_sitesi.Data;
using spor_sitesi.Models;
using Microsoft.AspNetCore.Authorization;


namespace spor_sitesi.Controllers
{
    [Authorize(Roles = "Admin")]

    public class SalonsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SalonsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: /Salons
        public IActionResult Index()
        {
            var salons = _context.Salonlar.ToList();
            return View(salons);
        }
        // GET: /Salons/Create
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Salon salon)
        {
            if (ModelState.IsValid)
            {
                _context.Salonlar.Add(salon);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(salon);
        }


    }
}
