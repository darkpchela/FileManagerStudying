using System;
using System.Collections.Generic;
using System.IO;
using FileManager.Classes.Etc;
namespace FileManager.Classes
{
    class FileDescriptor
    {
        public event MessageEventHandler ExceptionAppeared;
        public event EventHandler<SelectedFileChangedEventArgs> SelectedFileChanged;

        public FileInfo  currentSelectedFileInfo         { get; private set; }

        public bool      FileSelected                    { get; private set; }

        private void OnSelectedFileChanged(SelectedFileChangedEventArgs e)
        {
            SelectedFileChanged?.Invoke(this, e);
        }

        private void OnExceptionAppeared(string message)
        {
            ExceptionAppeared?.Invoke(this, message);
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
    }
}
