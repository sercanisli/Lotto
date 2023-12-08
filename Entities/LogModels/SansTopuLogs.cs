using System.Text.Json;

namespace Entities.LogModels
{
    public class SansTopuLogs
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public int RandomPlusNumber { get; set; }
        public List<int> RandomNumbers { get; set; }
        public SansTopuLogs()
        {
            RandomNumbers = new List<int>();
        }

        public override string ToString() => JsonSerializer.Serialize(this);

    }
}
