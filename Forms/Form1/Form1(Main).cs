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
using FileManager.Classes.Etc;

namespace FileManager.Forms.Form1
{
    public partial class Form1 : Form
    {
        FileManagerConnector connector;
        string PathMessage     = "";
        string FilePathMessage = "";
        public Form1()
        {
            InitializeComponent();

            connector = new FileManagerConnector();

            connector.pathController.ExceptionAppeared += ShowExceptionMessage;
            connector.fileController.ExceptionAppeared += ShowExceptionMessage;
            connector.fileDescriptor.ExceptionAppeared   += ShowExceptionMessage;
            connector.fileDescriptor.SelectedFileChanged += ShowFileInfo;

            connector.fileController.SubscribeToAlreadyExistedItemAppearedEvent(ShowOverwriteDialog);

            connector.fileController.BufferItemsChanged     += RefreshBuffer;
            connector.pathController.CurrentPathChanged     += ShowCurrentLoadedDirectory;
            connector.fileController.FileOperationCompleted += ReloadCurrentDirectory;
            connector.pathController.CurrentPathChanged     += RefreshPathText;

            PathMessage = WindowsDrivesInfo.drivesNames.First();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            
            comboBox_drives.Items.AddRange(WindowsDrivesInfo.drivesNames);
            comboBox_drives.SelectedIndex=0;
            
            connector.pathController.SetPath(PathMessage);
        }//OK

        private void comboBox_drives_SelectionChangeCommitted(object sender, EventArgs e)
        {
            PathMessage = comboBox_drives.Text;
            connector.pathController.SetPath(PathMessage);
        }//OK

        private void comboBox_path_SelectionChangeCommitted(object sender, EventArgs e)
        {
            PathMessage = ((ComboBox)sender).SelectedItem.ToString();
            connector.pathController.SetPath(PathMessage);
        }//OK

        private void comboBox_path_DropDown(object sender, EventArgs e)
        {
            comboBox_path.Items.Clear();
            string[] pathHistory = connector.pathController.GetPathHistory();
            if (pathHistory.Length > 0)
            {
                List<string> tempHistoryPath = pathHistory.ToList();
                tempHistoryPath = tempHistoryPath.Distinct().ToList();
                tempHistoryPath.Reverse();
                tempHistoryPath = tempHistoryPath.Take(20).ToList();
                comboBox_path.Items.AddRange(tempHistoryPath.ToArray());
            }

        }

        private void comboBox_path_TextChanged(object sender, EventArgs e)
        {
            SelectedFiles.Clear();
            comboBox_path.Text = comboBox_path.Text.Replace(@"\\", @"\");
            if (comboBox_path.Text != connector.pathController.currentPath)
            { 
                comboBox_path.BackColor = Color.AliceBlue;
                btn_go.Enabled = true;
            }
            else
            { 
                comboBox_path.BackColor = Color.White;
                btn_go.Enabled = false;
            }
        }

        private void btn_Back_Click(object sender, EventArgs e)
        {
            connector.pathController.SetPreviousPath();
        }

        private void btn_up_Click(object sender, EventArgs e)
        {
            connector.pathController.SetParentDirectoryPath();
        }

        private void btn_next_Click(object sender, EventArgs e) 
        {
            connector.pathController.SetNextPath();
        }

        private void btn_go_Click(object sender, EventArgs e)
        {
            PathMessage = comboBox_path.Text;
            connector.pathController.SetPath(PathMessage);
        }

        private void listView_main_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            string currentSelectedItem = e.Item.Text;

            FilePathMessage = connector.pathController.currentPath + "\\" + currentSelectedItem;
            FilePathMessage = FilePathMessage.Replace(@"\\", @"\");
            connector.fileDescriptor.SelectFile(FilePathMessage);

            SelectedFiles.Clear();

            foreach (ListViewItem item in listView_main.SelectedItems)
            {
                SelectedFiles.Add(connector.pathController.currentPath +"\\"+ item.Text);
            }
        }

        private void listView_main_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (PathValidator.IsDirectory(connector.fileDescriptor.currentSelectedFileInfo.FullName))
            {
                PathMessage = FilePathMessage;
                connector.pathController.SetPath(PathMessage);
            }
        }

        private void listView_main_AfterLabelEdit(object sender, LabelEditEventArgs e)
        {
            connector.fileController.Rename(connector.fileDescriptor.currentSelectedFileInfo.FullName, connector.pathController.currentPath + "\\" + e.Label);
        }
        private void checkedListBox_buffer_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            int index = e.Index;

            if (e.CurrentValue == CheckState.Unchecked)
                CheckedFilesBuffer.Add(checkedListBox_buffer.Items[index].ToString());
            else
                CheckedFilesBuffer.Remove(checkedListBox_buffer.Items[index].ToString());
        }
    }
}
