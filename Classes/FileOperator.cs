using System;
using System.Collections.Generic;
using System.IO;
using FileManager.Classes.Etc;
namespace FileManager.Classes
{
    class FileOperator
    {
        //public event MessageHandler ExceptionMessage;
        //public event Action SelectedFileChanged;
        //public event Action SelectedDirectoryChanged;
        public event MessageEventHandler ExceptionAppeared;
        public event EventHandler SelectedFileChanged;
        public event EventHandler SelectedDirectoryChanged;

        private void OnSelectedFileChanged(EventArgs e)
        {
            SelectedFileChanged?.Invoke(this, e);
        }

        private void OnSelectedDirectoryChanged(EventArgs e)
        {
            SelectedDirectoryChanged?.Invoke(this, e);
        }

        private void OnExceptionAppeared(string message)
        {
            ExceptionAppeared?.Invoke(this, message);
        }

        //public FileDistributor fileDistributor  { get; } 
        //public FileBuffer      buffer           { get; private set; }
        public FileInfo        currentSelectedFileInfo         { get; private set; }

        public bool          FileSelected  { get; private set; }

        public FileOperator()
        {
            FileSelected = false;
        }
        public void SelectFile(string path)//Maybe rebuild
        {
            try
            {
                currentSelectedFileInfo = new FileInfo(path);
                FileSelected = true;
                SelectedFileChangedEventArgs e = new SelectedFileChangedEventArgs();

                if (PathValidator.IsDirectory(path))
                    e.isDirectory = true;
                else
                    e.isDirectory = false;

                e.fullName = currentSelectedFileInfo.FullName;
                OnSelectedFileChanged(e);
            }
            catch (Exception ex)
            {
                FileSelected = false;
                OnExceptionAppeared(ex.Message);
            }
        }
        //public void AddFilesToBuffer(List<string> paths)//OK
        //{
        //    foreach (var item in paths)
        //    {
        //        if (!buffer.files.Contains(item))
        //        {
        //            SelectFile(item);
        //            if (FileSelected)
        //            { buffer.Add(fileInfo); }
        //        }
        //    }
        //}
    }
}
