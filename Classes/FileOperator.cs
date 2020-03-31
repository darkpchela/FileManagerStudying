using System;
using System.Collections.Generic;
using System.IO;

namespace FileManager.Classes
{
    class FileOperator
    {
        public event Action excActionFile;
        public event Action SelectedFileChanged;
        public event Action SelectedDirectoryChanged;

        public FileDistributor fileDistributor  { get; } 
        public FileBuffer      buffer           { get; private set; }
        public FileInfo        fileInfo         { get; private set; }

        public bool          FileSelected  { get; private set; }

        public FileOperator()
        {
            fileDistributor = new FileDistributor(); 
            buffer          = new FileBuffer();
        }
        public void SelectFile(string path)//Maybe rebuild
        {
            try
            {
                fileInfo   = new FileInfo(path);
                FileSelected = true;

                if (PathValidator.IsDirectory(path))
                    SelectedDirectoryChanged?.Invoke();
                else
                    SelectedFileChanged?.Invoke();
            }
            catch
            {
                FileSelected = false;

                excActionFile?.Invoke();
            }
        }
        public void AddFilesToBuffer(List<string> paths)//OK
        {
            foreach (var item in paths)
            {
                if (!buffer.files.Contains(item))
                {
                    SelectFile(item);
                    if (FileSelected)
                    { buffer.Add(fileInfo); }
                }
            }
        }
    }
}
