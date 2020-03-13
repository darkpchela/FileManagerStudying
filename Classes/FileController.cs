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

        public FileInfo     fileInfo      { get; private set; }
        public FileBuffer   buffer        { get; private set; } = new FileBuffer();
        public bool         FileSetted    { get; private set; }

        private string FileSettedPath;

        public void SetFile(string path)
        {
            try
            {
                fileInfo = new FileInfo(path);

                FileSetted = true;
                FileSettedPath = path;
                SelectedFileChanged?.Invoke();
            }
            catch
            {
                FileSetted = false;
                excActionFile?.Invoke();
            }
        }
        public void AddFileToBuffer()
        {
            if (FileSetted)
            {
                buffer.Add(fileInfo);
            }
        }
        public void AddRangeOfFilesToBuffer(List<string> paths)
        {
            foreach (var item in paths)
            {
                if (!buffer.files.Contains(item))
                {
                    SetFile(item);
                    AddFileToBuffer();
                }
            }
        }
    }
}
