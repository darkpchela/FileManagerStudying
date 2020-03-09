using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManager.Classes
{
    class DirectoryController
    {
        public Action excActionPath;
        public Action DirectoryLoaded;

        public PathHistory pathHistory = new PathHistory();
        public string currentPath { get; private set; }

        public void SetPath(string path)
        {
            if (path != null)
                currentPath = path.Replace(@"\\", @"\"); 
        }
        public bool IsAccessiblePath(string path)
        {
            try 
            {
                DirectoryInfo dirInfo = new DirectoryInfo(path);
                dirInfo.GetFiles();
                return true;
            }
            catch { excActionPath(); return false; }
        }
        public bool IsDirectory(string path)
        {
            FileAttributes _fileAttributes = File.GetAttributes(path);
            if (_fileAttributes.HasFlag(FileAttributes.Directory))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public void LoadDirectory(out string[] files, out string[] directories)
        {
            DirectoryInfo   dirInfo;
            FileInfo[]      fileInfo;
            DirectoryInfo[] directoriesInfo;

            if (IsDirectory(currentPath))
            {
                dirInfo         = new DirectoryInfo(currentPath);
                fileInfo        = dirInfo.GetFiles();
                directoriesInfo = dirInfo.GetDirectories();

                    if (!pathHistory.globalHistory.Contains(currentPath))
                         pathHistory.globalHistory.Add(currentPath); 
            }
            else
            {
                dirInfo         = new DirectoryInfo(pathHistory.globalHistory.Last());
                fileInfo        = dirInfo.GetFiles();
                directoriesInfo = dirInfo.GetDirectories();
            }

            files       = FileSystemInfoNames(fileInfo);
            directories = FileSystemInfoNames(directoriesInfo);

            string[] FileSystemInfoNames<T>(T[] list) where T : FileSystemInfo
            {
                List<string> temp = new List<string>();
                temp.Sort();
                foreach (var item in list)
                {
                    temp.Add(item.Name);
                }
                string[] array = temp.ToArray();
                return array;
            }
        }
    
    }
}
