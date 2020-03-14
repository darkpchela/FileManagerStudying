using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FileManager.Classes;

namespace FileManager.Forms
{
     public partial class Form1
     {
        PathController _pathController = new PathController();
        FileController _fileController = new FileController();
        List<string>   SelectedFiles   = new List<string>();

        private void RefreshBuffer()
        {
            checkedListBox_buffer.Items.Clear();
            foreach (var item in _fileController.buffer.files)
            {
                checkedListBox_buffer.Items.Add(item);
            }
        }
        private void RefreshPathText()
        {
            comboBox_path.Text = _pathController.tempPath;
        }
        private void ShowDirectory()
        {
            _pathController.SetPath();
            _pathController.LoadDirectory();
            LoadListView();
            RefreshPathText();

            void LoadListView()
            {
                listView_main.Clear();
                foreach (var item in _pathController.currentLoadedDirectories)
                {
                    listView_main.Items.Add(item, 4);
                }
                foreach (var item in _pathController.currentLoadedFiles)
                {
                    listView_main.Items.Add(item, 3);
                }
            }
        }
        private void ShowFileInfo()
        {
            label_fileName.Text = "";
            if (_fileController.FileSetted)
            {
                label_fileName.Text += _fileController.fileInfo.FullName + " ";
                label_fileName.Text += (((double)_fileController.fileInfo.Length) / 1024 / 1024).ToString("#.##") + "Mb" + " ";
                label_fileName.Text += _fileController.fileInfo.Attributes.ToString() + " ";

                label_fileType.Text  = _fileController.fileInfo.Extension;
            }
        }
        private void ShowDirectoryInfo()
        {
            label_fileName.Text = "";
            if (_fileController.FileSetted)
            {
                label_fileName.Text += _fileController.fileInfo.FullName + " ";

                label_fileType.Text = "Directory";
            }
        }
    }
}
