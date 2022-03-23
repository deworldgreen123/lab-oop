using System.Collections.Generic;
using Backups.Backup;
using Ionic.Zip;

namespace Backups.Algorithm
{
    public class SplitStorage : IStorageAlgorithm
    {
        public List<ZipFile> CreateStorage(List<JopObject> files, string path, string pointId)
        {
            var zipFiles = new List<ZipFile>();
            foreach (JopObject file in files)
            {
                string zipName = path + "/" + file.Name + "_" + pointId + ".zip";
                var zip = new ZipFile();
                zip.AddFile(file.FullName, "/");
                zip.Save(zipName);
                zipFiles.Add(zip);
            }

            return zipFiles;
        }
    }
}