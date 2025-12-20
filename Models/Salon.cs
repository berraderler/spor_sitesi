using System.ComponentModel.DataAnnotations;

namespace Spor_web_sitesi.Models
{
    // Bu dosya veritabanındaki "Salonlar" tablosunu temsil eder.
    // Eğer bu dosya yoksa, Controller "Salon" kelimesini tanımaz ve hata verir.
    public class Salon
    {
        // Her kaydın benzersiz bir numarası olur.
        [Key]
        public int Id { get; set; }

        // [Required] -> Bu alanın doldurulması zorunludur demektir.
        [Required(ErrorMessage = "Salon adı boş bırakılamaz.")]
        [Display(Name = "Spor Salonu Adı")]
        public string Ad { get; set; }

        [Display(Name = "Adres")]
        public string Adres { get; set; }

        [Display(Name = "Telefon Numarası")]
        public string Telefon { get; set; }

        [Display(Name = "Çalışma Saatleri")]
        public string CalismaSaatleri { get; set; } // Örn: 09:00 - 22:00
    }
}