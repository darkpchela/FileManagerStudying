using System;
using System.IO;
using System.Linq;

namespace FileManager.Classes
{
    class PathController
    {
        public Action excActionPath;

        public History         pathHistory     = new History();
        public DirectoryLoader direcoryLoader  = new DirectoryLoader();
        public  string[] currentLoadedFiles         { get { return _currentLoadedFiles;       } set { } }
        public  string[] currentLoadedDirectories   { get { return _currentLoadedDirectories; } set { } }

        private string[] _currentLoadedFiles;
        private string[] _currentLoadedDirectories;
        public string currentPath { get; private set; }
        public string tempPath    { get;         set; }

        public void SetPath()
        {
            if (IsAccessiblePath() && direcoryLoader.IsDirectory(tempPath))
                { currentPath = tempPath.Replace(@"\\", @"\");    }
            else
                { currentPath = pathHistory.globalHistory.Last(); }
        }

        public void SetParentDirectoryPath()
        {
            try   { tempPath = Directory.GetParent(currentPath).ToString(); }
            catch { excActionPath?.Invoke(); }
        }
        public bool IsAccessiblePath()
        {
            try   { Directory.GetFiles(tempPath);   return true;  }
            catch { excActionPath?.Invoke();        return false; }
        }
        
        public void LoadDirectory()
        {
            direcoryLoader.LoadDirectory(currentPath, ref _currentLoadedFiles, ref _currentLoadedDirectories); 

            if (!pathHistory.globalHistory.Any())
                { pathHistory.UpdateHistory(currentPath); }
            else
                if (pathHistory.globalHistory.Last() != currentPath)
                    { pathHistory.UpdateHistory(currentPath); }  
        }
    }
}
