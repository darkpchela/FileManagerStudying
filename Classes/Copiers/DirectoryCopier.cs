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

        public DirectoryDescriptor directoryDescriptor;

        private DialogOptions currentSelectedOption;

        private void OnAlreadyExistedFileAppeared(ExistedItemAppearedEventArgs e)
        {
            DialogOptionEventHandler<ExistedItemAppearedEventArgs> handler = AlreadyExistedItemAppeared;
            currentSelectedOption = handler?.Invoke(this,e)??DialogOptions.Cancel;
        }
        public void SetDirectory(DirectoryInfo directory)
        {
            directoryDescriptor = new DirectoryDescriptor();
            directoryDescriptor.SetDirectory(directory);
            directoryDescriptor.LoadAllSubFilesAndDirectories();

        }//OK
        public void SetDirectory(string path)
        {
            directoryDescriptor = new DirectoryDescriptor();
            directoryDescriptor.SetDirectory(path);
            directoryDescriptor.LoadAllSubFilesAndDirectories();
        }//OK

        public void Copy(string toDirectory, bool overwrite = false)
        {
            currentSelectedOption = DialogOptions.Default;

            List<(string, string)> fileCollisions = new List<(string, string)>();
            string deltaPath = directoryDescriptor.currentDirectory.Parent.FullName;

            if (deltaPath == toDirectory)
            {
                if (directoryDescriptor.currentDirectory.Name.Contains(" -copy"))
                {
                    toDirectory = directoryDescriptor.currentDirectory.FullName;
                    PathValidator.FileCopyPathUpdate(ref toDirectory);
                }
                else
                {
                    toDirectory = directoryDescriptor.currentDirectory.FullName + " -copy";
                    PathValidator.FileCopyPathUpdate(ref toDirectory);
                }
            }
            else
                if (toDirectory.StartsWith(directoryDescriptor.currentDirectory.FullName))
                throw new Exception("The final folder is a child of the folder, in which it is located!");


            foreach (var dir in directoryDescriptor.allDirectories)
            {
                string tempDirectoryName = dir.FullName.Replace(deltaPath, toDirectory);

                if (!Directory.Exists(tempDirectoryName))
                    Directory.CreateDirectory(tempDirectoryName);
            }

            foreach (var file in directoryDescriptor.allFiles)
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

            string deltaPath = directoryDescriptor.currentDirectory.Parent.FullName;
            string newPath   = Path.Combine(toDirectory, directoryDescriptor.currentDirectory.Name);

            List<(string, string)> fileCollisions = new List<(string, string)>();
            List<string> tempCreatedDirectories   = new List<string>();

            //-----------it can be solved in another way
            if (toDirectory.StartsWith(directoryDescriptor.currentDirectory.FullName))
                throw new Exception("The final folder is a child of the folder, in which it is located!");
            //-----------------------------------------------

            foreach (var dir in directoryDescriptor.allDirectories)
            {
                string tempDirectoryName = dir.FullName.Replace(deltaPath, toDirectory);

                if (!Directory.Exists(tempDirectoryName))
                    Directory.CreateDirectory(tempDirectoryName);
            }

            foreach (var file in directoryDescriptor.allFiles)
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
                            return;
                    }
                }
            }
            directoryDescriptor.currentDirectory.Delete(true);
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
