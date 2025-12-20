using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Spor_web_sitesi.Models
{
    // Antrenörlerin (Eğitmenlerin) bilgilerini tutan sınıf
    public class Antrenor
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Antrenör adı ve soyadı boş bırakılamaz.")]
        [Display(Name = "Ad Soyad")]
        public string AdSoyad { get; set; }

        [Display(Name = "Uzmanlık Alanı")]
        public string UzmanlikAlani { get; set; } // Örn: Kilo Verme, Kas Geliştirme

        [Display(Name = "Hakkında")]
        public string Biyografi { get; set; }

        // İlişkiler: Bir antrenörün birden fazla randevusu ve müsaitlik saati olabilir
        [ValidateNever]
        public virtual ICollection<Randevu> Randevular { get; set; }
        public virtual ICollection<AntrenorMusaitlik> Musaitlikler { get; set; }
    }
}