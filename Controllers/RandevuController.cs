using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Spor_web_sitesi.Data;
using Spor_web_sitesi.Identity;
using Spor_web_sitesi.Models;

namespace Spor_web_sitesi.Controllers
{
    [Authorize]
    public class RandevuController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<AppUser> _userManager;

        public RandevuController(ApplicationDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // Randevu Listesi (Admin Görür)
        public async Task<IActionResult> Index()
        {
            string? memberId = null;
            if (User.IsInRole("User"))
            {
                var user = await _userManager.FindByNameAsync(User.Identity!.Name!);
                memberId = await _userManager.GetUserIdAsync(user!);
            }
            var query = _context.Randevular
                .Include(r => r.Antrenor)
                .Include(r => r.Hizmet)
                .AsQueryable();

            var userQuery = _context.Users.AsQueryable();

            if(memberId != null)
            {
                query = query.Where(r => r.UyeId == memberId);
                userQuery = userQuery.Where(r => r.Id == memberId);
            }

            var randevular = await query.ToListAsync();
            var users = await userQuery.ToListAsync();
            ViewBag.Users = users;

            return View(randevular);
        }

        // Randevu Alma Sayfası
        [HttpGet]
        public IActionResult Al()
        {
            if (User.IsInRole("Admin"))
            {
                ViewBag.Uyeler = _context.Users.ToList();
            }
            ViewBag.Antrenorler = _context.Antrenorler.ToList();
            ViewBag.Hizmetler = _context.Hizmetler
                .Select(h => new
                {
                    h.Id,
                    Gosterim = h.Ad + " (" + h.Ucret + "₺)"
                })
                .ToList();
            return View();
        }

        // Randevu Kaydetme ve Çakışma Kontrolü
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Al(Randevu randevu)
        {
            // 1. Antrenörün Müsaitlik Kontrolü
            string secilenGun = randevu.RandevuTarihi.ToString("dddd", new System.Globalization.CultureInfo("tr-TR"));
            TimeSpan secilenSaat = randevu.RandevuTarihi.TimeOfDay;

            var musaitMi = await _context.AntrenorMusaitlikler.AnyAsync(m =>
                m.AntrenorId == randevu.AntrenorId &&
                m.Gun == secilenGun &&
                secilenSaat >= m.BaslangicSaat &&
                secilenSaat <= m.BitisSaat);
            if (!musaitMi)
            {
                ModelState.AddModelError("", "Seçilen antrenör bu saatte çalışmamaktadır.");
            }

            // 2. Çakışma Kontrolü (Dolu Saat Kontrolü)
            var cakismaVarMi = await _context.Randevular.AnyAsync(r =>
                r.AntrenorId == randevu.AntrenorId &&
                r.RandevuTarihi == randevu.RandevuTarihi &&
                r.Durum != "Reddedildi");

            if (cakismaVarMi)
            {
                ModelState.AddModelError("", "Bu saatte antrenörün başka bir randevusu bulunmaktadır.");
            }

            if (ModelState.IsValid)
            {
                randevu.Durum = "Beklemede";
                randevu.OlusturmaTarihi = DateTime.Now;
                string? memberId;

                if (User.IsInRole("Admin"))
                {
                    memberId = randevu.UyeId;
                }
                else
                {
                    var user = await _userManager.FindByNameAsync(User.Identity.Name);
                    memberId = await _userManager.GetUserIdAsync(user);
                }
                randevu.UyeId = memberId ?? "Anonim";

                _context.Add(randevu);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Randevu"); // Başarılıysa ana sayfaya
            }

            // Hata varsa listeleri tekrar doldur
            ViewBag.Antrenorler = _context.Antrenorler.ToList();
            ViewBag.Hizmetler = _context.Hizmetler.ToList();
            return View(randevu);
        }

        // Randevu Onaylama (Sadece Admin)
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Onayla(int id)
        {
            var randevu = await _context.Randevular.FindAsync(id);
            if (randevu != null)
            {
                randevu.Durum = "Onaylandı";
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Iptal(int id)
        {
            var randevu = await _context.Randevular.FindAsync(id);
            if (randevu != null)
            {
                randevu.Durum = "Reddedildi";
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}