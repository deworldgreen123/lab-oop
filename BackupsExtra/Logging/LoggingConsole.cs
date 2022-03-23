using System;

namespace BackupsExtra.Logging
{
    public class LoggingConsole : ILogging
    {
        public void Logging(string report)
        {
            Console.WriteLine(report);
        }
    }
}