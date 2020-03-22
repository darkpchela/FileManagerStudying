using System;
using System.Collections.Generic;
using FileManager.Classes;

namespace FileManager.Forms.Form1
{
     public partial class Form1
     {
        PathController _pathController    = new PathController();
        FileController _fileController    = new FileController();
        List<string>   SelectedFiles      = new List<string>();
        List<string>   BufferSelectedFile = new List<string>();

        string tempPath = "";

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
            comboBox_path.Text = _pathController.currentPath;
        }
        private void LoadListView()
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
        private void LoadDirectory()
        {
            _pathController.SetPath(tempPath);
            _pathController.LoadDirectory();
            LoadListView();
            RefreshPathText();
        }
        private void ReloadDirectory()
        {
            _pathController.LoadDirectory();
            LoadListView();
            RefreshPathText();
        }
        private void ShowFileInfo()
        {
            label_fileName.Text = "";
            if (_fileController.FileSelected)
            {
                textBox_fileName.Text = _fileController.fileInfo.Name;
                label_fileName.Text += (((double)_fileController.fileInfo.Length) / 1024 / 1024).ToString("#.##") + "Mb" + " ";
                label_fileName.Text += _fileController.fileInfo.Attributes.ToString() + " ";

                label_fileType.Text  = _fileController.fileInfo.Extension;
            }
        }
        private void ShowDirectoryInfo()
        {
            label_fileName.Text = "";
            if (_fileController.FileSelected)
            {
                textBox_fileName.Text = _fileController.fileInfo.Name;

                label_fileType.Text = "Directory";
            }
        }
    }
}
