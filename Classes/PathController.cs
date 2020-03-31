using System;
using System.IO;
using System.Linq;
using FileManager.Classes.Etc;

namespace FileManager.Classes
{
    class PathController
    {
        public MessageHandler excActionPath;

        public History         pathHistory     = new History();
        public DirectoryLoader directoryLoader;
        public string currentPath { get; private set; }

        public void SetPath(string path)
        {
            if (PathValidator.IsDirectory(path))
                { currentPath = path.Replace(@"\\", @"\");        }
            else
                { currentPath = pathHistory.globalHistory.Last(); }
        }

        public void SetParentDirectoryPath()
        {
            try   { currentPath = Directory.GetParent(currentPath).ToString(); }
            catch (Exception ex)
            { excActionPath?.Invoke(ex.Message); }
        }     
        public void LoadDirectory()
        {
            directoryLoader      = new DirectoryLoader(currentPath);
            bool directoryLoaded = directoryLoader.TryLoadDirectory();

            if (directoryLoaded)
            {
                if (!pathHistory.globalHistory.Any() || pathHistory.globalHistory.Last() != currentPath)
                    pathHistory.UpdateHistory(currentPath);
            }
            else
            {
                currentPath = pathHistory.globalHistory.Last();
                excActionPath?.Invoke("Not accessible path!");
            }
        }

    }
}
