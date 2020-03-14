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
        public event Action excActionFile;
        public event Action SelectedFileChanged;
        public event Action SelectedDirectoryChanged;

        public Manager manager = new Manager();

        public FileInfo      fileInfo      { get; private set; }
        public DirectoryInfo directoryInfo { get; private set; }
        public FileBuffer    buffer        { get; private set; } = new FileBuffer();

        public bool          FileSetted    { get; private set; }

        public void SetFile(string path)//Maybe rebuild
        {
            try
            {
                fileInfo = new FileInfo(path);
                FileSetted = true;

                if (fileInfo.Attributes.HasFlag(FileAttributes.Directory))
                { SelectedDirectoryChanged?.Invoke(); }
                else
                { SelectedFileChanged?.Invoke();      }
            }
            catch
            {
                FileSetted = false;

                excActionFile?.Invoke();
            }
        }
        public void AddFilesToBuffer(List<string> paths)
        {
            foreach (var item in paths)
            {
                if (!buffer.files.Contains(item))
                {
                    SetFile(item);
                    if (FileSetted)
                    { buffer.Add(fileInfo); }
                }
            }
        }
    }
}
