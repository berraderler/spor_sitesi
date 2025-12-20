using System.ComponentModel.DataAnnotations;

namespace Spor_web_sitesi.DTOs.Antrenor
{
    public class AntrenorEkleViewModel
    {
        // === ANTRENÖR ===
        [Required(ErrorMessage = "Antrenör adı ve soyadı boş bırakılamaz.")]
        public string AdSoyad { get; set; }

        public string UzmanlikAlani { get; set; }
        public string Biyografi { get; set; }

        // === MÜSAİTLİK ===
        [Required(ErrorMessage = "En az bir gün seçmelisiniz.")]
        public List<string> Gunler { get; set; } = new();

        [Required]
        public TimeSpan BaslangicSaat { get; set; }

        [Required]
        public TimeSpan BitisSaat { get; set; }
    }
}