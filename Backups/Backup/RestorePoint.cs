using System;
using System.Collections.Generic;
using Backups.Algorithm;
using Ionic.Zip;

namespace Backups.Backup
{
    public class RestorePoint
    {
        public RestorePoint(List<JopObject> files, string path, string idPoint, List<ZipFile> storage)
        {
            RestorePointCreateDate = DateTime.Now;
            Files = files;
            Storage = storage;
            IdPoint = idPoint;
        }

        public string IdPoint { get; }
        public DateTime RestorePointCreateDate { get; }
        public List<JopObject> Files { get; }
        public List<ZipFile> Storage { get; }
    }
}