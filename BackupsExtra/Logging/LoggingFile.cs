using System.IO;

namespace BackupsExtra.Logging
{
    public class LoggingFile : ILogging
    {
        private string _pathOfLog;

        public LoggingFile(string pathOfLog)
        {
            File.Create(pathOfLog);
            _pathOfLog = pathOfLog;
        }

        public void Logging(string report)
        {
            File.AppendAllText(_pathOfLog, report);
        }
    }
}