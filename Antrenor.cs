namespace spor_sitesi.Models
{
    public class Antrenor
    {
        public int Id { get; set; }

        public string Ad { get; set; }
        public string Soyad { get; set; }

        public string UzmanlikAlani { get; set; }

        public string Telefon { get; set; }

        public string Email { get; set; }

        // Şimdilik basit bir açıklama alanı: "Hafta içi 10:00-18:00" gibi
        public string MusaitlikBilgisi { get; set; }
    }
}
