using System;
using System.Collections.Generic;
using System.Linq;
using Backups.Algorithm;
using Backups.Backup;
using BackupsExtra.Logging;

namespace BackupsExtra.BackupJopExtra
{
    public class BackupJopExtra
    {
        private DateTime _timeLimit;
        private int _valueLimit;

        public BackupJopExtra(string path, IStorageAlgorithm algorithm, ILogging logging)
        {
            Jop = new BackupJop(path, algorithm);
            Logging = logging;
            _timeLimit = DateTime.Now;
            _valueLimit = 0;
            Repository = new RepositoryExtra();
        }

        public BackupJop Jop { get; }
        public ILogging Logging { get; }
        public RepositoryExtra Repository { get; }

        public void ClearingValueLimit()
        {
            var ans = new LimitValue(_valueLimit);
            ans.Clearing(this);
        }

        public void ClearingTimeLimit()
        {
            var ans = new LimitTime(_timeLimit);
            ans.Clearing(this);
        }

        public void ClearingAllHybridLimit()
        {
            var ans = new AllHibridLimit(_valueLimit, _timeLimit);
            ans.Clearing(this);
        }

        public void ClearingNotAllHybridLimit()
        {
            var ans = new NotAllHibridLimit(_valueLimit, _timeLimit);
            ans.Clearing(this);
        }

        public void SetTimeLimit(DateTime newTimeLimit)
        {
            _timeLimit = newTimeLimit;
            Logging.Logging("set time limit");
        }

        public void SetNumberLimit(int newNumberLimit)
        {
            _valueLimit = newNumberLimit;
            Logging.Logging("set value limit");
        }

        public void AddFile(string filePath, string fileName)
        {
            Jop.AddObject(filePath, fileName);
            Logging.Logging("file add");
        }

        public void RemoveFile(string filePath, string fileName)
        {
            Jop.RemoveObject(filePath, fileName);
            Logging.Logging("file remove");
        }

        public void SaveRestorePoint()
        {
            Jop.Save();
            Logging.Logging("Restore point created");
        }

        public void RemoveVirtualRestorePoints()
        {
            Jop.VirtualRestorePoints.Clear();
            Logging.Logging("Remove Virtual Restore Point");
        }

        public void MakeVirtualRestorePoint()
        {
            Jop.MakeVirtualRestorePoint();
            Logging.Logging("virtual Restore Point created");
        }

        public void Merge()
        {
            if (Jop.RestorePoints.Count < 2)
            {
                return;
            }

            if (!(Jop.Algorithm is SingleStorage))
            {
                var fileTrans = new List<JopObject>();
                var fileRemove = new List<JopObject>();
                foreach (JopObject jopObject in Jop.RestorePoints[^2].Files)
                {
                    if (!Jop.RestorePoints.Last().Files.Contains(jopObject))
                    {
                        Jop.RestorePoints.Last().Files.Add(jopObject);
                        Jop.RestorePoints[^2].Files.Remove(jopObject);
                        fileTrans.Add(jopObject);
                    }
                    else
                    {
                        fileRemove.Add(jopObject);
                    }
                }

                foreach (JopObject jopObject in fileTrans)
                {
                    Repository.TransFile(this, Jop.RestorePoints[^2].IdPoint, Jop.RestorePoints.Last().IdPoint, jopObject);
                }

                foreach (JopObject jopObject in fileRemove)
                {
                    Repository.RemoveFile(this, Jop.RestorePoints[^2], jopObject);
                }
            }

            Jop.RestorePoints.RemoveAt(Jop.RestorePoints.Count - 2);
            Logging.Logging("merge");
        }
    }
}