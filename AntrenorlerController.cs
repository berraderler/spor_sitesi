using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using spor_sitesi.Data;
using spor_sitesi.Models;
using System.Linq;

namespace spor_sitesi.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AntrenorlerController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AntrenorlerController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: /Antrenorler
        public IActionResult Index()
        {
            var antrenorler = _context.Antrenorler.ToList();
            return View(antrenorler);
        }

        // GET: /Antrenorler/Create
        [HttpGet]
        public IActionResult Create()
        {

            return View();
        }

        // POST: /Antrenorler/Create
        [HttpPost]
        public IActionResult Create(Antrenor antrenor)
        {
            if (antrenor.MusaitBaslangic >= antrenor.MusaitBitis)
            {
                ModelState.AddModelError("", "Müsait başlangıç, bitişten küçük olmalı.");
                return View(antrenor);
            }

            _context.Antrenorler.Add(antrenor);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
