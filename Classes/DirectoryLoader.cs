using System.Collections.Generic;
using System.IO;

namespace FileManager.Classes
{
    class DirectoryLoader
    {   
        public bool IsDirectory(string path)
        {
            FileAttributes _fileAttributes = File.GetAttributes(path);

            if (_fileAttributes.HasFlag(FileAttributes.Directory))
                { return true; }
            else
                { return false;}
        }

        public void LoadDirectory(string path, ref string[] files, ref string[] directories)
        {
            DirectoryInfo   dirInfo;
            FileInfo[]      fileInfo;
            DirectoryInfo[] directoriesInfo;

            if (IsDirectory(path))
            {
                dirInfo         = new DirectoryInfo(path);
                fileInfo        = dirInfo.GetFiles();
                directoriesInfo = dirInfo.GetDirectories();
                files           = FileSystemInfoNames(fileInfo);
                directories     = FileSystemInfoNames(directoriesInfo);
            }

            string[] FileSystemInfoNames<T>(T[] list) where T : FileSystemInfo
            {
                List<string> temp = new List<string>();
                foreach (var item in list)
                {
                    temp.Add(item.Name);
                }
                temp.Sort();
                string[] array = temp.ToArray();
                return array;
            }
        }
    }

}
