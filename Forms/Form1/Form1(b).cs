using System;
using System.Collections.Generic;
using System.Windows.Forms;
using FileManager.Classes;
using FileManager.Classes.Etc;

namespace FileManager.Forms.Form1
{
     public partial class Form1
     {
        //FileDistributor fileDistributor     = new FileDistributor();
        //PathController pathController      = new PathController();
        //FileOperator   fileOperator        = new FileOperator();
        List<string>   SelectedFiles       = new List<string>();
        List<string>   CheckedFilesBuffer  = new List<string>();
        //----------------------------------
        MessageBoxButtons messageBoxButtons;
        DialogResult dialogResult;
        string message;
        string caption;
        //------------------------------------
        string tempPath = "";

        private void RefreshBuffer()
        {
            checkedListBox_buffer.Items.Clear();
            foreach (var item in connector.fileDistributor.GetFilesFromBuffer())
            {
                checkedListBox_buffer.Items.Add(item);
            }
        }
        private void RefreshPathText()
        {
            comboBox_path.Text = connector.pathController.currentPath;
        }
        private void LoadListView()
        {
            listView_main.Clear();
            foreach (var item in connector.pathController.directoryLoader.directories)
            {
                listView_main.Items.Add(item.Name, 4);
            }
            foreach (var item in connector.pathController.directoryLoader.files)
            {
                listView_main.Items.Add(item.Name, 3);
            }
        }
        private void LoadDirectory()
        {
            connector.pathController.SetPath(tempPath);
            connector.pathController.LoadDirectory();
            LoadListView();
            RefreshPathText();
        }
        private void ReloadDirectory()
        {
            connector.pathController.LoadDirectory();
            LoadListView();
            RefreshPathText();
        }
        private void ShowFileInfo(object sender, EventArgs e)
        {
            label_fileName.Text = "";
            if (connector.fileOperator.FileSelected)
            {
                label_fileName.Text += (((double)connector.fileOperator.currentSelectedFileInfo.Length) / 1024 / 1024).ToString("#.##") + "Mb" + " ";
                label_fileName.Text += connector.fileOperator.currentSelectedFileInfo.Attributes.ToString() + " ";

                label_fileType.Text  = connector.fileOperator.currentSelectedFileInfo.Extension;
            }
        }
        private void ShowDirectoryInfo(object sender, EventArgs e)
        {
            label_fileName.Text = "";
            if (connector.fileOperator.FileSelected)
            {
                label_fileType.Text = "Directory";
            }
        }


        private DialogOptions ShowOverwriteDialog(object sender, ExistedItemAppearedEventArgs e)
        {
            messageBoxButtons = MessageBoxButtons.YesNoCancel;
            caption = "Copying";

            if (!e.multipleProcces)
            {
                message = "This file already exist: \n" + e.currentCollision.Item1 + "\nOverwrite it by:\n" + e.currentCollision.Item2;
                dialogResult = MessageBox.Show(message, caption, messageBoxButtons, MessageBoxIcon.Question);
                switch (dialogResult)
                {
                    case DialogResult.Yes:
                        return DialogOptions.Yes;

                    case DialogResult.No:
                        return DialogOptions.No;

                    default:
                        return DialogOptions.Cancel;
                }
            }
            else
            {
                message = $"Some files({e.collisions.Length}) already existed.\n Current file: {e.currentCollision.Item2}\n Overwrite it by: {e.currentCollision.Item1}?";
                dialogResult = MessageBox.Show(message, caption, messageBoxButtons, MessageBoxIcon.Question);

                if ((dialogResult == DialogResult.Yes || dialogResult == DialogResult.No))
                    return ShowSubDialog(dialogResult);
                else
                    return DialogOptions.Cancel;
            }

            DialogOptions ShowSubDialog(DialogResult dialogResult)
            {
                DialogResult subDialogResult = new DialogResult();
                message = "Use selected option to all other collisions?";
                subDialogResult = MessageBox.Show(message, caption, messageBoxButtons);
                switch (subDialogResult)
                {
                    case DialogResult.Yes:
                        if (dialogResult == DialogResult.Yes)
                            return DialogOptions.YesToAll;
                        else
                            if (dialogResult == DialogResult.No)
                            return DialogOptions.NoToAll;
                        else
                            goto default;

                    case DialogResult.No:
                        if (dialogResult == DialogResult.Yes)
                            return DialogOptions.Yes;
                        else
                            if (dialogResult == DialogResult.No)
                            return DialogOptions.No;
                        else
                            goto default;

                    default:
                        return DialogOptions.Cancel;

                }
            }
        }//Need be checked later
        private void ShowExceptionMessage(object sender, string message)
        {
            MessageBox.Show(message, "Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
