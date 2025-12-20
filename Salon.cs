using System.Collections.Generic;

namespace spor_sitesi.Models
{
    public class Salon
    {
        public int Id { get; set; }

        public string Ad { get; set; }

        public string Adres { get; set; }

        public string Telefon { get; set; }

        // Bir salonda birden fazla hizmet olabilir
        public ICollection<Hizmet> Hizmetler { get; set; }
    }
}
