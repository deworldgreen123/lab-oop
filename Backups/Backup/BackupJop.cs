using System.Collections.Generic;
using System.Linq;
using Backups.Algorithm;

namespace Backups.Backup
{
    public class BackupJop
    {
        public BackupJop(string path, IStorageAlgorithm algorithm)
        {
            JopObjects = new List<JopObject>();
            RestorePoints = new List<RestorePoint>();
            VirtualRestorePoints = new List<VirtualRestorePoint>();
            Algorithm = algorithm;
            Repository = new Repository();
            Path = Repository.CreateDirectory(path);
        }

        public List<VirtualRestorePoint> VirtualRestorePoints { get; set; }
        public string Path { get; set; }
        public List<JopObject> JopObjects { get; set; }
        public List<RestorePoint> RestorePoints { get; set; }
        public IStorageAlgorithm Algorithm { get; set; }
        public Repository Repository { get; set; }

        public void AddObject(string filePath, string fileName)
        {
            JopObjects.Add(new JopObject(filePath, fileName));
        }

        public void RemoveObject(string filePath, string fileName)
        {
            JopObject fileToRemove = JopObjects.SingleOrDefault(r => r.Name == fileName && r.Path == filePath);
            if (fileToRemove != null)
                JopObjects.Remove(fileToRemove);
        }

        public void Save()
        {
            var newRestorePoint = new RestorePoint(JopObjects, Path, (RestorePoints.Count + 1).ToString(), Algorithm);
            RestorePoints.Add(newRestorePoint);
        }

        public void MakeVirtualRestorePoint()
        {
            var newVirtualRestorePoint = new VirtualRestorePoint(JopObjects);
            VirtualRestorePoints.Add(newVirtualRestorePoint);
        }

        public bool CheckRestorePoint(int pointId, int filesNumber)
        {
            return RestorePoints[pointId - 1].ZipFiles.Count == filesNumber;
        }

        public bool IsFileHere(int pointId, string filePath)
        {
            return RestorePoints[pointId - 1].ZipFiles.Any(file => file.Name == filePath);
        }

        public bool CheckVirtualRestorePoint(int pointId, string filePath)
        {
            return VirtualRestorePoints[pointId - 1].Files.Any(file => file == filePath);
        }
    }
}