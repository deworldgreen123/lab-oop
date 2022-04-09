using System;
using Backups.Algorithm;
using Backups.Backup;
using BackupsExtra.BackupJopExtra;
using BackupsExtra.Logging;
using NUnit.Framework;

namespace BackupsExtra.Tests
{
    public class BackupExtraTest
    {
        private BackupJopExtra.BackupJopExtra _backup;
        private string _pathFile = "";
        
        [SetUp]
        public void Setup()
        { 
            IStorageAlgorithm algorithm = new SplitStorage();
            _backup = new BackupJopExtra.BackupJopExtra(".", algorithm, new LoggingConsole());
            
            _pathFile = "./../../..";
            _backup.AddFile(_pathFile, "file1.txt");
            _backup.AddFile(_pathFile, "file2.txt");
            _backup.SaveRestorePoint();
        }
        
        [Test]
        public void CheckValueLimit()
        {
            Assert.AreEqual(_backup.Jop.RestorePoints.Count, 1);
            Assert.AreEqual(_backup.Jop.RestorePoints[0].Files.Count, 2);
            
            _backup.RemoveFile(_pathFile,"file2.txt");
            _backup.SaveRestorePoint();
            _backup.SetNumberLimit(1);
            _backup.ClearingValueLimit();
            
            Assert.True(1 == _backup.Jop.RestorePoints.Count);
        }

        [Test]
        public void CheckMerge()
        {
            _backup.RemoveFile(_pathFile, "file2.txt");
            _backup.SaveRestorePoint();

            _backup.Merge();
            Assert.True(1 == _backup.Jop.RestorePoints.Count);
        }
    }
}