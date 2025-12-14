using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using spor_sitesi.Data;
using spor_sitesi.Models;
using System.Linq;

namespace spor_sitesi.Controllers
{
    [Authorize(Roles = "Admin")]
     public class HizmetlerController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HizmetlerController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: /Hizmetler
        public IActionResult Index()
        {
            var hizmetler = _context.Hizmetler.ToList();
            return View(hizmetler);
        }

        // GET: /Hizmetler/Create
        public IActionResult Create()
        {
            return View();   // → Views/Hizmetler/Create.cshtml arar
        }

        // POST: /Hizmetler/Create
        [HttpPost]
        public IActionResult Create(Hizmet hizmet)
        {
           
                _context.Hizmetler.Add(hizmet);
                _context.SaveChanges();
                return RedirectToAction("Index");
            

            return View(hizmet);
        }
    }
}
