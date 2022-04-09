using System;
using System.IO;

namespace BackupsExtra.Logging
{
    public class LoggingFile : ILogging
    {
        private string _pathOfLog;

        public LoggingFile(string path)
        {
            string pathOfLog = path + DateTime.Now.ToShortDateString() + "_Backup.txt";
            File.Create(pathOfLog);
            _pathOfLog = pathOfLog;
        }

        public void Logging(string report)
        {
            File.AppendAllText(_pathOfLog, report);
        }
    }
}