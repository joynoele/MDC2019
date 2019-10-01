using System;
using System.Collections.Generic;
using System.Text;
using Serilog;
using Serilog.Context;

namespace TroubleshootDemo
{
    public class CreateEvents
    {
        private Random rand;
        public CreateEvents()
        {
            rand = new Random();
        }

        public void RunRandomQuery()
        {
            var query = rand.Next(1, 6);
            switch (query)
            {
                case 1: Query1(); break;
                case 2: Query2(); break;
                case 3: Query3(); break;
                case 4: Query4(); break;
                case 5: Query5(); break;
            }
        }

        private void Query1()
        {
            using (LogContext.PushProperty("query", "Query1"))
            {
                var queryTime = rand.Next(1, 2000);
                ExecuteQuery(queryTime);
                Log.Logger.Debug("");
                Log.Logger.Information("{query} took {ms}ms to run", "Query1", queryTime);
            }
        }
        private void Query2()
        {
            using (LogContext.PushProperty("query", "Query2"))
            {
                var queryTime = rand.Next(1, 3000);
                ExecuteQuery(queryTime);
                Log.Logger.Debug("");
                Log.Logger.Information("{query} took {ms}ms to run", "Query2", queryTime);
            }
        }
        private void Query3()
        {
            using (LogContext.PushProperty("query", "Query3"))
            {
                var queryTime = rand.Next(1, 4000);
                ExecuteQuery(queryTime);
                Log.Logger.Debug("");
                Log.Logger.Information("{query} took {ms}ms to run", "Query3", queryTime);
            }
        }
        private void Query4()
        {
            using (LogContext.PushProperty("query", "Query4"))
            {
                var queryTime = rand.Next(1000, 5000);
                ExecuteQuery(queryTime);
                Log.Logger.Debug("");
                Log.Logger.Information("{query} took {ms}ms to run", "Query4", queryTime);
            }
        }
        private void Query5()
        {
            using (LogContext.PushProperty("query", "Query5"))
            {
                var queryTime = rand.Next(3000, 5000);
                ExecuteQuery(queryTime);
                Log.Logger.Debug("");
                Log.Logger.Information("{query} took {ms}ms to run", "Query5", queryTime);
            }
        }

        private void ExecuteQuery(int sleeptime)
        {
            System.Threading.Thread.Sleep(sleeptime);
            if (sleeptime > 2500 && sleeptime < 3500)
                Log.Logger.Error(new Exception($"Error Id: {Guid.NewGuid()}"), "\tQuery execution failed with execution time {sec}s", sleeptime / 1000.0);
        }
    }
}
