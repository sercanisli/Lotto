using NLog;
using NLog.LayoutRenderers;
using System.Text;

namespace WebApi.LayoutRenderers
{
    public class RandomNumbersLayoutRenderer : LayoutRenderer
    {
        protected override void Append(StringBuilder builder, LogEventInfo logEvent)
        {
            var randomNumbers = logEvent.Properties["randomNumbers"];
            builder.Append(randomNumbers);
        }
    }
}
