using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.ComponentModel.DataAnnotations;

namespace Spor_web_sitesi.Models
{
    // Üyelerin aldığı randevuların detaylarını tutan sınıf
    public class Randevu
    {
        public int Id { get; set; }

        [Required]
        public string UyeId { get; set; } // Giriş yapan üyenin kimliği

        [Required(ErrorMessage = "Lütfen bir antrenör seçin.")]
        [Display(Name = "Antrenör")]
        public int AntrenorId { get; set; }
        [ValidateNever]
        public virtual Antrenor Antrenor { get; set; } // Antrenör bilgilerine ulaşmak için

        [Required(ErrorMessage = "Lütfen bir hizmet seçin.")]
        [Display(Name = "Hizmet")]
        public int HizmetId { get; set; }
        [ValidateNever]
        public virtual Hizmet Hizmet { get; set; } // Hizmet bilgilerine ulaşmak için

        [Required(ErrorMessage = "Randevu tarihi ve saati seçilmelidir.")]
        [Display(Name = "Randevu Zamanı")]
        public DateTime RandevuTarihi { get; set; }

        [Display(Name = "Randevu Durumu")]
        public string Durum { get; set; } = "Beklemede"; // Beklemede, Onaylandı, Reddedildi

        public DateTime OlusturmaTarihi { get; set; } = DateTime.Now;
    }
}