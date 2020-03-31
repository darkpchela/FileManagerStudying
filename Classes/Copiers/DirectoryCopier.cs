using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace FileManager.Classes.Copiers
{
    class DirectoryCopier
    {
        public event EventHandler AlreadyExistedFileAppeared;
        public DirectoryInfo       currentDirectory                 { get; private set; }
        public List<DirectoryInfo> AllDirectoriesAtCurrentDirectory { get; private set; }
        public List<FileInfo>      AllFilesAtCurrentDirectory       { get; private set; }

        private void OnAlreadyExistedFileAppeared(EventArgs e)
        {
            EventHandler handler = AlreadyExistedFileAppeared;
            handler?.Invoke(this,e);
        }
        public void SetDirectory(DirectoryInfo directory)
        {
            currentDirectory = directory;

            List<DirectoryInfo> tempDirectories = new List<DirectoryInfo>();
            List<FileInfo>      tempFiles       = new List<FileInfo>();

            DirectoryLoader.GetAllFilesAndDirectoriesFromDirectory(currentDirectory.FullName, ref tempFiles, ref tempDirectories);

            AllDirectoriesAtCurrentDirectory = tempDirectories;
            AllFilesAtCurrentDirectory       = tempFiles;
        }
        public void SetDirectory(string path)
        {
            currentDirectory = new DirectoryInfo(path);

            List<DirectoryInfo> tempDirectories = new List<DirectoryInfo>();
            List<FileInfo> tempFiles            = new List<FileInfo>();

            DirectoryLoader.GetAllFilesAndDirectoriesFromDirectory(currentDirectory.FullName, ref tempFiles, ref tempDirectories);

            AllDirectoriesAtCurrentDirectory = tempDirectories;
            AllFilesAtCurrentDirectory       = tempFiles;
        }

        public bool TryCopyDirectory(string toDirectory, bool overwrite = false)
        {
            try
            {
                string deltaPath = currentDirectory.Parent.FullName;

                if (deltaPath == toDirectory)
                {
                    if (currentDirectory.FullName.Contains(" -copy"))
                    {
                        toDirectory = currentDirectory.FullName;
                        PathValidator.FileCopyPathUpdate(ref toDirectory);
                    }
                    else
                    {
                        toDirectory = currentDirectory.FullName + " -copy";
                        PathValidator.FileCopyPathUpdate(ref toDirectory);
                    }
                }

                foreach (var dir in AllDirectoriesAtCurrentDirectory)
                {
                    string tempDirectoryName = dir.FullName.Replace(deltaPath, toDirectory);

                    DirectoryInfo dirInfo = new DirectoryInfo(tempDirectoryName);

                    if (!dirInfo.Exists)
                    { dirInfo.Create(); }

                }

                foreach (var file in AllFilesAtCurrentDirectory)
                {
                    string tempFilePath = file.FullName.Replace(deltaPath, toDirectory);

                    if (!File.Exists(tempFilePath))
                        file.CopyTo(tempFilePath);
                    else
                    {
                        file.CopyTo(tempFilePath, overwrite);
                    }
                }

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
