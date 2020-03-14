using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace FileManager.Classes
{
    class Manager
    {
        public  event Action  excActionManager;

        private DirectoryInfo dirInfo;
        private FileInfo      fileInfo;

        public void CreateDirectory(string parentDirectoryPath, string name)
        {
            dirInfo = new DirectoryInfo(parentDirectoryPath);
            if (!dirInfo.Exists)
            {
                dirInfo.Create();
            }
            dirInfo.CreateSubdirectory(name);
        }

        public void CreateFile(string parentDirectoryPath, string name)
        {
            string fullName = String.Concat(parentDirectoryPath, name);
            fileInfo        = new FileInfo(fullName);

            if (!File.Exists(fullName))
            { fileInfo.Create(); }
        }

        public void Copy(string path)
        {

        }


        public void Delete(string path)
        {
            fileInfo = new FileInfo(path);
            try
            {
                if (IsDirectory(path))
                {
                    DirectoryInfo dirInfo = new DirectoryInfo(path);
                    dirInfo.Delete(true);
                }
                else
                  { fileInfo.Delete(); }
            }
            catch 
            {
                excActionManager?.Invoke();
            }
        }

        public void Move(string path, string newPath)
        {
            fileInfo = new FileInfo(path);

            if (fileInfo.Exists)
            {
                if (IsDirectory(path))
                {
                    if (!Directory.Exists(newPath))
                    {
                        dirInfo = new DirectoryInfo(path);
                        dirInfo.MoveTo(newPath);
                    }
                }
                else
                    if (!File.Exists(newPath))
                    {
                        fileInfo.MoveTo(newPath);
                    }

            }
        }

        private bool IsDirectory(string path)
        {
            if (File.GetAttributes(path).HasFlag(FileAttributes.Directory))
            { return true; }
            else
            { return false;}
        }
    }
}
