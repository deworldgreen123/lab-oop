using System;
using System.Runtime.InteropServices;
using System.Text;
using Backups.Algorithm;
using Backups.Backup;

namespace Backups
{
    internal static class Program
    {
        private static void Main()
        {
            IStorageAlgorithm algorithm = new SingleStorage();
            var backup = new BackupJop(@"C:\Users\dewor\Desktop", algorithm);
            backup.AddObject(@"C:\Users\dewor\Desktop", "A");
            backup.AddObject(@"C:\Users\dewor\Desktop", "B");
            backup.Save();
            backup.RemoveObject(@"C:\Users\dewor\Desktop", "B");
            backup.Save();
        }
    }
}
