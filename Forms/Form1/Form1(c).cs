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
            foreach (var file in checkedListBox_buffer.CheckedItems)
            {
                fileController.manager.CopyAsync(file.ToString(), pathController.currentPath);
            }
            ReloadDirectory();
            RefreshBuffer();
        }//OK

        private void toolStripMenuItem_cut_Click(object sender, EventArgs e)
        {
            string[] checkedFiles = new string[checkedListBox_buffer.CheckedItems.Count];
            checkedListBox_buffer.CheckedItems.CopyTo(checkedFiles,0);
            foreach (var file in checkedFiles)
            {
                fileController.manager.Move(file, pathController.currentPath);
            }
            ReloadDirectory();
            RefreshBuffer();
        }

        private void ToolStripMenuItem_rename_Click(object sender, EventArgs e)//OK
        {
            textBox_fileName.Enabled = true;
        }

        private void ToolStripMenuItem_createFolder_Click(object sender, EventArgs e)
        {
            string name = "New folder";
            fileController.manager.CreateDirectory(pathController.currentPath, name);
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
                        fileController.manager.Delete(item);
                    }
                }
                else
                    { SelectedFiles.Clear(); }
               
            }

            ReloadDirectory();
        }//OK
    }
}
