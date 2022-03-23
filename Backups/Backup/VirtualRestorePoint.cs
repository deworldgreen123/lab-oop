using System;
using System.Collections.Generic;
using System.IO;

namespace Backups.Backup
{
    public class VirtualRestorePoint
    {
        public VirtualRestorePoint(List<JopObject> filesToSave)
        {
            Date = DateTime.Now;
            Files = new List<string>();
            foreach (JopObject file in filesToSave)
            {
                Files.Add(File.ReadAllText(file.FullName));
            }
        }

        public DateTime Date { get; set; }
        public List<string> Files { get; }
    }
}