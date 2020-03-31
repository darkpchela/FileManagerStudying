using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FileManager.Classes;

namespace FileManager.Forms.Form1
{
    public partial class Form1
    {
        MessageBoxButtons messageBoxButtons;
        DialogResult      dialogResult;
        string            message;
        string            caption;

        private void toolStripMenuItem_add_Click(object sender, EventArgs e)//OK
        {
            fileController.AddFilesToBuffer(SelectedFiles);
            SelectedFiles.Clear();
            RefreshBuffer();
        }
        private void ToolStripMenuItem_copy_Click(object sender, EventArgs e)
        {
            foreach (var file in CheckedFilesBuffer)
            {
                fileController.fileDistributor.Copy(file.ToString(), pathController.currentPath);
            }
            ReloadDirectory();
            RefreshBuffer();
            CheckedFilesBuffer.Clear();
        }//OK

        private void toolStripMenuItem_cut_Click(object sender, EventArgs e)
        {
            foreach (var file in CheckedFilesBuffer)
            {
                fileController.fileDistributor.Move(file, pathController.currentPath); ;
            }
            ReloadDirectory();
            RefreshBuffer();
            CheckedFilesBuffer.Clear();
        }

        private void ToolStripMenuItem_rename_Click(object sender, EventArgs e)//OK
        {
            listView_main.SelectedItems[listView_main.SelectedItems.Count - 1].BeginEdit();
        }

        private void ToolStripMenuItem_createFolder_Click(object sender, EventArgs e)
        {
            string name = "New folder";
            fileController.fileDistributor.CreateDirectory(pathController.currentPath, name);
            ReloadDirectory();
        }//OK

        private void ToolStripMenuItem_createFile_Click(object sender, EventArgs e)
        {

        }//Empty

        private void ToolStripMenuItem_delete_Click(object sender, EventArgs e)
        {
            if (SelectedFiles.Any())
            {
                messageBoxButtons = MessageBoxButtons.YesNo;
                caption           = "Deleting";
                message           = "Are you realy want to delete selected items?";

                dialogResult = MessageBox.Show(message, caption, messageBoxButtons);

                if (dialogResult == DialogResult.Yes)
                {
                    foreach (var item in SelectedFiles)
                    {
                        fileController.fileDistributor.Delete(item);
                    }
                }
                else
                    { SelectedFiles.Clear(); }
               
            }

            ReloadDirectory();
        }//OK
        private void clearBufferToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fileController.buffer.Clear();
            CheckedFilesBuffer.Clear();
            RefreshBuffer();
        }//OK
        private void removeSelectedItemsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (var item in CheckedFilesBuffer)
            {
                fileController.buffer.Remove(item);
            }
            RefreshBuffer();
        }//OK
    }
}
