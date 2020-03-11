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
        PathController _pathController = new PathController();
        
        private void RefreshPathText()
        {
            comboBox_path.Text = tempPath;
        }
        private void TryAcceptChanges()
        {
            _pathController.SetPath(tempPath);
            LoadListView();

            void LoadListView()
            {
                listView_main.Clear();
                _pathController.LoadDirectory();
                foreach (var item in _pathController.currentLoadedDirectories)
                {
                    listView_main.Items.Add(item);
                }
                foreach (var item in _pathController.currentLoadedFiles)
                {
                    listView_main.Items.Add(item);
                }
            }
        }
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            _pathController.excActionPath += () => MessageBox.Show("Not accessible path!");

            comboBox_drives.Items.AddRange(WindowsDrivesInfo.drivesNames);
            comboBox_drives.SelectedIndex = 0;

            comboBox_path.Text = comboBox_drives.SelectedItem.ToString();

            tempPath           = comboBox_path.Text;

            TryAcceptChanges();
        }

        private void comboBox_drives_SelectionChangeCommitted(object sender, EventArgs e)
        {
            comboBox_path.Text = comboBox_drives.SelectedItem.ToString();
            tempPath           = comboBox_path.Text;

            _pathController.pathHistory.StopShifting();
            TryAcceptChanges();
        }

        private void comboBox_path_SelectionChangeCommitted(object sender, EventArgs e)
        {
            tempPath = ((ComboBox)sender).SelectedItem.ToString();

            RefreshPathText();
            TryAcceptChanges();
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
            tempPath = _pathController.pathHistory.GetPreviousPath();

            RefreshPathText();
            TryAcceptChanges();
        }

        private void btn_up_Click(object sender, EventArgs e)
        {
            _pathController.SetParentDirectoryPath();

            tempPath = _pathController.currentPath;

            RefreshPathText();
            TryAcceptChanges();
        }

        private void btn_next_Click(object sender, EventArgs e)
        {
            tempPath = _pathController.pathHistory.GetNextPath();
            RefreshPathText();
            TryAcceptChanges();
        }

        private void btn_go_Click(object sender, EventArgs e)
        {
            _pathController.pathHistory.StopShifting();
            tempPath = comboBox_path.Text;
            TryAcceptChanges();
        }

        private void listView_main_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            string currentSlectedItem = e.Item.Text;

            tempPath = _pathController.currentPath + "\\" + currentSlectedItem;

            if (_pathController.loader.IsDirectory(tempPath))
            { RefreshPathText(); }

        }

        private void listView_main_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            TryAcceptChanges();
        }
    }
}
