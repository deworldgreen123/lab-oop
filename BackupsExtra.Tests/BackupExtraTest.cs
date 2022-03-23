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
        
        [SetUp]
        public void Setup()
        { 
            IStorageAlgorithm algorithm = new SplitStorage();
            _backup = new BackupJopExtra.BackupJopExtra(".", algorithm, new LoggingConsole());
        }
        
        [Test]
        public void CheckValueLimit()
        {
            const string pathFile = "./../../..";
            _backup.AddFile(pathFile, "A");
            _backup.AddFile(pathFile, "B");
            _backup.MakeRestorePointExtra();
            
            Assert.AreEqual(_backup.Jop.RestorePoints.Count, 1);
            Assert.AreEqual(_backup.Jop.RestorePoints[0].Files.Count, 2);
            
            _backup.RemoveFile(pathFile,"B");
            _backup.MakeRestorePointExtra();
            _backup.SetNumberLimit(1);
            _backup.ClearingValueLimit();
            
            Assert.True(1 == _backup.Jop.RestorePoints.Count);
        }

        [Test]
        public void CheckTimeLimit()
        {
            const string pathFile = "./../../..";
            _backup.AddFile(pathFile, "A");
            _backup.AddFile(pathFile, "B");
            _backup.MakeRestorePointExtra();
            
            Assert.AreEqual(_backup.Jop.RestorePoints.Count, 1);
            Assert.AreEqual(_backup.Jop.RestorePoints[0].Files.Count, 2);
            
            _backup.RemoveFile(pathFile, "B");
            _backup.MakeRestorePointExtra();
            _backup.SetTimeLimit(DateTime.Now);
            _backup.ClearingTimeLimit();
            
            Assert.True(1 == _backup.Jop.RestorePoints.Count);
        }

        [Test]
        public void CheckMerge()
        {
            const string pathFile = "./../../..";
            _backup.AddFile(pathFile, "A");
            _backup.AddFile(pathFile, "B");
            _backup.MakeRestorePointExtra();
            _backup.RemoveFile(pathFile, "B");
            _backup.MakeRestorePointExtra();

            _backup.Merge();
            Assert.True(1 == _backup.Jop.RestorePoints.Count);
        }
    }
}