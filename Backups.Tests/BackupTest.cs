using Backups.Algorithm;
using Backups.Backup;
using NUnit.Framework;

namespace Backups.Tests
{
    public class BackupTest
    {
        private BackupJop _backup;
        
        [SetUp]
        public void Setup()
        { 
            IStorageAlgorithm algorithm = new SplitStorage();
            _backup = new BackupJop(".", algorithm);
        }

        [Test]
        public void test1()
        {
            const string pathFile = "./../../..";
            _backup.AddObject(pathFile, "file1.txt");
            _backup.AddObject(pathFile, "file2.txt");
            _backup.MakeVirtualRestorePoint();
            
            Assert.AreEqual(_backup.VirtualRestorePoints.Count, 1);
            Assert.AreEqual(_backup.VirtualRestorePoints[0].Files.Count, 2);
            
            _backup.RemoveObject(pathFile,"file2.txt");
            _backup.MakeVirtualRestorePoint();
            
            Assert.AreEqual(_backup.VirtualRestorePoints.Count, 2);
            Assert.AreEqual(_backup.VirtualRestorePoints[1].Files.Count, 1);
            
            Assert.True(_backup.CheckVirtualRestorePoint(1, 2));
            Assert.True(_backup.CheckVirtualRestorePoint(2, 1));
        }
    }
}