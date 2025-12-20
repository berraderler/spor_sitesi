using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Spor_web_sitesi.Data;
using Spor_web_sitesi.Models;

namespace Spor_web_sitesi.Controllers
{
    [Authorize(Roles = "Admin")]
    public class HizmetController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HizmetController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Sadece admin görebilir
        public async Task<IActionResult> Index()
        {
            return View(await _context.Hizmetler.ToListAsync());
        }


        [HttpGet]
        public IActionResult Ekle()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Ekle(Hizmet h)
        {
            if (!ModelState.IsValid)
                return View(h);

            await _context.Hizmetler.AddAsync(h);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        

        // Silme işlemi (Index.cshtml'deki silme butonu için)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Sil(int id)
        {
            var hizmet = await _context.Hizmetler.FindAsync(id);
            if (hizmet != null)
            {
                _context.Hizmetler.Remove(hizmet);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}