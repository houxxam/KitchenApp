using System.ComponentModel.DataAnnotations.Schema;

namespace Repas.Models
{
    public class RepasService
    {
        public int Id { get; set; }
        public int? TotalRepas { get; set;}
        public int TypeRepasId { get; set;}
        public int ServiceId { get; set;}
        public Destination   destination { get; set; }
        public int DateFornitureId { get; set; }
        [ForeignKey("TypeRepasId")]
        public TypeRepas? TypeRepas { get; set; }
        [ForeignKey("ServiceId")]
        public Service? Service { get; set; }
        [ForeignKey("DateFornitureId")]
        public DateForniture? dateForniture { get; set; }

    }
}
