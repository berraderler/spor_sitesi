using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Spor_web_sitesi.Models
{
    // Salonun sunduğu hizmetleri (Pilates, Fitness vb.) tutan sınıf
    public class Hizmet
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Hizmet adı zorunludur.")]
        [Display(Name = "Hizmet Adı")]
        public string Ad { get; set; }

        [Display(Name = "Süre (Dakika)")]
        public int SureDakika { get; set; } // Örn: 60

        [Display(Name = "Ücret (TL)")]
        public decimal Ucret { get; set; }

        // İlişki: Bu hizmet türüne ait birçok randevu olabilir
        [ValidateNever]
        public virtual ICollection<Randevu> Randevular { get; set; }
    }
}