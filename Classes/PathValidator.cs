using System.IO;

namespace FileManager.Classes
{
    static class PathValidator
    {
        private static FileAttributes fileAttributes;
        static public bool IsDirectory(string path)
        {
            fileAttributes = File.GetAttributes(path);

            if (fileAttributes.HasFlag(FileAttributes.Directory))
                return true;
            else
                return false;
        }

        static public bool IsNormalFile(string path)
        {
            fileAttributes = File.GetAttributes(path);

            if (fileAttributes.HasFlag(FileAttributes.Normal))
                return true;
            else
                return false;
        }

        static public bool IsHidden(string path)
        {
            fileAttributes = File.GetAttributes(path);

            if (fileAttributes.HasFlag(FileAttributes.Hidden))
                return true;
            else
                return false;
        }
        static public bool IsReadOnly(string path)
        {
            fileAttributes = File.GetAttributes(path);

            if (fileAttributes.HasFlag(FileAttributes.ReadOnly))
                return true;
            else
                return false;
        }
        static public bool IsSystemFile(string path)
        {
            fileAttributes = File.GetAttributes(path);

            if (fileAttributes.HasFlag(FileAttributes.System))
                return true;
            else
                return false;
        }
        static public bool IsCompressed(string path)
        {
            fileAttributes = File.GetAttributes(path);

            if (fileAttributes.HasFlag(FileAttributes.Compressed))
                return true;
            else
                return false;
        }
        static public bool Exists(string path)
        {
            if (File.Exists(path))
                return true;
            else
                return false;
        }

        static public void FileCopyPathUpdate(ref string path)
        {
            FileInfo file = new FileInfo(path);

            int index = file.FullName.IndexOf(" -copy");

            if (index>0)
            {
                string realName = file.FullName.Remove(index, file.FullName.Length - index);

                if (File.Exists(realName)||Directory.Exists(realName)|| File.Exists(realName + " -copy") || Directory.Exists(realName + " -copy"))
                {
                    int num = 0;
                    string temp = path;
                    while (File.Exists(temp)||Directory.Exists(temp))
                    {
                        num++;
                        if (num == 1)
                            temp = realName + " -copy";
                        else
                            temp = realName + " -copy" + "(" + num + ")";
                    }
                    path = temp;
                }
            }
            
        }
    }
}
