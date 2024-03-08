namespace Repas.Models
{
    public class TypeRepas
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public Destination Destination { get; set; }
    }
}
