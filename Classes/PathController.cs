using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManager.Classes
{
    class PathController
    {
        public Action excActionPath;

        public PathHistory pathHistory = new PathHistory();
        public DirectoryLoader loader  = new DirectoryLoader();
        public  string[] currentLoadedFiles         { get { return _currentLoadedFiles; }       set { } }
        public  string[] currentLoadedDirectories   { get { return _currentLoadedDirectories; } set { } }

        private string[] _currentLoadedFiles;
        private string[] _currentLoadedDirectories;
        public string currentPath { get; private set; }

        public void SetPath(string path)
        {
            if (Directory.Exists(path))
                currentPath = path.Replace(@"\\", @"\"); 
        }

        public void SetParentDirectoryPath()
        {
            try   { currentPath = Directory.GetParent(currentPath).ToString(); }
            catch { excActionPath(); }
        }
        public bool IsAccessiblePath(string path)
        {
            try   { Directory.GetFiles(path);   return true;  }
            catch { excActionPath();            return false; }
        }
        
        public void LoadDirectory()
        {
            if (IsAccessiblePath(currentPath) & loader.IsDirectory(currentPath))
                { loader.LoadDirectory(currentPath, ref _currentLoadedFiles, ref _currentLoadedDirectories); }
            else 
                { currentPath = pathHistory.globalHistory.Last(); }

            if (!pathHistory.globalHistory.Any())
                { pathHistory.UpdateHistory(currentPath); }
            else
                if (pathHistory.globalHistory.Last() != currentPath)
                    { pathHistory.UpdateHistory(currentPath); }  
        }
    }
}
