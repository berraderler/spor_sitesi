using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Spor_web_sitesi.Data;
using Spor_web_sitesi.DTOs.Antrenor;
using Spor_web_sitesi.Models;

// DÜZELTME: Namespace projenin asıl adı olan Spor_web_sitesi yapıldı.
namespace Spor_web_sitesi.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AntrenorController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AntrenorController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Eğitmen Listesi
        public async Task<IActionResult> Index()
        {
            return View(await _context.Antrenorler.ToListAsync());
        }

        // --- YENİ EKLEME: Eğitmen Ekleme Sayfaları ---
        [HttpGet]
        public IActionResult Ekle()
        {
            return View(new AntrenorEkleViewModel());
        }

        [HttpPost]
        public IActionResult Ekle(AntrenorEkleViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var antrenor = new Antrenor
            {
                AdSoyad = model.AdSoyad,
                UzmanlikAlani = model.UzmanlikAlani,
                Biyografi = model.Biyografi
            };

            _context.Antrenorler.Add(antrenor);
            _context.SaveChanges();

            foreach (var gun in model.Gunler)
            {
                var musaitlik = new AntrenorMusaitlik
                {
                    AntrenorId = antrenor.Id,
                    Gun = gun,
                    BaslangicSaat = model.BaslangicSaat,
                    BitisSaat = model.BitisSaat
                };

                _context.AntrenorMusaitlikler.Add(musaitlik);
            }

            _context.SaveChanges();

            return RedirectToAction("Index");
        }



        // --- MÜSAİTLİK YÖNETİMİ ---
        public async Task<IActionResult> MusaitlikYonetimi(int id)
        {
            var antrenor = await _context.Antrenorler
                .Include(a => a.Musaitlikler)
                .FirstOrDefaultAsync(a => a.Id == id);

            if (antrenor == null) return NotFound();

            return View(antrenor);
        }

        [HttpPost]
        public async Task<IActionResult> MusaitlikEkle(int antrenorId, string gun, string baslangic, string bitis)
        {
            // Zaman formatı kontrolü
            var yeni = new AntrenorMusaitlik
            {
                AntrenorId = antrenorId,
                Gun = gun,
                BaslangicSaat = TimeSpan.Parse(baslangic),
                BitisSaat = TimeSpan.Parse(bitis)
            };

            _context.AntrenorMusaitlikler.Add(yeni);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(MusaitlikYonetimi), new { id = antrenorId });
        }

        [HttpPost]
        public async Task<IActionResult> MusaitlikSil(int id, int antrenorId)
        {
            var musaitlik = await _context.Antrenorler.SelectMany(a => a.Musaitlikler).FirstOrDefaultAsync(m => m.Id == id);

            // Not: AntrenorMusaitlikler tablosu dbContext'te tanımlıysa direkt buradan silmek daha garantidir:
            var mSil = await _context.AntrenorMusaitlikler.FindAsync(id);

            if (mSil != null)
            {
                _context.AntrenorMusaitlikler.Remove(mSil);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(MusaitlikYonetimi), new { id = antrenorId });
        }

        // Eğitmen Silme
        [HttpPost]
        public async Task<IActionResult> Sil(int id)
        {
            var antrenor = await _context.Antrenorler.FindAsync(id);
            if (antrenor != null)
            {
                _context.Antrenorler.Remove(antrenor);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}