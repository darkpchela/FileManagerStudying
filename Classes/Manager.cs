using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using FileManager.Classes.Etc;

namespace FileManager.Classes
{
    class Manager
    {
        public  MessageHandler      exActionManager;
        public  DialogOptionHandler overwriteOptions;

        private DirectoryInfo   dirInfo;
        private FileInfo        fileInfo;
        private DirectoryLoader directoryLoader = new DirectoryLoader();
        private DialogOptions   option;

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
            catch(Exception ex)
            {
                exActionManager?.Invoke(ex.Message);
            }
        }

        public void CreateFile(string parentDirectoryPath, string name) //probably better not to use yet
        {
            string fullName = String.Concat(parentDirectoryPath, name);
            fileInfo        = new FileInfo(fullName);

            if (!File.Exists(fullName))
            { fileInfo.Create(); }
        }

        public async void CopyAsync(string name, string path)
        {
            await Task.Run(()=>Copy(name, path));
        }
        public void Copy(string name, string path)
        {
            string newPath          = "";
            bool   optionSetted     = false;
            DialogOptions option    = DialogOptions.Cancel;

            try
            {
                if (directoryLoader.IsDirectory(name))
                {
                    List<FileInfo> files            = new List<FileInfo>();
                    List<DirectoryInfo> directories = new List<DirectoryInfo>();

                    string deltaPath = Directory.GetParent(name).FullName;

                    directoryLoader.GetAllFilesAndDirectoriesFromDirectory(name, ref files, ref directories);

                    if (deltaPath == path)
                    {
                        deltaPath = name;
                        path = deltaPath + "-copy";
                    }


                    foreach (var dir in directories)
                    {
                        newPath = dir.FullName.Replace(deltaPath, path);
                        dirInfo = new DirectoryInfo(newPath);
                        if (!Directory.Exists(newPath))
                        {
                            dirInfo.Create();
                        }
                    }
                    

                    foreach (var file in files)
                    {
                        newPath = file.FullName.Replace(deltaPath, path);

                        if (!File.Exists(newPath))
                        { file.CopyTo(newPath); }
                        else
                        {
                            if (!optionSetted)
                            {
                                option = overwriteOptions?.Invoke() ?? DialogOptions.No;
                                optionSetted = true;
                            }

                            switch (option)
                            {
                                case DialogOptions.No:
                                    continue;

                                case DialogOptions.Yes:
                                    file.CopyTo(newPath, true);
                                    break;

                                case DialogOptions.Cancel:
                                    break;

                                default:
                                    break;
                            }
                        }
                    }

                }
                else
                {
                    fileInfo = new FileInfo(name);
                    newPath = Path.Combine(path, fileInfo.Name);

                    if (fileInfo.Exists)
                    {
                        if (!File.Exists(newPath))
                        { fileInfo.CopyTo(newPath, false); }
                        else
                        {
                            option = overwriteOptions?.Invoke() ?? DialogOptions.Cancel;

                            switch (option)
                            {
                                case DialogOptions.No:
                                    break;

                                case DialogOptions.Yes:
                                    fileInfo.CopyTo(newPath, true);
                                    break;

                                default:
                                    break;
                            }
                        }
                    }
                }
            }
            catch(Exception ex)
            { exActionManager?.Invoke(ex.Message); }
            
        }//Probably ready

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
            catch(Exception ex)
            {
                exActionManager?.Invoke(ex.Message);
            }
        }

        public void Move(string file, string newPath)
        {
           

            try
            {
                fileInfo = new FileInfo(file);
                newPath = Path.Combine(newPath, fileInfo.Name);

                if (directoryLoader.IsDirectory(file))
                {
                    if (!Directory.Exists(newPath))
                    {
                        dirInfo = new DirectoryInfo(file);
                        dirInfo.MoveTo(newPath);
                    }
                }
                else
                {
                    if (fileInfo.Exists)
                    {
                        if (File.Exists(newPath))
                        {
                            option = overwriteOptions?.Invoke() ?? DialogOptions.No;

                            switch(option)
                            {
                                case DialogOptions.Yes:
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
                        { fileInfo.MoveTo(newPath); }
                    }
                    else
                    { throw new NullReferenceException("Trying to move not existed file or directory."); }
                }

            }
            catch (Exception ex)
            {
                exActionManager?.Invoke(ex.Message);
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
            catch(Exception ex)
            {
                Directory.Move(oldName + "_temp", oldName);
                exActionManager?.Invoke(ex.Message);
            }
        }//OK

    }
}
