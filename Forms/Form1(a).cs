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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            _pathController.excActionPath += () => MessageBox.Show("Not accessible path!");
            _fileController.SelectedFileChanged += ShowFileInfo;

            comboBox_drives.Items.AddRange(WindowsDrivesInfo.drivesNames);
            comboBox_drives.SelectedIndex = 0;

            comboBox_path.Text       = comboBox_drives.SelectedItem.ToString();

            _pathController.tempPath = comboBox_path.Text;

            ShowDirectory();
        }

        private void comboBox_drives_SelectionChangeCommitted(object sender, EventArgs e)
        {
            _pathController.tempPath = comboBox_drives.Text;

            _pathController.pathHistory.StopShifting();
            RefreshPathText();
            ShowDirectory();
        }

        private void comboBox_path_SelectionChangeCommitted(object sender, EventArgs e)
        {
            _pathController.tempPath = ((ComboBox)sender).SelectedItem.ToString();

            RefreshPathText();
            ShowDirectory();
        }

        private void comboBox_path_DropDown(object sender, EventArgs e)
        {
            comboBox_path.Items.Clear();
            if (_pathController.pathHistory.globalHistory.Count>0)
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
            comboBox_path.Text = comboBox_path.Text.Replace(@"\\", @"\");
        }

        private void btn_Back_Click(object sender, EventArgs e)
        {
            _pathController.tempPath = _pathController.pathHistory.GetPreviousElement();

            RefreshPathText();
            ShowDirectory();
        }

        private void btn_up_Click(object sender, EventArgs e)
        {
            _pathController.SetParentDirectoryPath();

            RefreshPathText();
            ShowDirectory();
        }

        private void btn_next_Click(object sender, EventArgs e)
        {
            _pathController.tempPath = _pathController.pathHistory.GetNextElement();
            RefreshPathText();
            ShowDirectory();
        }

        private void btn_go_Click(object sender, EventArgs e)
        {
            _pathController.tempPath = comboBox_path.Text;
            
            _pathController.pathHistory.StopShifting();
            ShowDirectory();
        }

        private void listView_main_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            string currentSlectedItem = e.Item.Text;

            _pathController.tempPath = _pathController.currentPath + "\\" + currentSlectedItem;
            _pathController.tempPath = _pathController.tempPath.Replace(@"\\", @"\");

            if (_pathController.direcoryLoader.IsDirectory(_pathController.tempPath))
            { RefreshPathText(); label_fileName.Text = ""; label_fileType.Text = ""; }
            else
            { comboBox_path.Text = _pathController.currentPath; }
            //------------------------------------
            if (e.IsSelected)
            {
                SelectedFiles.Add(_pathController.tempPath);
            }
            else
            {
                SelectedFiles.Remove(_pathController.tempPath);
            }
            //-------------------------------------

            _fileController.SetFile(_pathController.tempPath);
        }

        private void listView_main_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ShowDirectory();
        }

        private void toolStripMenuItem_add_Click(object sender, EventArgs e)
        {
            _fileController.AddRangeOfFilesToBuffer(SelectedFiles);
            SelectedFiles.Clear();
            RefreshBuffer();
        }
    }
}
