using System;
using System.Collections.Generic;
using Backups.Algorithm;
using Ionic.Zip;

namespace Backups.Backup
{
    public class RestorePoint
    {
        public RestorePoint(List<JopObject> filesToSave, string path, string idPoint, IStorageAlgorithm algorithm)
        {
            CreationTime = DateTime.Now;
            Files = filesToSave;
            ZipFiles = algorithm.CreateStorage(filesToSave, path, idPoint);
            IdPoint = idPoint;
        }

        public string IdPoint { get; set; }
        public DateTime CreationTime { get; set; }
        public List<JopObject> Files { get; set; }
        public List<ZipFile> ZipFiles { get; set; }
    }
}