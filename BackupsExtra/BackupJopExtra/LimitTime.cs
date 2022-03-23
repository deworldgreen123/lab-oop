using System;
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

            return count;
        }
    }
}