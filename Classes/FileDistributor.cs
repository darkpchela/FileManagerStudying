using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.IO;
using FileManager.Classes.Etc;
using FileManager.Classes.Copiers;

namespace FileManager.Classes
{
    class FileDistributor
    {
        public event MessageEventHandler ExceptionAppeared;

        private FileBuffer      fileBuffer;
        private FileCopier      fileCopier;
        private DirectoryCopier directoryCopier;

        private DirectoryInfo   dirInfo;
        private FileInfo        fileInfo;

        public FileDistributor()
        {
            fileCopier      = new FileCopier();
            directoryCopier = new DirectoryCopier();
            fileBuffer      = new FileBuffer();
        }
        private void OnExceptionAppeared(string message)
        {
            ExceptionAppeared?.Invoke(this, message);
        }
        public void SubscribeToAlreadyExistedItemAppearedEvent(DialogOptionEventHandler<ExistedItemAppearedEventArgs> handler)
        {
            fileCopier.AlreadyExistedItemAppeared += handler;
            directoryCopier.AlreadyExistedItemAppeared += handler;
        }//Rebuild later

        public void CreateDirectory(string parentDirectoryPath, string name) 
        {
            try
            {
                dirInfo = new DirectoryInfo(parentDirectoryPath);
                dirInfo.CreateSubdirectory(name);
            }
            catch(Exception ex)
            {
                OnExceptionAppeared(ex.Message);
            }
        }//OK

        public void CreateFile(string parentDirectoryPath, string name) //not ready
        {
            string fullName = Path.Combine(parentDirectoryPath, name);
            fileInfo        = new FileInfo(fullName);

            if (!File.Exists(fullName))
                fileInfo.Create();
        }

        public async void CopyAsync(string fullName, string toDirectory)
        {
            await Task.Run(()=>Copy(fullName, toDirectory));
        }//Works shitty
        public void Copy(string fullName, string toDirectory)
        {
            try
            {
                if (PathValidator.IsDirectory(fullName))
                {
                    directoryCopier.SetDirectory(fullName);
                    directoryCopier.Copy(toDirectory);
                }
                else
                {
                    fileCopier.SetFile(fullName);
                    fileCopier.Copy(toDirectory);
                }
            }
            catch(Exception ex)
            { 
                OnExceptionAppeared(ex.Message);
            }

        }//OK

        public void Delete(string fullName)//OK
        {
            try
            {
                fileInfo = new FileInfo(fullName);

                if (PathValidator.IsDirectory(fullName))
                {
                    DirectoryInfo dirInfo = new DirectoryInfo(fullName);
                    dirInfo.Delete(true);
                }
                else
                {   fileInfo.Delete(); }
            }
            catch(Exception ex)
            {
                OnExceptionAppeared(ex.Message);
            }
        }

        public void Move(string fullName, string toDirectory)
        {
            try
            {
                if (PathValidator.IsDirectory(fullName))
                {
                    directoryCopier.SetDirectory(fullName);
                    directoryCopier.Move(toDirectory);
                }
                else
                {
                    fileCopier.SetFile(fullName);
                    fileCopier.Move(toDirectory);
                }

            }
            catch (Exception ex)
            {
                OnExceptionAppeared(ex.Message);
            }

        }//OK

        public void Rename(string oldPath, string newPath)
        {
            try
            {
                if (PathValidator.IsDirectory(oldPath))
                {
                    Directory.Move(oldPath, oldPath + "_temp");
                    Directory.Move(oldPath + "_temp", newPath);
                }
                else
                {
                    File.Move(oldPath, oldPath + "_temp");
                    File.Move(oldPath + "_temp", newPath);
                }
            }
            catch(Exception ex)
            {
                Directory.Move(oldPath + "_temp", oldPath);
                OnExceptionAppeared(ex.Message);
            }
        }//OK
        public void Rename(string directory, string oldName, string newName)
        {
            string oldPath = Path.Combine(directory, oldName);
            string newPath = Path.Combine(directory, newName);
            Rename(oldPath, newPath);
        }//OK
        public void AddFilesToBuffer(List<string> paths)//OK
        {
            foreach (var item in paths)
            {
                if (!fileBuffer.files.Contains(item) && (File.Exists(item) || Directory.Exists(item)))
                    fileBuffer.Add(item);
            }
        }
        public string[] GetFilesFromBuffer()
        {
            string[] files;
            files = fileBuffer.files.ToArray();
            return files;
        }
        public void ClearBuffer()
        {
            fileBuffer.Clear();
        }

        public void RemoveFromBuffer(string name)
        {
            fileBuffer.Remove(name);
        }
    }
}
