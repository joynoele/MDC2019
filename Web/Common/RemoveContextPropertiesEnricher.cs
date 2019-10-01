using Serilog.Core;
using Serilog.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Common
{
    public class RemoveContextPropertiesEnricher : ILogEventEnricher
    {
        public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
        {
            logEvent.RemovePropertyIfPresent("ActionId");
            logEvent.RemovePropertyIfPresent("RequestId");
            logEvent.RemovePropertyIfPresent("ActionName");
            logEvent.RemovePropertyIfPresent("SpanId");
            logEvent.RemovePropertyIfPresent("TraceId");
            logEvent.RemovePropertyIfPresent("ParentId");
        }
    }
}
