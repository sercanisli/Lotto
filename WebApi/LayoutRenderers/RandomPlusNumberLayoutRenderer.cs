using NLog;
using NLog.LayoutRenderers;
using System.Text;

namespace WebApi.LayoutRenderers
{
    public class RandomPlusNumberLayoutRenderer : LayoutRenderer
    {
        protected override void Append(StringBuilder builder, LogEventInfo logEvent)
        {
            var plusNumber = logEvent.Properties["plusNumber"];
            builder.Append(plusNumber);
        }
    }
}
