using System;
using System.ComponentModel.DataAnnotations.Schema;


namespace spor_sitesi.Models
{
    public class Randevu
    {
        public int Id { get; set; }

        public DateTime Tarih { get; set; }

        public TimeSpan BaslangicSaati { get; set; }

        // İlişki: Hizmet
        public int HizmetId { get; set; }
        public Hizmet Hizmet { get; set; }

        // İlişki: Antrenör
        public int AntrenorId { get; set; }
        public Antrenor Antrenor { get; set; }

        // İlişki: Üye (Identity kullanıcı Id'si)
        public string UyeId { get; set; }   // AspNetUsers tablosundaki Id ile eşleşecek

        public bool OnaylandiMi { get; set; }

        public bool IptalEdildiMi { get; set; }

        public string Not { get; set; }

        public decimal? Ucret { get; set; }
    }
}
