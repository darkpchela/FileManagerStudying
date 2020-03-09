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

namespace FileManager
{
    public partial class Form1 : Form
    {
        string tempPath = "";
        DirectoryController _directoryController = new DirectoryController();
        string[] tempFiles;
        string[] tempDirectories;
        public Form1()
        {
            InitializeComponent();
        }

        private void TryAcceptChanges()
        {
            if (_directoryController.IsAccessiblePath(tempPath))
            {
                _directoryController.SetPath(tempPath);
                LoadListView();
            }
            void LoadListView()
            {
                listView_main.Clear();
                _directoryController.LoadDirectory(out tempFiles, out tempDirectories);
                foreach (var item in tempDirectories)
                {
                    listView_main.Items.Add(item);
                }
                foreach (var item in tempFiles)
                {
                    listView_main.Items.Add(item);
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            _directoryController.excActionPath += () => MessageBox.Show("Not accessible path!");
            comboBox_drives.Items.AddRange(WindowsDrivesInfo.drivesNames);
            comboBox_drives.SelectedIndex = 0;
            comboBox_path.Text = comboBox_drives.SelectedItem.ToString();
            tempPath = comboBox_path.Text;
            TryAcceptChanges();
        }

        private void btn_go_Click(object sender, EventArgs e)
        {
            tempPath = comboBox_path.Text;
            TryAcceptChanges();
        }

        private void listView_main_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            string currentSlectedItem = e.Item.Text;

            tempPath = _directoryController.currentPath +"\\" + currentSlectedItem;
            if (_directoryController.IsDirectory(tempPath))
            {
                comboBox_path.Text = tempPath;
            }
        }

        private void comboBox_drives_SelectionChangeCommitted(object sender, EventArgs e)
        {
            comboBox_path.Text = comboBox_drives.SelectedItem.ToString();
            tempPath = comboBox_path.Text;
            TryAcceptChanges();
        }

        private void comboBox_path_SelectionChangeCommitted(object sender, EventArgs e)
        {

        }

        private void comboBox_path_DropDown(object sender, EventArgs e)
        {
            comboBox_path.Items.Clear();
            if (_directoryController.pathHistory.localHistory.Count>0)
            {
                List<string> tempHistoryPath = _directoryController.pathHistory.globalHistory;
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
            tempPath = _directoryController.pathHistory.GetPreviousPath();
            TryAcceptChanges();
        }

        private void btn_up_Click(object sender, EventArgs e)
        {

        }

        private void btn_next_Click(object sender, EventArgs e)
        {
            tempPath = _directoryController.pathHistory.GetNextPath();
            TryAcceptChanges();
        }
    }
}
