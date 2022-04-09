using System.Drawing;
using System.IO;

namespace Backups.Backup
{
    public class JopObject
    {
        public JopObject(string path = "", string fileName = "")
        {
            var backupFileInfo = new FileInfo(path + "/" + fileName);
            Name = backupFileInfo.Name;
            FullName = backupFileInfo.FullName;
            Path = path;
        }

        public string Path { get; set; }
        public string Name { get; set; }
        public string FullName { get; set; }
    }
}