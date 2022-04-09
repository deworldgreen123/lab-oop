using System;
using System.Collections.Generic;
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

        protected override int GetLimit(List<RestorePoint> restorePoints)
        {
            int count = 0;
            foreach (RestorePoint restorePoint in restorePoints)
            {
                if (DateTime.Compare(restorePoint.RestorePointCreateDate, Time) <= 0)
                {
                    count++;
                }
            }

            return Math.Min(restorePoints.Count - MaxCount, count);
        }
    }
}