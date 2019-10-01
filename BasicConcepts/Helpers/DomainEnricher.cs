using System;
using System.Collections.Generic;
using System.Text;
using Serilog.Core;
using Serilog.Events;

namespace Console
{
    public class DomainEnricher : ILogEventEnricher
    {
        public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
        {
            logEvent.AddPropertyIfAbsent(propertyFactory.CreateProperty(
                "Domain", GetDomain()));
        }

        private string GetDomain()
        {
            var domain = new Random();
            switch (domain.Next(1, 4))
            {
                case 1:
                    return "USA";
                case 2:
                    return "China";
                case 3:
                    return "Greenland";
                case 4:
                    return "Poland";
                default:
                    return "Unknown";
            }
        }
    }
}
