namespace Entities.Models
{
    public class GetRandomLogs
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public List<int> RandomNumbers { get; set; }


        public GetRandomLogs()
        {
            RandomNumbers = new List<int>();
        }
    }
}
