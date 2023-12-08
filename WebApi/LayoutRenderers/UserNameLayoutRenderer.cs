using NLog;
using NLog.LayoutRenderers;
using System.Text;

namespace WebApi.LayoutRenderers
{
    public class UserNameLayoutRenderer : LayoutRenderer
    {
        protected override void Append(StringBuilder builder, LogEventInfo logEvent)
        {
            var userName = logEvent.Properties["userName"];
            builder.Append(userName);
        }
    }
}
