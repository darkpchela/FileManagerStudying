using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using FileManager.Classes.Etc;

namespace FileManager.Classes.Copiers
{
    class DirectoryCopier:ICopier
    {
        public event DialogOptionEventHandler<ExistedItemAppearedEventArgs>  AlreadyExistedItemAppeared;
        public DirectoryInfo       currentDirectoryInfo             { get; private set; }
        public List<DirectoryInfo> AllDirectoriesAtCurrentDirectory { get; private set; }
        public List<FileInfo>      AllFilesAtCurrentDirectory       { get; private set; }

        private DialogOptions currentSelectedOption;

        private void OnAlreadyExistedFileAppeared(ExistedItemAppearedEventArgs e)
        {
            DialogOptionEventHandler<ExistedItemAppearedEventArgs> handler = AlreadyExistedItemAppeared;
            currentSelectedOption = handler?.Invoke(this,e)??DialogOptions.Cancel;
        }
        public void SetDirectory(DirectoryInfo directory)
        {
            currentDirectoryInfo = directory;

            List<DirectoryInfo> tempDirectories = new List<DirectoryInfo>();
            List<FileInfo>      tempFiles       = new List<FileInfo>();

            DirectoryLoader.GetAllFilesAndDirectoriesFromDirectory(currentDirectoryInfo.FullName, ref tempFiles, ref tempDirectories);

            AllDirectoriesAtCurrentDirectory = tempDirectories;
            AllFilesAtCurrentDirectory       = tempFiles;
        }//OK
        public void SetDirectory(string path)
        {
            currentDirectoryInfo = new DirectoryInfo(path);

            List<DirectoryInfo> tempDirectories = new List<DirectoryInfo>();
            List<FileInfo>      tempFiles       = new List<FileInfo>();

            DirectoryLoader.GetAllFilesAndDirectoriesFromDirectory(currentDirectoryInfo.FullName, ref tempFiles, ref tempDirectories);

            AllDirectoriesAtCurrentDirectory = tempDirectories;
            AllFilesAtCurrentDirectory       = tempFiles;
        }//OK

        public void Copy(string toDirectory, bool overwrite = false)
        {
            currentSelectedOption = DialogOptions.Default;

            List<(string, string)> fileCollisions = new List<(string, string)>();
            string deltaPath = currentDirectoryInfo.Parent.FullName;

            currentSelectedOption = DialogOptions.Default;

            if (deltaPath == toDirectory)
            {
                if (currentDirectoryInfo.FullName.Contains(" -copy"))
                {
                    toDirectory = currentDirectoryInfo.FullName;
                    PathValidator.FileCopyPathUpdate(ref toDirectory);
                }
                else
                {
                    toDirectory = currentDirectoryInfo.FullName + " -copy";
                    PathValidator.FileCopyPathUpdate(ref toDirectory);
                }
            }
            else
                if (toDirectory.StartsWith(deltaPath))
                throw new Exception("The final folder is a child of the folder, in which it is located!");


            foreach (var dir in AllDirectoriesAtCurrentDirectory)
            {
                string tempDirectoryName = dir.FullName.Replace(deltaPath, toDirectory);

                if (!Directory.Exists(tempDirectoryName))
                    Directory.CreateDirectory(tempDirectoryName);
            }

            foreach (var file in AllFilesAtCurrentDirectory)
            {
                string tempFilePath = file.FullName.Replace(deltaPath, toDirectory);

                if (!File.Exists(tempFilePath))
                    file.CopyTo(tempFilePath);
                else
                {
                    var collision = (file.FullName, tempFilePath);
                    fileCollisions.Add(collision);
                }
            }


            if (fileCollisions.Count > 0)
            {

                foreach (var file in fileCollisions)
                {
                    if (currentSelectedOption != DialogOptions.YesToAll && currentSelectedOption != DialogOptions.NoToAll)
                    {
                        ExistedItemAppearedEventArgs e = new ExistedItemAppearedEventArgs();

                        e.collisions       = fileCollisions.ToArray();
                        e.multipleProcces  = true;
                        e.currentCollision = file;

                        OnAlreadyExistedFileAppeared(e);
                    }

                    switch (currentSelectedOption)
                    {
                        case DialogOptions.Yes:
                            File.Copy(file.Item1, file.Item2, true);
                            break;

                        case DialogOptions.No:
                            continue;

                        case DialogOptions.NoToAll:
                            goto case DialogOptions.No;

                        case DialogOptions.YesToAll:
                            goto case DialogOptions.Yes;

                        default:
                            return;
                    }
                }
            }
        }//Probably works
        public bool TryCopy(string toDirectory, bool overwrite = false)
        {
            try
            {
                Copy(toDirectory, overwrite);
                return true;
            }
            catch
            {
                return false;
            }
        }//OK

        public void Move(string toDirectory)
        {
            currentSelectedOption = DialogOptions.Default;

            string deltaPath = currentDirectoryInfo.Parent.FullName;
            string newPath   = Path.Combine(toDirectory, currentDirectoryInfo.Name);

            List<(string, string)> fileCollisions = new List<(string, string)>();
            List<string> tempCreatedDirectories   = new List<string>();


            foreach (var dir in AllDirectoriesAtCurrentDirectory)
            {
                string tempDirectoryName = dir.FullName.Replace(deltaPath, toDirectory);

                if (!Directory.Exists(tempDirectoryName))
                    Directory.CreateDirectory(tempDirectoryName);
            }

            foreach (var file in AllFilesAtCurrentDirectory)
            {
                string tempFilePath = file.FullName.Replace(deltaPath, toDirectory);

                if (!File.Exists(tempFilePath))
                    file.MoveTo(tempFilePath);
                else
                {
                    var collision = (file.FullName, tempFilePath);
                    fileCollisions.Add(collision);
                }
            }


            if (fileCollisions.Count > 0)
            {

                foreach (var file in fileCollisions)
                {
                    if (currentSelectedOption!=DialogOptions.YesToAll&&currentSelectedOption!=DialogOptions.NoToAll)
                    {
                        ExistedItemAppearedEventArgs e = new ExistedItemAppearedEventArgs();

                        e.collisions       = fileCollisions.ToArray();
                        e.multipleProcces  = true;
                        e.currentCollision = file;

                        OnAlreadyExistedFileAppeared(e);
                    }

                    switch (currentSelectedOption)
                    {
                        case DialogOptions.Yes:
                            File.Copy(file.Item1, file.Item2, true);
                            File.Delete(file.Item1);
                            break;

                        case DialogOptions.No:
                            continue;

                        case DialogOptions.NoToAll:
                            goto case DialogOptions.No;

                        case DialogOptions.YesToAll:
                            goto case DialogOptions.Yes;

                        default:
                            fileCollisions.Clear();
                            break;
                    }
                }
            }
        }//Probably works

        public bool TryMove(string toDirectory)
        {
            try
            {
                Move(toDirectory);
                return true;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }//OK
    }
}
