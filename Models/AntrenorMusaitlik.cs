using System;
using System.ComponentModel.DataAnnotations;

namespace Spor_web_sitesi.Models
{
    // Antrenörlerin hangi gün ve saatlerde çalıştığını tutan sınıf
    public class AntrenorMusaitlik
    {
        public int Id { get; set; }

        [Required]
        public int AntrenorId { get; set; }
        public virtual Antrenor Antrenor { get; set; }

        [Required]
        [Display(Name = "Gün")]
        public string Gun { get; set; } // Örn: Pazartesi, Salı...

        [Required]
        [Display(Name = "Başlangıç Saati")]
        public TimeSpan BaslangicSaat { get; set; } // Örn: 09:00

        [Required]
        [Display(Name = "Bitiş Saati")]
        public TimeSpan BitisSaat { get; set; } // Örn: 17:00
    }
}