namespace Repas.Models
{
    public class TypeRepasDTO
    {
        public TypeRepasDTO(string name, int count)
        {
            Name = name;
            Count = count;
        }

        public string Name { get; set; }
        public int Count { get; set; }


    }
}
