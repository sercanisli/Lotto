namespace WebApi.Models
{
    public class SuperLoto
    {
        public int Id { get; set; }
        public List<int> Numbers { get; set; }

        public SuperLoto()
        {
            Numbers = new List<int>();
        }
    }
}
