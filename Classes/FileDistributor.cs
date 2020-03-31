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
        public  MessageHandler      exActionManager;
        public  DialogOptionHandler overwriteOptions;

        private DirectoryInfo   dirInfo;
        private FileInfo        fileInfo;

        private DialogOptions   option;

        public void CreateDirectory(string parentDirectoryPath, string name) 
        {
            try
            {
                dirInfo = new DirectoryInfo(parentDirectoryPath);
                dirInfo.CreateSubdirectory(name);
            }
            catch(Exception ex)
            {
                exActionManager?.Invoke(ex.Message);
            }
        }//OK

        public void CreateFile(string parentDirectoryPath, string name) //not ready
        {
            string fullName = Path.Combine(parentDirectoryPath, name);
            fileInfo        = new FileInfo(fullName);

            if (!File.Exists(fullName))
            { fileInfo.Create(); }
        }

        public async void CopyAsync(string fullName, string toDirectory)
        {
            await Task.Run(()=>Copy(fullName, toDirectory));
        }
        public void CopyRange(string[] fullNames, string toDirectory)
        {
            bool optionSetted = false;
            int count = fullNames.Length;
            while (count>0)
            {
                foreach (var file in fullNames)
                {
                    Copy(file, toDirectory);
                }
                count--;
            }
        }//Not ready
        public void Copy(string fullName, string toDirectory, DialogOptions option=DialogOptions.Cancel)
        {
            try
            {
                if (PathValidator.IsDirectory(fullName))
                {
                    DirectoryCopier directoryCopier = new DirectoryCopier();
                    directoryCopier.SetDirectory(fullName);
                    if (!directoryCopier.TryCopyDirectory(toDirectory))
                    {
                        //option = overwriteOptions?.Invoke() ?? DialogOptions.No;
                        switch (option)
                        {
                            case DialogOptions.No:
                                break;

                            case DialogOptions.Yes:
                                directoryCopier.TryCopyDirectory(toDirectory, true);
                                break;

                            case DialogOptions.Cancel:
                                break;

                            default:
                                break;
                        }
                    }
                }
                else
                {
                    FileCopier fileCopier = new FileCopier();
                    fileCopier.SetFile(fullName);
                    if (!fileCopier.TryCopy(toDirectory))
                    {
                        option = overwriteOptions?.Invoke() ?? DialogOptions.Cancel;

                        switch (option)
                        {
                            case DialogOptions.No:
                                break;

                            case DialogOptions.Yes:
                                fileCopier.TryCopy(toDirectory, true);
                                break;

                            default:
                                break;
                        }
                    }

                }
            }
            catch(Exception ex)
            { exActionManager?.Invoke(ex.Message); }

        }//nearly ready

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
                exActionManager?.Invoke(ex.Message);
            }
        }

        public void Move(string fullName, string toDirectory)
        {
            try
            {
                string newPath    = "";
                bool isDirectory  = false;
                bool optionSetted = false;

                fileInfo = new FileInfo(fullName);
                newPath  = Path.Combine(toDirectory, fileInfo.Name);

                if (PathValidator.IsDirectory(fullName))
                {
                    isDirectory = true;
                    dirInfo = new DirectoryInfo(fullName);
                }

                if (fileInfo.Exists || dirInfo.Exists)
                {
                    if (File.Exists(newPath) || Directory.Exists(newPath))
                    {
                        if (!optionSetted)
                        {
                            option       = overwriteOptions?.Invoke() ?? DialogOptions.Cancel;
                            optionSetted = true;
                        }

                        switch (option)
                        {
                            case DialogOptions.Yes:

                                if (isDirectory)
                                    dirInfo.MoveTo(newPath);
                                else
                                    fileInfo.MoveTo(newPath);
                                break;

                            case DialogOptions.No:
                                break;

                            case DialogOptions.Cancel:
                                goto case DialogOptions.No;

                            default:
                                break;
                        }
                    }
                    else
                    {
                        if (isDirectory)
                            dirInfo.MoveTo(newPath);
                        else
                            fileInfo.MoveTo(newPath);
                    }
                }


            }
            catch (Exception ex)
            {
                exActionManager?.Invoke(ex.Message);
            }

        }//Nearly ready

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
                exActionManager?.Invoke(ex.Message);
            }
        }//OK
        public void Rename(string path, string oldName, string newName)
        {
            string oldPath = Path.Combine(path, oldName);
            string newPath = Path.Combine(path, newName);
            this.Rename(oldPath, newPath);
        }//OK
    }
}
