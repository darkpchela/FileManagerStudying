using System;
using System.Collections.Generic;
using System.Windows.Forms;
using FileManager.Classes;
using FileManager.Classes.Etc;

namespace FileManager.Forms.Form1
{
     public partial class Form1
     {
        List<string>   SelectedFiles       = new List<string>();
        List<string>   CheckedFilesBuffer  = new List<string>();

        private void RefreshBuffer(object sender, EventArgs e)
        {
            checkedListBox_buffer.Items.Clear();
            foreach (var item in connector.fileController.GetFilesFromBuffer())
            {
                checkedListBox_buffer.Items.Add(item);
            }
        }
        private void RefreshPathText(object sender, EventArgs e)
        {
            comboBox_path.Text = connector.pathController.currentPath;
        }
        private void ShowCurrentLoadedDirectory(object sender, EventArgs e)
        {
            listView_main.Clear();
            foreach (var item in connector.pathController.directoryDescriptor.childDirectories)
            {
                listView_main.Items.Add(item.Name, 4);
            }
            foreach (var item in connector.pathController.directoryDescriptor.childFiles)
            {
                listView_main.Items.Add(item.Name, 3);
            }
        }
        private void ReloadCurrentDirectory(object sender, EventArgs e)
        {
            connector.pathController.SetPath(connector.pathController.currentPath);
        }
        private void ShowFileInfo(object sender, SelectedFileChangedEventArgs e)
        {
            label_fileName.Text = "";
            if (connector.fileDescriptor.FileSelected)
            {
                if (e.isDirectory)
                {

                }
                else
                {
                    label_fileName.Text += (((double)connector.fileDescriptor.currentSelectedFileInfo.Length) / 1024 / 1024).ToString("#.##") + "Mb" + " ";
                    label_fileName.Text += connector.fileDescriptor.currentSelectedFileInfo.Attributes.ToString() + " ";

                    label_fileType.Text = connector.fileDescriptor.currentSelectedFileInfo.Extension;
                }
            }
        }
        private void RefreshHistoryInfo(object sender, EventArgs e)
        {

        }

        //Form Messages<>
        MessageBoxButtons messageBoxButtons;
        DialogResult dialogResult;
        string message;
        string caption;
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
        //Form Messages<.>
    }
}
