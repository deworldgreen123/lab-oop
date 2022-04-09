using System;
using System.Collections.Generic;
using Backups.Backup;

namespace BackupsExtra.BackupJopExtra
{
    public class LimitTime : ILimit
    {
        public LimitTime(DateTime time)
        {
            Time = time;
        }

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

            return count;
        }
    }
}