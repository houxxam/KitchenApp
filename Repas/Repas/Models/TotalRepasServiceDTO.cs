namespace Repas.Models
{
    public class TotalRepasServiceDTO
    {
        public int DateId { get; set; }
        public DateTime DateRepas { get; set; }
        public List<TypeRepasDTO> TotalRepasByType { get; set; }
       

    }
}
