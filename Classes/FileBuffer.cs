using System.Collections.Generic;
using System.IO;

namespace FileManager.Classes
{
    class FileBuffer
    {
        public List<string> files { get; private set; } = new List<string>();

        public void Add(string path)
        {
            files.Add(path);
        }
        public void Add(FileInfo file)
        {
            files.Add(file.FullName);
        }

        public void Remove(string path)
        {
            files.Remove(path);
        }
        public void Remove(FileInfo file)
        {
            files.Remove(file.FullName);
        }

        public void Clear()
        {
            files.Clear();
        }
    }
}
