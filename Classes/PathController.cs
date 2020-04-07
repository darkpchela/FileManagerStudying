using System;
using System.IO;
using System.Linq;
using FileManager.Classes.Etc;

namespace FileManager.Classes
{
    class PathController
    {
        public event MessageEventHandler ExceptionAppeared;
        public event EventHandler        CurrentPathChanged;

        private History             pathHistory;
        public  DirectoryDescriptor directoryDescriptor;
        public string currentPath { get; private set; }
        
        public PathController()
        {
            pathHistory     = new History();
            directoryDescriptor = new DirectoryDescriptor();
        }

        //Events<>
        private void OnExceptionAppeared(string message)
        {
            ExceptionAppeared?.Invoke(this, message);
        }
        private void OnCurrentPathChanged(EventArgs e)
        {
            CurrentPathChanged?.Invoke(this, e);
        }
        //Events<.>


        public void SetPath(string path)
        {
            try
            {
                if (PathValidator.IsDirectory(path))
                    currentPath = path.Replace(@"\\", @"\");
                else
                    currentPath = pathHistory.globalHistory.Last();

                LoadDirectory();
                OnCurrentPathChanged(EventArgs.Empty);
            }
            catch(Exception ex)
            {
                OnExceptionAppeared(ex.Message);
            }
        }
        public void SetParentDirectoryPath()
        {
            string parentDirectoryPath;

            try
            {
                parentDirectoryPath = Directory.GetParent(currentPath).ToString();
                SetPath(parentDirectoryPath);
            }
            catch(Exception ex) 
            {
                OnExceptionAppeared(ex.Message);
            }
        }     
        public void SetPreviousPath()
        {
            string previousPath;

            previousPath = pathHistory.GetPreviousElement();
            SetPath(previousPath);
        }
        public void SetNextPath()
        {
            string nextPath;

            nextPath = pathHistory.GetNextElement();
            SetPath(nextPath);
        }
        public string[] GetPathHistory()
        {
            string[] history = pathHistory.globalHistory.ToArray();
            return history;
        }

        //Sugaring<>
        private void LoadDirectory()
        {
            try
            {
                directoryDescriptor.SetDirectory(currentPath);
                VerifyHistory();
            }
            catch(Exception)
            {
                currentPath = pathHistory.globalHistory.Last();
                throw;
            }
        }
        private void VerifyHistory()
        {
            if (!pathHistory.globalHistory.Any() || pathHistory.globalHistory.Last() != currentPath)
                pathHistory.UpdateHistory(currentPath);

            if (!pathHistory.localHistory.Contains(currentPath))
                pathHistory.StopShifting();
        }
        //Sugaring<.>

    }
}
