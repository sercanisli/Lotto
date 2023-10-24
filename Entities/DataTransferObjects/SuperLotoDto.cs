using System.Runtime.Serialization;

namespace Entities.DataTransferObjects
{
    [Serializable]
    public record SuperLotoDto
    {
        public int Id { get; init; }
        public List<int> Numbers { get; init; }

        public SuperLotoDto()
        {
            Numbers = new List<int>();
        }
    }
}
