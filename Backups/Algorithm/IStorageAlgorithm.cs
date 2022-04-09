using System.Collections.Generic;
using Backups.Backup;
using Ionic.Zip;

namespace Backups.Algorithm
{
    public interface IStorageAlgorithm
    {
        List<ZipFile> CreateStorage(List<JopObject> files, string path, string pointId);
    }
}