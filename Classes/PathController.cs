using System;
using System.IO;
using System.Linq;
using FileManager.Classes.Etc;

namespace FileManager.Classes
{
    class PathController
    {
        public event MessageEventHandler ExceptionAppeared;
        public event EventHandler        DirectoryLoaded;

        public History         pathHistory;
        public DirectoryLoader directoryLoader;
        public string currentPath { get; private set; }
        
        public PathController()
        {
            pathHistory     = new History();
            directoryLoader = new DirectoryLoader();
        }
        private void OnExceptionAppeared(string message)
        {
            ExceptionAppeared?.Invoke(this, message);
        }
        private void OnDirectoryLoaded(EventArgs e)
        {
            DirectoryLoaded?.Invoke(this, e);
        }
        public void SetPath(string path)
        {
            if (PathValidator.IsDirectory(path))
                currentPath = path.Replace(@"\\", @"\");
            else
                currentPath = pathHistory.globalHistory.Last();
        }

        public void SetParentDirectoryPath()
        {
            try
            {
                currentPath = Directory.GetParent(currentPath).ToString(); 
            }
            catch (Exception ex)
            {
                OnExceptionAppeared(ex.Message); 
            }
        }     
        public void LoadDirectory()
        {
            directoryLoader.SetDirectory(currentPath);

            try
            {
                directoryLoader.LoadDirectory();

                if (!pathHistory.globalHistory.Any() || pathHistory.globalHistory.Last() != currentPath)
                    pathHistory.UpdateHistory(currentPath);

                EventArgs e = new EventArgs();
                OnDirectoryLoaded(e);
            }
            catch(Exception ex)
            {
                currentPath = pathHistory.globalHistory.Last();
                OnExceptionAppeared(ex.Message);
            }
        }

    }
}
