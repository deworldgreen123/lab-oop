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

        public BackupJop Jop { get; set; }
        public ILogging Logging { get; set; }
        public RepositoryExtra Repository { get; set; }

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
        }

        public void SetNumberLimit(int newNumberLimit)
        {
            _valueLimit = newNumberLimit;
        }

        public void RecoveringToOriginalLocation(JopObject file)
        {
            Repository.FileRecovery(file.FullName);
            Logging.Logging("the file has been restored");
        }

        public void RecoveringToDifferentLocation(JopObject file, string path)
        {
            Repository.FileRecovery(file.FullName, path);
            Logging.Logging("the file has been restored");
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

        public void MakeRestorePointExtra()
        {
            Jop.Save();
            Logging.Logging("Restore point created");
        }

        public void RemoveVirtualMemory()
        {
            Jop.JopObjects.Clear();
        }

        public void MakeVirtualRestorePointExtra()
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
                foreach (var jopObject in Jop.RestorePoints[^2].Files)
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