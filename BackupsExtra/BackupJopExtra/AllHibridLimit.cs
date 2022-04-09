using System;
using System.Collections.Generic;
using System.Linq;
using Backups.Backup;

namespace BackupsExtra.BackupJopExtra
{
    public class AllHibridLimit : ILimit
    {
        public AllHibridLimit(int maxCount, DateTime time)
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

            return Math.Max(restorePoints.Count - MaxCount, count);
        }
    }
}