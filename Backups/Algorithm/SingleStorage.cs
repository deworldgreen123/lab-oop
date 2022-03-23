using System.Collections.Generic;
using Backups.Backup;
using Ionic.Zip;

namespace Backups.Algorithm
{
    public class SingleStorage : IStorageAlgorithm
    {
        public List<ZipFile> CreateStorage(List<JopObject> files, string path, string pointId)
        {
            var zipFiles = new List<ZipFile>();
            var zip = new ZipFile();
            string zipName = path + "/RestorePoint" + pointId + ".zip";
            foreach (JopObject file in files)
            {
                zip.AddFile(file.FullName, "/");
            }

            zip.Save(zipName);
            zipFiles.Add(zip);
            return zipFiles;
        }
    }
}