using System.ComponentModel.DataAnnotations.Schema;
namespace spor_sitesi.Models

{
    public class Hizmet
    {
        public int Id { get; set; }
        public string Ad { get; set; }
        public int SureDakika { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Ucret { get; set; }

        public int MaxKisiSayisi { get; set; }

        public int SalonId { get; set; }
        public Salon Salon { get; set; }
    }

}
