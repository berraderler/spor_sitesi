namespace spor_sitesi.Models
{
    public class AntrenorHizmet
    {
        public int Id { get; set; }

        public int AntrenorId { get; set; }
        public Antrenor Antrenor { get; set; }

        public int HizmetId { get; set; }
        public Hizmet Hizmet { get; set; }
    }
}
