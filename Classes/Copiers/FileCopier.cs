using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using FileManager.Classes.Etc;

namespace FileManager.Classes.Copiers
{
    class FileCopier
    { 
        public FileInfo currentFileInfo { get; private set; }

        public void SetFile(FileInfo file)
        {
            currentFileInfo = file;
        }
        public void SetFile(string path)
        {
            currentFileInfo = new FileInfo(path);
        }
        public bool TryCopy(string toDirectory, bool overwrite=false)
        {

            try
            {
                string newPath = Path.Combine(toDirectory, currentFileInfo.Name);

                if (currentFileInfo.FullName.Contains(" -copy"))
                {
                    PathValidator.FileCopyPathUpdate(ref newPath);
                }
                else
                if (currentFileInfo.FullName.Equals(newPath))
                {
                    newPath = newPath + " -copy";
                    PathValidator.FileCopyPathUpdate(ref newPath);
                }

                currentFileInfo.CopyTo(newPath, overwrite);

                return true;
            }
            catch
            { return false; }

        }

    }
}
