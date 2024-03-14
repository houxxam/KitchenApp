namespace Repas.Models
{
    public class RepasServiceDTO
    {
        public int Id { get; set; }
        public string ServiceName { get; set; }
        public string TypeRepasName { get; set; }
        public int? TotalRapas { get; set; }
        public Destination Destination { get; set; }
    }
}
