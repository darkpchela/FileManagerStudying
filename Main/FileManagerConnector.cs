using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FileManager.Classes;
using System.Windows.Forms;

namespace FileManager.Main
{
    class FileManagerConnector
    {
        public FileDistributor fileDistributor      = new FileDistributor();
        public PathController  pathController       = new PathController();
        public FileOperator    fileOperator         = new FileOperator();

        public List<string>    selectedFiles        = new List<string>();
        public List<string>    CheckedFilesBuffer   = new List<string>();
        string tempPath = "";
    }
}
