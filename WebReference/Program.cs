using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

using Serilog;
using WebReference.Common;

namespace WebReference
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Basic
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Override("Microsoft.AspNetCore", Serilog.Events.LogEventLevel.Warning)
                .WriteTo.Debug()
                .CreateLogger();

            // Use in destructuring
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Override("Microsoft.AspNetCore", Serilog.Events.LogEventLevel.Warning)
                .Enrich.With<RemoveContextPropertiesEnricher>()
                .WriteTo.Debug()
                .WriteTo.Seq("http://localhost:5341")
                .CreateLogger();

            // Use with injected properties
            var secretRetriever = new SecretRetriever();
            var secrets = new Secrets(secretRetriever);
            var mongoConnectionString = $"mongodb://{secrets.MongoUsername}:{secrets.MongoPassword}/MDC2019"; 

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Override("Microsoft.AspNetCore", Serilog.Events.LogEventLevel.Warning)
                .Enrich.With<RemoveContextPropertiesEnricher>()
                .WriteTo.Debug()
                .WriteTo.Seq("http://localhost:5341")
                .WriteTo.MongoDB("mongodb://localhost/MDC2019", "trees") //(mongoConnectionString, "trees")
                .CreateLogger();

            // SelfLog
            //Serilog.Debugging.SelfLog.Enable(Console.Error);
            //Serilog.Debugging.SelfLog.WriteLine("Hello selflog!");

            //var file = File.CreateText(@"C:\log\TreeSelfLog.txt");
            //var writer = TextWriter.Synchronized(file);
            //Serilog.Debugging.SelfLog.Enable(msg =>
            //{
            //    System.Diagnostics.Debug.WriteLine(msg);
            //    writer.WriteLine(msg);
            //    writer.Flush();
            //});
            //Serilog.Debugging.SelfLog.WriteLine("Test the selflogger");

            //Log.Logger = new LoggerConfiguration()
            //    .MinimumLevel.Override("Microsoft.AspNetCore", Serilog.Events.LogEventLevel.Warning)
            //    .Enrich.With<RemoveContextPropertiesEnricher>()
            //    .WriteTo.Seq("http://localhost:5341", batchPostingLimit: 3, eventBodyLimitBytes: 60)
            //    .WriteTo.File(@"C:\log\ReadOnlyLog.txt") // Create a read-only file
            //    .CreateLogger();
            //var i = 0;
            //var bigmsg = "*";
            //do
            //{
            //    Log.Information("i={iteration}, {msg}", i, bigmsg);
            //    bigmsg = bigmsg + bigmsg;
            //} while (System.Text.ASCIIEncoding.Unicode.GetByteCount(bigmsg) < 100);

            try
            {
                Log.Information("Starting web example...");
                CreateHostBuilder(args).Build().Run();
            } 
            catch (Exception ex)
            {
                Log.Error(ex, "Host terminated unexpectely");
            }
            finally
            {
                Log.Information("Closing web example...");
                Log.CloseAndFlush();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                }).UseSerilog();
    }
}
