using System;
using System.Collections.Generic;
using System.Windows.Forms;
using FileManager.Classes;

namespace FileManager.Forms.Form1
{
     public partial class Form1
     {
        PathController pathController     = new PathController();
        FileController fileController     = new FileController();
        List<string>   SelectedFiles      = new List<string>();
        List<string>   BufferSelectedFile = new List<string>();

        string tempPath = "";

        private void RefreshBuffer()
        {
            checkedListBox_buffer.Items.Clear();
            foreach (var item in fileController.buffer.files)
            {
                checkedListBox_buffer.Items.Add(item);
            }
        }
        private void RefreshPathText()
        {
            comboBox_path.Text = pathController.currentPath;
        }
        private void LoadListView()
        {
            listView_main.Clear();
            foreach (var item in pathController.currentLoadedDirectories)
            {
                listView_main.Items.Add(item, 4);
            }
            foreach (var item in pathController.currentLoadedFiles)
            {
                listView_main.Items.Add(item, 3);
            }
        }
        private void LoadDirectory()
        {
            pathController.SetPath(tempPath);
            pathController.LoadDirectory();
            LoadListView();
            RefreshPathText();
        }
        private void ReloadDirectory()
        {
            pathController.LoadDirectory();
            LoadListView();
            RefreshPathText();
        }
        private void ShowFileInfo()
        {
            label_fileName.Text = "";
            if (fileController.FileSelected)
            {
                textBox_fileName.Text = fileController.fileInfo.Name;
                label_fileName.Text += (((double)fileController.fileInfo.Length) / 1024 / 1024).ToString("#.##") + "Mb" + " ";
                label_fileName.Text += fileController.fileInfo.Attributes.ToString() + " ";

                label_fileType.Text  = fileController.fileInfo.Extension;
            }
        }
        private void ShowDirectoryInfo()
        {
            label_fileName.Text = "";
            if (fileController.FileSelected)
            {
                textBox_fileName.Text = fileController.fileInfo.Name;

                label_fileType.Text = "Directory";
            }
        }


        private OverwriteOptions OverwriteDialog()
        {
            messageBoxButtons = MessageBoxButtons.YesNoCancel;
            caption = "Copying";
            message = "Some files already exist. Overwrite them?";

            dialogResult = MessageBox.Show(message, caption, messageBoxButtons);

            switch (dialogResult)
            {
                case DialogResult.Yes:
                    return OverwriteOptions.Yes;

                case DialogResult.No:
                    return OverwriteOptions.No;

                case DialogResult.Cancel:
                    return OverwriteOptions.Cancel;

                default:
                    return OverwriteOptions.Cancel;
            }
        }
    }
}
