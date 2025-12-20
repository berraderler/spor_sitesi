namespace Spor_web_sitesi.Models
{
    public class Analiz
    {
        public Guid Id { get; set; }
        public DateTime AnalizTarihi { get; set; } = DateTime.Now;
        public string UyeId { get; set; }
        public decimal Kilo { get; set; }
        public int BoyCm { get; set; }
        public string AnalizOzeti { get; set; }
    }
}
