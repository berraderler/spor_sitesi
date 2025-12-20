using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using spor_sitesi.Data;
using spor_sitesi.Models;
using System.Security.Claims;
using System.Linq;
using Microsoft.AspNetCore.Authorization;


namespace spor_sitesi.Controllers
{
    public class RandevularController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RandevularController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: /Randevular
        public IActionResult Index()
        {
            var query = _context.Randevular
       .Include(r => r.Hizmet)
       .Include(r => r.Salon)
       .Include(r => r.Antrenor)
       .AsQueryable();



            // Admin değilse sadece kendi randevularını görsün
            if (!User.IsInRole("Admin"))
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                query = query.Where(r => r.UyeId == userId);
            }

            var randevular = query.ToList();
            return View(randevular);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var randevu = _context.Randevular
                .FirstOrDefault(r => r.Id == id);

            if (randevu == null)
                return NotFound();

            ViewBag.Hizmetler = _context.Hizmetler.ToList();
            ViewBag.Antrenorler = _context.Antrenorler.ToList();

            return View(randevu);
        }

        // POST: /Randevular/Edit/5
        [HttpPost]

        public IActionResult Edit(Randevu randevu)
        {
            var secilenTarihSaat = randevu.Tarih.Date + randevu.BaslangicSaati;
            if (secilenTarihSaat < DateTime.Now)
            {
                ViewBag.Hizmetler = _context.Hizmetler.ToList();
                ViewBag.Antrenorler = _context.Antrenorler.ToList();

                ViewBag.Hata = "Geçmiş bir tarih/saat seçemezsiniz.";
                return View(randevu);
            }

            // Ücret = hizmetin ücreti olsun
            var hizmet = _context.Hizmetler.FirstOrDefault(h => h.Id == randevu.HizmetId);
            if (hizmet != null)
                randevu.Ucret = hizmet.Ucret;

            // Not boşsa null hata vermesin
            if (string.IsNullOrWhiteSpace(randevu.Not))
                randevu.Not = "";

            _context.Randevular.Update(randevu);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
        
        public IActionResult Delete(int id)
        {
            var randevu = _context.Randevular
                .Include(r => r.Hizmet)
                .Include(r => r.Antrenor)
                .FirstOrDefault(r => r.Id == id);

            if (randevu == null)
                return NotFound();

            return View(randevu);
        }

        // POST: /Randevular/Delete/5
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var randevu = _context.Randevular.Find(id);
            if (randevu == null)
                return NotFound();

            // Silme yerine iptal
            randevu.IptalEdildiMi = true;
            randevu.OnaylandiMi = false;

            _context.SaveChanges();
            return RedirectToAction("Index");
        }



        // GET: /Randevular/Create
        [HttpGet]
        public IActionResult Create()
        {
            // Create sayfası açıldığında dropdownlar dolu gelsin
            ViewBag.Salonlar = _context.Salonlar.ToList();
            ViewBag.Hizmetler = _context.Hizmetler.ToList();
            ViewBag.Antrenorler = _context.Antrenorler.ToList();

            return View();
        }

        // POST: /Randevular/Create
        [HttpPost]
        [HttpPost]
        public IActionResult Create(Randevu randevu)
        {
            // Dropdownlar (hata olursa geri dönerken lazım olacak)
            void LoadDropdowns()
            {
                ViewBag.Salonlar = _context.Salonlar.ToList();
                ViewBag.Hizmetler = _context.Hizmetler.ToList();
                ViewBag.Antrenorler = _context.Antrenorler.ToList();
            }

            // 0) Geçmiş tarih/saat kontrolü
            var secilenTarihSaat = randevu.Tarih.Date + randevu.BaslangicSaati;
            if (secilenTarihSaat < DateTime.Now)
            {
                LoadDropdowns();
                ViewBag.Hata = "Geçmiş bir tarih/saat için randevu oluşturamazsınız.";
                return View(randevu);
            }

            // 1) Antrenör müsaitlik kontrolü
            var antrenor = _context.Antrenorler
                .FirstOrDefault(a => a.Id == randevu.AntrenorId);

            if (antrenor == null)
            {
                ModelState.AddModelError("", "Antrenör bulunamadı.");
            }
            else
            {
                if (randevu.BaslangicSaati < antrenor.MusaitBaslangic ||
                    randevu.BaslangicSaati >= antrenor.MusaitBitis)
                {
                    ModelState.AddModelError("", "Seçilen saat antrenörün müsaitlik saatleri dışında.");
                }
            }

            // 2) ÇAKIŞMA kontrolü (aynı antrenör + aynı tarih + aynı saat)
            bool cakismaVarMi = _context.Randevular.Any(r =>
                r.AntrenorId == randevu.AntrenorId &&
                r.Tarih.Date == randevu.Tarih.Date &&
                r.BaslangicSaati == randevu.BaslangicSaati &&
                !r.IptalEdildiMi
            );

            if (cakismaVarMi)
            {
                ModelState.AddModelError("", "Bu antrenör bu saatte başka bir randevuya sahip.");
            }

            // 3) Hata varsa KAYDETME, formu geri göster
            if (!ModelState.IsValid)
            {
                LoadDropdowns();
                return View(randevu);
            }

            // 4) Ücret = seçilen hizmetin ücreti
            var hizmet = _context.Hizmetler.FirstOrDefault(h => h.Id == randevu.HizmetId);
            if (hizmet != null)
                randevu.Ucret = hizmet.Ucret;

            // 5) Giriş yapan kullanıcının Id'si
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            randevu.UyeId = userId;

            // 6) Not null olmasın
            if (string.IsNullOrWhiteSpace(randevu.Not))
                randevu.Not = "";

            _context.Randevular.Add(randevu);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult HizmetlerByAntrenor(int antrenorId)
        {
            var hizmetler = _context.AntrenorHizmetler
                .Where(x => x.AntrenorId == antrenorId)
                .Select(x => new { x.Hizmet.Id, x.Hizmet.Ad })
                .Distinct()
                .ToList();

            return Json(hizmetler);
        }


        [Authorize(Roles = "Admin")]
        public IActionResult Approve(int id)
        {
            var randevu = _context.Randevular.Find(id);
            if (randevu == null) return NotFound();

            randevu.OnaylandiMi = true;
            randevu.IptalEdildiMi = false;

            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Cancel(int id)
        {
            var randevu = _context.Randevular.Find(id);
            if (randevu == null) return NotFound();

            randevu.IptalEdildiMi = true;
            randevu.OnaylandiMi = false;

            _context.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}
