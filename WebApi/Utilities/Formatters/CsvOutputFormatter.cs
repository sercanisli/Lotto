using Entities.DataTransferObjects;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Net.Http.Headers;
using System.Text;

namespace WebApi.Utilities.Formatters
{
    public class CsvOutputFormatter : TextOutputFormatter
    {
        public CsvOutputFormatter()
        {
            SupportedMediaTypes.Add(MediaTypeHeaderValue.Parse("text/csv"));
            SupportedEncodings.Add(Encoding.UTF8);
            SupportedEncodings.Add(Encoding.Unicode);
        }

        protected override bool CanWriteType(Type? type)
        {
            if (typeof(SuperLotoDto).IsAssignableFrom(type) || typeof(IEnumerable<SuperLotoDto>).IsAssignableFrom(type))
            {
                return base.CanWriteType(type);
            }
            return false;
        }

        private static void FormatCsv(StringBuilder buffer, SuperLotoDto superLotoDto)
        {
            buffer.AppendLine($"{superLotoDto.Id}, {ConvertListToString(superLotoDto.Numbers)}");
        }

        private static string ConvertListToString(List<int> numbers)
        {
            return string.Join(", ", numbers);
        }

        public override async Task WriteResponseBodyAsync(OutputFormatterWriteContext context, Encoding selectedEncoding)
        {
            var response = context.HttpContext.Response;
            var buffer = new StringBuilder();

            if (context.Object is IEnumerable<SuperLotoDto>)
            {
                foreach (var superLoto in (IEnumerable<SuperLotoDto>)context.Object)
                {
                    FormatCsv(buffer, superLoto);
                }
            }
            else
            {
                FormatCsv(buffer, (SuperLotoDto)context.Object);
            }
            await response.WriteAsync(buffer.ToString());
        }
    }
}
