using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace FileManager.Classes
{
    delegate OverwriteOptions OverwriteOptionsHandler();
    enum OverwriteOptions { Yes, No, YesToAll, NoToAll, Cancel }
    class Manager
    {
        public  event    Action    excActionManager;
        public           Action    Operation;

        public OverwriteOptionsHandler overwriteOptions;

        private DirectoryInfo   dirInfo;
        private FileInfo        fileInfo;

        public  DirectoryLoader directoryLoader = new DirectoryLoader();

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

        public void Copy(string name, string path)
        {
            string newPath          = "";
            bool   optionSetted     = false;
            OverwriteOptions option = OverwriteOptions.Cancel;

            try
            {
                if (directoryLoader.IsDirectory(name))
                {
                    List<FileInfo> files = new List<FileInfo>();
                    List<DirectoryInfo> directories = new List<DirectoryInfo>();

                    string deltaPath = Directory.GetParent(name).FullName;

                    directoryLoader.GetAllFilesAndDirectoriesFromDirectory(name, ref files, ref directories);

                    if (deltaPath == path)
                    {
                        deltaPath = directories.First().FullName;
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
                                option = overwriteOptions?.Invoke() ?? OverwriteOptions.No;
                                optionSetted = true;
                            }

                            switch (option)
                            {
                                case OverwriteOptions.No:
                                    continue;

                                case OverwriteOptions.Yes:
                                    file.CopyTo(newPath, true);
                                    break;

                                case OverwriteOptions.Cancel:
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
                            option = overwriteOptions?.Invoke() ?? OverwriteOptions.Cancel;

                            switch (option)
                            {
                                case OverwriteOptions.No:
                                    break;

                                case OverwriteOptions.Yes:
                                    fileInfo.CopyTo(newPath, true);
                                    break;

                                default:
                                    break;
                            }
                        }
                    }
                }
            }
            catch
            { excActionManager?.Invoke(); }
            
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
