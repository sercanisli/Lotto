namespace Entities.LogModels
{
    public class LogModels
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public List<int> RandomNumbers { get; set; }
        public LogModels()
        {
            RandomNumbers = new List<int>();
        }
    }
}
