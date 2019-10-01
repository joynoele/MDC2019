using System;
using System.Collections.Generic;
using System.Text;
using Serilog;

namespace ConsoleReference
{
    public static class Messages
    {
        public static void LogSimpleTestMessages()
        {
            Log.Verbose("hello verbose!");
            Log.Debug("hello debug!");
            Log.Information("hello information!");
            Log.Warning("hello warning!");
            Log.Error("hello error!");
            Log.Fatal("hello fatal!");
        }

        public static double DoMath()
        {
            var numerator = 5;
            var denominator = 0;
            double result = 0;
            try
            {
                result = numerator / denominator;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Uh-oh! Denominator must be non-zero value. Setting result to *almost* zero.");
                result = 0.00000001;
            }

            Log.Information("Division result is: {result}", result);
            return result;
        }

        public static void PrintObject(object obj)
        {
            Log.Information($"String interpolation: $\"{{obj}}\" = {obj}");
            Log.Information("Serilog interpolation: \"{{obj}}\" = {param}", obj);
            Log.Information("Deserializaion: \"{{@obj}}\" = {@param}", obj);
        }
    }
}
