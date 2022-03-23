namespace BackupsExtra.BackupJopExtra
{
    public abstract class ILimit
    {
        public BackupJopExtra Clearing(BackupJopExtra backup)
        {
            for (int i = 0; i < GetLimit(backup); i++)
            {
                backup.Repository.RemoveRestorePoint(backup, i, backup.Jop.Algorithm);
            }

            backup.Logging.Logging("restore limit");
            return backup;
        }

        protected virtual int GetLimit(BackupJopExtra backup)
        {
            throw new System.NotImplementedException();
        }
    }
}