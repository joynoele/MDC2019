using System;
using System.Collections.Generic;
using Serilog;

namespace ConsoleReference
{
    class Program
    {
        static void Main(string[] args)
        {
            // multiple sinks
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Debug()
                .WriteTo.Console()
                .CreateLogger();

            // multi level minimum event
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Verbose()
                .WriteTo.Debug(restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Debug)
                .WriteTo.Console(restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Information)
                .CreateLogger();

            // sublogger
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Verbose()
                .WriteTo.Logger(i => i
                    .Filter.ByExcluding(x => x.Level > Serilog.Events.LogEventLevel.Warning)
                    .WriteTo.File(@"C:\log\goodtoknow.txt"))
                .WriteTo.Logger(e => e
                    .Filter.ByIncludingOnly(x => x.Level >= Serilog.Events.LogEventLevel.Warning)
                    .WriteTo.File(@"C:\log\issues.txt"))
                .CreateLogger();

            // template
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Debug(outputTemplate: "[{Level:u3}] {Message:lj}{NewLine}{Exception}")
                .WriteTo.Console(outputTemplate: "[{Timestamp:yyyy-MM-dd HH:mm:ss}] [{Level:u3}] {Message:lj}{NewLine}{Exception}")
                .CreateLogger();

            // enrichers & context (see demo context)
            Log.Logger = new LoggerConfiguration()
                .Enrich.WithMachineName() // serilog.enrichers.environment
                .Enrich.WithProperty("ENV", "prod")
                .Enrich.With<DomainEnricher>()
                .WriteTo.Debug()
                .WriteTo.Console(outputTemplate: "[{Timestamp:yyyy-MM-dd HH:mm:ss}] [{Domain}:{MachineName}:{ENV}:{Level:u3}] {Message:lj}{NewLine}{Exception}")
                .CreateLogger();

            // Lifecycles & structure - don't forget to flush!
            Log.Logger = new LoggerConfiguration()
            .Enrich.WithMachineName()
            .Enrich.WithProperty("ENV", "prod")
            .Enrich.With<DomainEnricher>()
            .WriteTo.Debug()
            .WriteTo.Console(outputTemplate: "[{Timestamp:yyyy-MM-dd HH:mm:ss}] [{Domain}:{MachineName}:{ENV}:{Level:u3}] {Message:lj}{NewLine}{Exception}")
            .WriteTo.Seq("http://localhost:5341")
            .CreateLogger();

            //Messages.LogSimpleTestMessages();
            //Log.CloseAndFlush();



            // structured log - don't forget to flush!
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .WriteTo.Seq("http://localhost:5341")
                .CreateLogger();

            Messages.DoMath();
            Messages.PrintObject(5);
            Messages.PrintObject(new List<string> { "apple", "banana", "cantalope" });
            Messages.PrintObject(new Tree(TreeType.Pine, 60));
            Log.CloseAndFlush();
        }
    }
}
