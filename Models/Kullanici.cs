using System.ComponentModel.DataAnnotations;

namespace Spor_web_sitesi.Models
{
    // Bu dosya Models klasörüne eklenmelidir.
    public class Kullanici
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "E-posta adresi zorunludur.")]
        [EmailAddress(ErrorMessage = "Geçerli bir e-posta giriniz.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Şifre zorunludur.")]
        [DataType(DataType.Password)]
        public string Sifre { get; set; }

        public string AdSoyad { get; set; }

        // Kullanıcının yetkisi: "Admin" veya "Uye"
        public string Rol { get; set; } = "Uye";
    }
}