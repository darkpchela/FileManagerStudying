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
        public  event    Action    excActionManager;
        public           Action    Operation; 

        private DirectoryInfo   dirInfo;
        private FileInfo        fileInfo;

        private DirectoryLoader directoryLoader = new DirectoryLoader();

        public void CreateDirectory(string parentDirectoryPath, string name)
        {
            try
            {
                dirInfo = new DirectoryInfo(parentDirectoryPath);
                if (!dirInfo.Exists)
                {
                    dirInfo.Create();
                }
                dirInfo.CreateSubdirectory(name);
            }
            catch
            {
                excActionManager?.Invoke();
            }
        }

        public void CreateFile(string parentDirectoryPath, string name) //probably better not to use
        {
            string fullName = String.Concat(parentDirectoryPath, name);
            fileInfo        = new FileInfo(fullName);

            if (!File.Exists(fullName))
            { fileInfo.Create(); }
        }

        public void Copy(string file, string path)
        {
            string newPath;
            fileInfo = new FileInfo(file);
            newPath  = Path.Combine(path, fileInfo.Name);

            if (fileInfo.Exists)
            {
                fileInfo.CopyTo(newPath, true);
            }
        }


        public void Delete(string path)//OK
        {
            try
            {
                fileInfo = new FileInfo(path);

                if (directoryLoader.IsDirectory(path))
                {
                    DirectoryInfo dirInfo = new DirectoryInfo(path);
                    dirInfo.Delete(true);
                }
                else
                {   fileInfo.Delete(); }
            }
            catch
            {
                excActionManager?.Invoke();
            }
        }

        public void Move(string file, string newPath)
        {
            try
            {
                fileInfo = new FileInfo(file);

                if (fileInfo.Exists)  
                {
                    if (directoryLoader.IsDirectory(file))
                    {
                        if (!Directory.Exists(newPath))
                        {
                            dirInfo = new DirectoryInfo(file);
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
            catch
            {
                excActionManager?.Invoke();
            }
        }

        public void Rename(string oldName, string newName)
        {
            try
            {
                if (directoryLoader.IsDirectory(oldName))
                {
                    Directory.Move(oldName, oldName + "_temp");
                    Directory.Move(oldName + "_temp", newName);
                }
                else
                {
                    File.Move(oldName, oldName + "_temp");
                    File.Move(oldName + "_temp", newName);
                }
            }
            catch
            {
                Directory.Move(oldName + "_temp", oldName);
                excActionManager?.Invoke();
            }
        }//OK

    }
}
