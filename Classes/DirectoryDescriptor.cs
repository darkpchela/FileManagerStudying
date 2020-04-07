using System.Collections.Generic;
using System.IO;
using System.Linq;
using System;

namespace FileManager.Classes
{
    class DirectoryDescriptor
    {
        public DirectoryInfo   currentDirectory { get; private set; }
        public FileInfo[]      childFiles       { get; private set; }
        public DirectoryInfo[] childDirectories { get; private set; }
        public FileInfo[]      allFiles         { get; private set; }
        public DirectoryInfo[] allDirectories   { get; private set; }
        public void SetDirectory(string path)
        {
            SetDirectory(new DirectoryInfo(path));
        }//OK
        public void SetDirectory(DirectoryInfo directory)
        {
            currentDirectory = directory;
            childFiles       = currentDirectory.GetFiles();
            childDirectories = currentDirectory.GetDirectories();
        }//OK

        public void LoadAllSubFilesAndDirectories()
        {
            List<FileInfo>      tempAllFiles       = new List<FileInfo>();
            List<DirectoryInfo> tempAllDirectories = new List<DirectoryInfo>();
            GetAllFilesAndDirectoriesFromDirectory(currentDirectory.FullName, ref tempAllFiles, ref tempAllDirectories);

            allFiles       = tempAllFiles.ToArray();
            allDirectories = tempAllDirectories.ToArray();
        }
        private void GetAllFilesAndDirectoriesFromDirectory(string name, ref List<FileInfo> filesOut, ref List<DirectoryInfo> directoriesOut)
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
       
        }//Maybe rebuild later
    }

}
