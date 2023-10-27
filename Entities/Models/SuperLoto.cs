using System.Globalization;

namespace Entities.Models
{
    public class SuperLoto
    {
        public int Id { get; set; }
        public List<int> Numbers { get; set; }
        public DateTime? Date { get; set; }

        public SuperLoto()
        {
            Numbers = new List<int>();
        }

        public SuperLoto(DateTime date) : this()
        {
            Date = date;
        }

        public SuperLoto(string dateString) : this()
        {
            if (!string.IsNullOrWhiteSpace(dateString))
            {
                DateTime parsedDate;
                if (DateTime.TryParseExact(dateString, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out parsedDate))
                {
                    Date = parsedDate;
                }
                else
                {
                    throw new ArgumentException("Invalid date format.", nameof(dateString));
                }
            }
        }
    }
}
