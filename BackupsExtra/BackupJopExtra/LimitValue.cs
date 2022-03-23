using System;
using System.Linq;
using Backups.Backup;

namespace BackupsExtra.BackupJopExtra
{
    public class LimitValue : ILimit
    {
        public LimitValue(int maxCount)
        {
            MaxCount = maxCount;
        }

        private int MaxCount { get; set; }

        protected override int GetLimit(BackupJopExtra backup)
        {
            if (backup.Jop.RestorePoints.Count - MaxCount <= 0)
            {
                return 0;
            }

            return backup.Jop.RestorePoints.Count - MaxCount;
        }
    }
}