using System.IO;
using Ionic.Zip;

namespace Backups.Backup
{
    public class Repository
    {
        public string CreateDirectory(string path)
        {
            string directory = path + "/Backup";
            Directory.CreateDirectory(directory);
            return directory;
        }
    }
}