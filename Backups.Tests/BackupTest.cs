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
            _backup.AddObject(pathFile, "A");
            _backup.AddObject(pathFile, "B");
            _backup.Save();
            
            Assert.AreEqual(_backup.RestorePoints.Count, 1);
            Assert.AreEqual(_backup.RestorePoints[0].Files.Count, 2);
            
            _backup.RemoveObject(pathFile,"B");
            _backup.Save();
            
            Assert.AreEqual(_backup.RestorePoints.Count, 2);
            Assert.AreEqual(_backup.RestorePoints[0].Files.Count, 1);
            
            Assert.True(_backup.CheckRestorePoint(1, 2));
            Assert.True(_backup.IsFileHere(1, "./Backup/A_1.zip"));
            Assert.True(_backup.IsFileHere(1, "./Backup/B_1.zip"));
            Assert.True(_backup.CheckRestorePoint(2, 1));
            Assert.True(_backup.IsFileHere(2, "./Backup/A_2.zip"));
            Assert.False(_backup.IsFileHere(2, "./Backup/B_2.zip"));
        }
    }
}