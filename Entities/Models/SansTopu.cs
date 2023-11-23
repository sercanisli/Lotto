namespace Entities.Models
{
    public class SansTopu : Lotto
    {
        public List<int> PlusNumbers { get; set; }

        public SansTopu()
        {
            PlusNumbers = new List<int>();
        }
    }
}
