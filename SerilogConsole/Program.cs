using log4net;
using log4net.Config;
using Serilog;
using Serilog.Sinks.File;
using Serilog.Sinks.RollingFile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerilogConsole
{
    class Program
    {

        static void Main(string[] args)
        {
            try
            {
                LogSimpleMessage();
                //LogException();
                //LogObject();
            }
            catch (Exception ex)
            {
                Console.WriteLine("{0} - {1}", ex.GetType(), ex.Message);
                Console.WriteLine(ex.StackTrace ?? String.Empty);
            }
            finally
            {
                Console.WriteLine();
                Console.WriteLine("...");
                Console.ReadKey();
            }
        }

        private static void LogException()
        {
            try
            {
                throw new ApplicationException("Exceptions happen!");
            }
            catch (Exception exception)
            {
                Logger4Net.Error($"A {exception.GetType()} exception", exception);
                Serilogger.Error(exception, $"A {exception.GetType()} exception");
                Serilogger.Debug("{@Exception}", exception);
            }
        }

        private static void LogObject()
        {
            var thing = new Thing()
            {
                Color = "Red",
                Height = 23.5,
                Id = 1,
                Name = "Thing1"
            };
            Logger4Net.Info($"thing: {thing}");
            Serilogger.Information("{@Thing}", thing);
        }

        private static void LogSimpleMessage()
        {
            string message = "There are many messages but this one is mine!";

            Logger4Net.Info(message);
            Logger4Net.Debug(message);
            Logger4Net.Warn(message);
            Logger4Net.Error(message);
            Logger4Net.Fatal(message);

            Serilogger.Information(message);
            Serilogger.Debug(message);
            Serilogger.Warning(message);
            Serilogger.Error(message);
            Serilogger.Fatal(message);
        }

        #region Plumbing

        static Program()
        {
            Logger4Net = LogManager.GetLogger("log4net_logger");
            XmlConfigurator.Configure();

            Serilogger = new LoggerConfiguration()
                            .MinimumLevel.Verbose()     // default is Information level
                            .WriteTo.ColoredConsole()
                            .WriteTo.RollingFile(".\\Serilog.log")
                            .CreateLogger();
        }

        private static readonly ILog Logger4Net;
        private static readonly ILogger Serilogger;

        #endregion
    }
}
