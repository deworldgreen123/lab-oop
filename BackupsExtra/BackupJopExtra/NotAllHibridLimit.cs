using System;
using Backups.Backup;

namespace BackupsExtra.BackupJopExtra
{
    public class NotAllHibridLimit : ILimit
    {
        public NotAllHibridLimit(int maxCount, DateTime time)
        {
            MaxCount = maxCount;
            Time = time;
        }

        private int MaxCount { get; set; }
        private DateTime Time { get; set; }

        protected override int GetLimit(BackupJopExtra backup)
        {
            int count = 0;
            foreach (RestorePoint restorePoint in backup.Jop.RestorePoints)
            {
                if (DateTime.Compare(restorePoint.CreationTime, Time) <= 0)
                {
                    count++;
                }
            }

            return Math.Min(backup.Jop.RestorePoints.Count - MaxCount, count);
        }
    }
}