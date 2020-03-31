using System.Collections.Generic;
using System.IO;
using System.Linq;
using System;

namespace FileManager.Classes
{
    class DirectoryLoader
    {
        public DirectoryInfo   currentDirectory { get; private set; }
        public FileInfo[]      files            { get; private set; }
        public DirectoryInfo[] directories      { get; private set; }
        public DirectoryLoader(DirectoryInfo directory)
        {
            currentDirectory = directory;
        }
        public DirectoryLoader(string path)
        {
            currentDirectory = new DirectoryInfo(path);
        }
        public void LoadDirectory()
        {
            files       = currentDirectory.GetFiles();
            directories = currentDirectory.GetDirectories();
        }//OK

        public bool TryLoadDirectory()
        {
            try
            {
                this.files       = currentDirectory.GetFiles();
                this.directories = currentDirectory.GetDirectories();

                return true;
            }
            catch
            {
                return false;
            }

        }//OK
        public static void GetAllFilesAndDirectoriesFromDirectory(string name, ref List<FileInfo> filesOut, ref List<DirectoryInfo> directoriesOut)
        {

            if (Directory.Exists(name))
            {
                DirectoryInfo dirInfo = new DirectoryInfo(name);

                directoriesOut.Add(dirInfo);
                filesOut.AddRange(dirInfo.GetFiles());

                if (dirInfo.GetDirectories().Any())
                {
                    foreach (var dir in dirInfo.GetDirectories())
                    {
                        GetAllFilesAndDirectoriesFromDirectory(dir.FullName, ref filesOut, ref directoriesOut);
                    }
                }
            }
       
        }//OK
    }

}
