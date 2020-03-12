using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace FileManager.Classes
{
    class FileController
    {
        public Action excActionFile;
        public Action SelectedFileChanged;


        public FileInfo fileInfo   { get; private set; }

        public bool     FileSetted { get; private set; }

        public void SetFile(string path)
        {
            try
            {
                fileInfo = new FileInfo(path);

                FileSetted = true;
                SelectedFileChanged?.Invoke();
            }
            catch
            {
                FileSetted = false;
                excActionFile?.Invoke();
            }
        }
    }
}
