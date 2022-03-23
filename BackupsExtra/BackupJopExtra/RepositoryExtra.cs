using System.IO;
using Backups.Algorithm;
using Backups.Backup;

namespace BackupsExtra.BackupJopExtra
{
    public class RepositoryExtra : Repository
    {
        public void RemoveRestorePoint(BackupJopExtra jop, int idPoint, IStorageAlgorithm algorithm)
        {
            if (algorithm is SingleStorage)
            {
                File.Delete(jop.Jop.Path + "/RestorePoint" + jop.Jop.RestorePoints[idPoint].IdPoint + ".zip");
            }
            else
            {
                foreach (JopObject file in jop.Jop.RestorePoints[idPoint].Files)
                {
                    File.Delete(jop.Jop.Path + "/" + file.Name + "_" + jop.Jop.RestorePoints[idPoint].IdPoint + ".zip");
                }
            }

            jop.Jop.RestorePoints.RemoveAt(idPoint);
        }

        public void FileRecovery(string fullname)
        {
            File.Create(fullname);
        }

        public void FileRecovery(string name, string path)
        {
            File.Create(path + "/" + name);
        }

        public void RemoveFile(BackupJopExtra backup, RestorePoint restorePoint, JopObject file)
        {
            File.Delete(backup.Jop.Path + "/" + file.Name + "_" + restorePoint.IdPoint + ".zip");
            restorePoint.Files.Remove(file);
        }

        public void TransFile(BackupJopExtra backup, string oldIdPoint, string newIdPoint, JopObject file)
        {
            string oldName = backup.Jop.Path + "/" + file.Name + "_" + oldIdPoint + ".zip";
            string newName = backup.Jop.Path + "/" + file.Name + "_" + newIdPoint + ".zip";
            File.Move(oldName, newName);
        }
    }
}