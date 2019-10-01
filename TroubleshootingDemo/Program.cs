using System;
using System.Threading;
using Serilog;
using Serilog.Context;
using Serilog.Events;

namespace TroubleshootDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            ConfigLogger();
            TroubleshootingDemo();
        }

        static void ConfigLogger()
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Verbose()
                .Enrich.FromLogContext()
                .WriteTo.Seq("http://localhost:5341", restrictedToMinimumLevel:LogEventLevel.Information)
                .WriteTo.MongoDB("mongodb://localhost/MDC2019", collectionName: "troubleshootdemo", restrictedToMinimumLevel: LogEventLevel.Information)
                .WriteTo.File("C:\\log\\TroubleshootExample.txt", restrictedToMinimumLevel: LogEventLevel.Debug)
                .WriteTo.ColoredConsole(
                    restrictedToMinimumLevel: LogEventLevel.Information,
                    outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss} [{Thread}:{Level:u3}] {Message:lj}{NewLine}{Exception}")
                .CreateLogger();
        }

        static void TroubleshootingDemo()
        {

            Thread evezino = new Thread(delegate ()
            {
                RunXTimes(40);
            })
            { Name = "evezino" };

            Thread nblumhardt = new Thread(delegate ()
            {
                RunXTimes(35);
            })
            { Name = "nblumhardt" };

            Thread jatwood = new Thread(delegate ()
            {
                RunXTimes(25);
            })
            { Name = "jerdahl" };


            evezino.Start();
            nblumhardt.Start();
            jatwood.Start();
        }

        private static void RunXTimes(int runTimes)
        {
            using (LogContext.PushProperty("Thread", Thread.CurrentThread.Name))
            {
                var events = new CreateEvents();
                for (int i = 0; i < runTimes; i++)
                {
                    events.RunRandomQuery();
                }
            }
        }
    }
}
