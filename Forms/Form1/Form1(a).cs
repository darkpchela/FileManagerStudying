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

namespace FileManager.Forms.Form1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            _pathController.excActionPath += () => MessageBox.Show("Not accessible path!");
            _fileController.SelectedDirectoryChanged += ShowDirectoryInfo;
            _fileController.SelectedFileChanged += ShowFileInfo;

            comboBox_drives.Items.AddRange(WindowsDrivesInfo.drivesNames);
            comboBox_drives.SelectedIndex = 0;

            tempPath = WindowsDrivesInfo.drivesNames.First();

            LoadDirectory();
        }

        private void comboBox_drives_SelectionChangeCommitted(object sender, EventArgs e)
        {
            tempPath = comboBox_drives.Text;

            _pathController.pathHistory.StopShifting();
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
            if (_pathController.pathHistory.globalHistory.Count > 0)
            {
                List<string> tempHistoryPath = _pathController.pathHistory.globalHistory;
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
            if (comboBox_path.Text != _pathController.currentPath)
            { comboBox_path.BackColor = Color.AliceBlue; }
            else
            { comboBox_path.BackColor = Color.White; }
        }

        private void btn_Back_Click(object sender, EventArgs e)
        {
            tempPath = _pathController.pathHistory.GetPreviousElement();

            LoadDirectory();
        }

        private void btn_up_Click(object sender, EventArgs e)
        {
            _pathController.SetParentDirectoryPath();
            ReloadDirectory();
        }

        private void btn_next_Click(object sender, EventArgs e)
        {
            tempPath = _pathController.pathHistory.GetNextElement();
            LoadDirectory();
        }

        private void btn_go_Click(object sender, EventArgs e)
        {
            if (SelectedFiles.Any())
                tempPath = SelectedFiles.Last();
            else
                tempPath = comboBox_path.Text;

            _pathController.pathHistory.StopShifting();
            LoadDirectory();
        }

        private void listView_main_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            string currentSlectedItem = e.Item.Text;

            tempPath = _pathController.currentPath + "\\" + currentSlectedItem;
            tempPath = tempPath.Replace(@"\\", @"\");

            if (!_pathController.direcoryLoader.IsDirectory(tempPath))
            { comboBox_path.Text = _pathController.currentPath; }
            //------------------------------------
            if (e.IsSelected)
            { SelectedFiles.Add(tempPath); }
            else
            { SelectedFiles.Remove(tempPath); }
            //-------------------------------------
            _fileController.SelectFile(tempPath);
        }

        private void listView_main_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            _pathController.pathHistory.StopShifting();
            LoadDirectory();
        }

        private void textBox_fileName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                _fileController.manager.Rename(SelectedFiles.Last(), _pathController.currentPath + "\\" + textBox_fileName.Text);
                textBox_fileName.Enabled = false;
            }
            if (e.KeyCode == Keys.Escape)
            {
                textBox_fileName.Enabled = false;
            }
            ReloadDirectory();
        }

        private void debugButtonToolStripMenuItem_Click(object sender, EventArgs e) //Delete later
        {
            //List<System.IO.DirectoryInfo> dirs = new List<System.IO.DirectoryInfo>();
            //List<System.IO.FileInfo> files = new List<System.IO.FileInfo>();
            //_fileController.manager.AllFilesAndDirectoriesFromDirectory(SelectedFiles.Last(), ref files, ref dirs);
            //checkedListBox_buffer.Items.Clear();
            //foreach (var item in dirs)
            //{
            //    checkedListBox_buffer.Items.Add(item.FullName);
            //}

            //foreach (var item in files)
            //{
            //    checkedListBox_buffer.Items.Add(item.FullName);
            //}
        }
    }
}
