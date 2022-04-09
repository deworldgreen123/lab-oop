using System.IO;
using Ionic.Zip;

namespace Backups.Backup
{
    public class Repository
    {
        public static string CreateDirectory(string path)
        {
            string directory = path + "/backup";
            Directory.CreateDirectory(directory);
            return directory;
        }
    }
}