using System;
using System.Collections.Generic;
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

        protected override int GetLimit(List<RestorePoint> restorePoints)
        {
            if (restorePoints.Count - MaxCount <= 0)
            {
                return 0;
            }

            return restorePoints.Count - MaxCount;
        }
    }
}