using System;

[assembly: log4net.Config.XmlConfigurator(Watch=true)]

namespace ConsoleApplication1
{
    internal class Program
    {
        // we copy-paste this method to every class we want to use the logger in
        // ALT: log= LogHelper.GetLogger(); this instead of Reflection gives full path to the file
        
        // The log file saves to ConsoleApplication1\bin\Debug\logs\log-file.txt
        private static readonly log4net.ILog log =
            log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public static void Main(string[] args)
        {
            Console.WriteLine("works");
            
            // Severity goes from Debug(Most specific) > Info > Warn > Error (handled exception) > Fatal (Crashed)
            log.Debug("Most specific log, for developers (lowest level)");
            log.Info("Maintenance: for example log that a user logged in successfully");
            log.Warn("Maintenance: User entered a wrong password 5 times for example");

            var i = 0;
            try
            {
                var x = 10 / i;
            }
            catch (DivideByZeroException ex)
            {
                //with ex, it will log the exception info as well
                log.Error("This is my error message", ex);
            }

            log.Fatal("Maintenance: Highest warning, program crashed for example");
            
        }
    }
}