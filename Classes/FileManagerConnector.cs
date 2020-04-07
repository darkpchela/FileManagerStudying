using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FileManager.Classes;

namespace FileManager.Classes
{
    class FileManagerConnector
    {
        public FileController fileController;
        public PathController pathController;
        public FileDescriptor fileDescriptor;

        

        public FileManagerConnector()
        {
            fileController = new FileController();
            pathController = new PathController();
            fileDescriptor = new FileDescriptor();
        }
    }
}
