using System;
using System.Collections.Generic;
using System.IO;

namespace Backups.Backup
{
    public class VirtualRestorePoint
    {
        public VirtualRestorePoint(List<JopObject> files)
        {
            Date = DateTime.Now;
            Files = new List<string>();
            foreach (JopObject file in files)
            {
                Files.Add(File.ReadAllText(file.FullName));
            }
        }

        public DateTime Date { get; }
        public List<string> Files { get; }
    }
}