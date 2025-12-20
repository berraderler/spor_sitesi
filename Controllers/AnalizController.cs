using Google.GenAI;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Spor_web_sitesi.Data;
using Spor_web_sitesi.DTOs.Analiz;
using Spor_web_sitesi.Identity;
using Spor_web_sitesi.Models;

namespace Spor_web_sitesi.Controllers
{
    [Authorize(Roles = "User")]
    public class AnalizController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly Client _genAIClient;

        public AnalizController(ApplicationDbContext context, UserManager<AppUser> userManager, IConfiguration _configuration)
        {
            _context = context;
            _userManager = userManager;
            _genAIClient = new Client(apiKey: _configuration["GEMINI_API_KEY"]);
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.FindByNameAsync(User.Identity!.Name!);
            var analizler = await _context.Analizler.Where(a => a.UyeId == user!.Id).ToListAsync();
            return View(analizler);
        }

        [HttpGet]
        public IActionResult Ekle()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Ekle(AnalizEkleViewModel vm)
        {
            if (!ModelState.IsValid)
                return View(vm);

            if (vm.Gorsel == null || !vm.Gorsel.ContentType.StartsWith("image/"))
            {
                ModelState.AddModelError("Gorsel", "Lütfen geçerli bir görsel yükleyiniz.");
                return View(vm);
            }

            byte[] imageBytes;
            using (var ms = new MemoryStream())
            {
                await vm.Gorsel.CopyToAsync(ms);
                imageBytes = ms.ToArray();
            }

            var prompt = $@"
Sen profesyonel bir fitness ve vücut analiz uzmanısın.

Aşağıdaki bilgilere göre kullanıcı için bir vücut analizi yap:
- Kilo: {vm.Kilo} kg
- Boy: {vm.BoyCm} cm
- Yüklenen vücut görseli

Lütfen analiz sonucunu TEK BİR METİN (string) halinde, Türkçe olarak ver.

Analizde şunlar mutlaka olsun:
1. Tahmini vücut tipi
2. Yağ oranı eğilimi (düşük / orta / yüksek)
3. Genel fiziksel durum
4. Kısa ve kişisel beslenme önerileri
5. Kısa ve kişisel egzersiz önerileri

Gerçekçi, net ve abartısız yaz.";

            var response = await _genAIClient.Models.GenerateContentAsync(
                model: "gemini-2.5-flash",
                contents: new List<Google.GenAI.Types.Content>
                {
                    new Google.GenAI.Types.Content
                    {
                        Parts = new List<Google.GenAI.Types.Part>
                        {
                            new Google.GenAI.Types.Part { Text = prompt },
                            new Google.GenAI.Types.Part
                            {
                                InlineData = new Google.GenAI.Types.Blob
                                {
                                    MimeType = vm.Gorsel.ContentType,
                                    Data = imageBytes
                                }
                            }
                        }
                    }
                }
            );

            string analizSonucu = response?.Candidates[0].Content.Parts[0].Text ?? "Analiz sonucu üretilemedi.";

            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            var analiz = new Analiz
            {
                Id = Guid.NewGuid(),
                AnalizOzeti = analizSonucu,
                AnalizTarihi = DateTime.Now,
                BoyCm = vm.BoyCm,
                Kilo = vm.Kilo,
                UyeId = user.Id
            };

            await _context.Analizler.AddAsync(analiz);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }
    }
}