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
using FileManager.Main;

namespace FileManager.Forms.Form1
{
    public partial class Form1 : Form
    {
        FileManagerConnector connector = new FileManagerConnector();
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            connector.pathController.ExceptionAppeared      += ShowExceptionMessage;
            connector.fileDistributor.ExceptionAppeared     += ShowExceptionMessage;
            connector.fileOperator.ExceptionAppeared        += ShowExceptionMessage;
            connector.fileOperator.SelectedDirectoryChanged += ShowDirectoryInfo;
            connector.fileOperator.SelectedFileChanged      += ShowFileInfo;
            connector.fileDistributor.SubscribeToAlreadyExistedItemAppearedEvent(ShowOverwriteDialog);

            //pathController.excActionPath                  += ShowExceptionMessage;
            //fileDistributor.exActionManager  += ShowExceptionMessage;
            //fileOperator.SelectedDirectoryChanged         += ShowDirectoryInfo;
            //fileOperator.SelectedFileChanged              += ShowFileInfo;
            //fileDistributor.SubscribeToAlreadyExistedItemAppearedEvent(ShowOverwriteDialog);

            comboBox_drives.Items.AddRange(WindowsDrivesInfo.drivesNames);
            comboBox_drives.SelectedIndex = 0;

            tempPath = WindowsDrivesInfo.drivesNames.First();

            LoadDirectory();
        }

        private void comboBox_drives_SelectionChangeCommitted(object sender, EventArgs e)
        {
            tempPath = comboBox_drives.Text;

            connector.pathController.pathHistory.StopShifting();
            LoadDirectory();
        }

        private void comboBox_path_SelectionChangeCommitted(object sender, EventArgs e)
        {
            tempPath = ((ComboBox)sender).SelectedItem.ToString();

            LoadDirectory();
        }

        private void comboBox_path_DropDown(object sender, EventArgs e)
        {
            comboBox_path.Items.Clear();
            if (connector.pathController.pathHistory.globalHistory.Count > 0)
            {
                List<string> tempHistoryPath = connector.pathController.pathHistory.globalHistory;
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
            { comboBox_path.BackColor = Color.AliceBlue; }
            else
            { comboBox_path.BackColor = Color.White; }
        }

        private void btn_Back_Click(object sender, EventArgs e)
        {
            tempPath = connector.pathController.pathHistory.GetPreviousElement();

            LoadDirectory();
        }

        private void btn_up_Click(object sender, EventArgs e)
        {
            connector.pathController.SetParentDirectoryPath();
            ReloadDirectory();
        }

        private void btn_next_Click(object sender, EventArgs e)
        {
            tempPath = connector.pathController.pathHistory.GetNextElement();
            LoadDirectory();
        }

        private void btn_go_Click(object sender, EventArgs e)
        {
            if (SelectedFiles.Any())
                tempPath = SelectedFiles.Last();
            else
                tempPath = comboBox_path.Text;

            connector.pathController.pathHistory.StopShifting();
            LoadDirectory();
        }

        private void listView_main_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            string currentSelectedItem = e.Item.Text;

            tempPath = connector.pathController.currentPath + "\\" + currentSelectedItem;
            tempPath = tempPath.Replace(@"\\", @"\");

            SelectedFiles.Clear();

            foreach (ListViewItem item in listView_main.SelectedItems)
            {
                SelectedFiles.Add(connector.pathController.currentPath +"\\"+ item.Text);
            }
        }

        private void listView_main_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            connector.pathController.pathHistory.StopShifting();
            LoadDirectory();
        }

        private void listView_main_AfterLabelEdit(object sender, LabelEditEventArgs e)
        {
            connector.fileDistributor.Rename(SelectedFiles.Last(), connector.pathController.currentPath + "\\" + e.Label);
            ReloadDirectory();
        }

        private void debugButtonToolStripMenuItem_Click(object sender, EventArgs e) //Delete later
        {
            //List<string> dirs  = new List<string>();
            //List<string> files = new List<string>();
            //_fileController.manager.CopyDirectory(SelectedFiles.Last(), _pathController.currentPath+"\\ttt", out files, out dirs);
            //checkedListBox_buffer.Items.Clear();
            //foreach (var item in dirs)
            //{
            //    checkedListBox_buffer.Items.Add(item);
            //}

            //foreach (var item in files)
            //{
            //    checkedListBox_buffer.Items.Add(item);
            //}
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
