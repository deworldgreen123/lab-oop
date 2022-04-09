using System.Collections.Generic;
using Backups.Backup;

namespace BackupsExtra.BackupJopExtra
{
    public abstract class ILimit
    {
        public BackupJopExtra Clearing(BackupJopExtra backup)
        {
            for (int i = 0; i < GetLimit(backup.Jop.RestorePoints); i++)
            {
                backup.Repository.RemoveRestorePoint(backup, i, backup.Jop.Algorithm);
            }

            backup.Logging.Logging("restore limit");
            return backup;
        }

        protected virtual int GetLimit(List<RestorePoint> backup)
        {
            throw new System.NotImplementedException();
        }
    }
}