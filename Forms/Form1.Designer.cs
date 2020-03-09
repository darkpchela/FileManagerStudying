namespace FileManager
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.listView_main = new System.Windows.Forms.ListView();
            this.imageList_Icons = new System.Windows.Forms.ImageList(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.label_fileName = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label_fileType = new System.Windows.Forms.Label();
            this.btn_Back = new System.Windows.Forms.Button();
            this.btn_go = new System.Windows.Forms.Button();
            this.comboBox_drives = new System.Windows.Forms.ComboBox();
            this.comboBox_path = new System.Windows.Forms.ComboBox();
            this.pathHistoryBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.directoryControllerBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.btn_up = new System.Windows.Forms.Button();
            this.btn_next = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pathHistoryBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.directoryControllerBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // listView_main
            // 
            this.listView_main.HideSelection = false;
            this.listView_main.LargeImageList = this.imageList_Icons;
            this.listView_main.Location = new System.Drawing.Point(12, 39);
            this.listView_main.Name = "listView_main";
            this.listView_main.Size = new System.Drawing.Size(1117, 485);
            this.listView_main.SmallImageList = this.imageList_Icons;
            this.listView_main.TabIndex = 1;
            this.listView_main.UseCompatibleStateImageBehavior = false;
            this.listView_main.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.listView_main_ItemSelectionChanged);
            // 
            // imageList_Icons
            // 
            this.imageList_Icons.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            this.imageList_Icons.ImageSize = new System.Drawing.Size(48, 48);
            this.imageList_Icons.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(12, 550);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(112, 26);
            this.label1.TabIndex = 3;
            this.label1.Text = "File Name";
            // 
            // label_fileName
            // 
            this.label_fileName.AutoSize = true;
            this.label_fileName.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label_fileName.Location = new System.Drawing.Point(155, 551);
            this.label_fileName.Name = "label_fileName";
            this.label_fileName.Size = new System.Drawing.Size(40, 24);
            this.label_fileName.TabIndex = 4;
            this.label_fileName.Text = "-----";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.Location = new System.Drawing.Point(12, 587);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(100, 26);
            this.label3.TabIndex = 5;
            this.label3.Text = "File Type";
            // 
            // label_fileType
            // 
            this.label_fileType.AutoSize = true;
            this.label_fileType.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label_fileType.Location = new System.Drawing.Point(155, 589);
            this.label_fileType.Name = "label_fileType";
            this.label_fileType.Size = new System.Drawing.Size(40, 24);
            this.label_fileType.TabIndex = 6;
            this.label_fileType.Text = "-----";
            // 
            // btn_Back
            // 
            this.btn_Back.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btn_Back.Location = new System.Drawing.Point(12, 8);
            this.btn_Back.Name = "btn_Back";
            this.btn_Back.Size = new System.Drawing.Size(75, 26);
            this.btn_Back.TabIndex = 7;
            this.btn_Back.Text = "Back";
            this.btn_Back.UseVisualStyleBackColor = true;
            this.btn_Back.Click += new System.EventHandler(this.btn_Back_Click);
            // 
            // btn_go
            // 
            this.btn_go.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btn_go.Location = new System.Drawing.Point(1050, 7);
            this.btn_go.Name = "btn_go";
            this.btn_go.Size = new System.Drawing.Size(75, 23);
            this.btn_go.TabIndex = 8;
            this.btn_go.Text = "GO";
            this.btn_go.UseVisualStyleBackColor = true;
            this.btn_go.Click += new System.EventHandler(this.btn_go_Click);
            // 
            // comboBox_drives
            // 
            this.comboBox_drives.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_drives.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.comboBox_drives.FormattingEnabled = true;
            this.comboBox_drives.Location = new System.Drawing.Point(217, 10);
            this.comboBox_drives.Name = "comboBox_drives";
            this.comboBox_drives.Size = new System.Drawing.Size(47, 24);
            this.comboBox_drives.TabIndex = 9;
            this.comboBox_drives.SelectionChangeCommitted += new System.EventHandler(this.comboBox_drives_SelectionChangeCommitted);
            // 
            // comboBox_path
            // 
            this.comboBox_path.FormattingEnabled = true;
            this.comboBox_path.Location = new System.Drawing.Point(270, 12);
            this.comboBox_path.Name = "comboBox_path";
            this.comboBox_path.Size = new System.Drawing.Size(761, 21);
            this.comboBox_path.TabIndex = 10;
            this.comboBox_path.DropDown += new System.EventHandler(this.comboBox_path_DropDown);
            this.comboBox_path.SelectionChangeCommitted += new System.EventHandler(this.comboBox_path_SelectionChangeCommitted);
            this.comboBox_path.TextChanged += new System.EventHandler(this.comboBox_path_TextChanged);
            // 
            // pathHistoryBindingSource
            // 
            this.pathHistoryBindingSource.DataMember = "pathHistory";
            this.pathHistoryBindingSource.DataSource = this.directoryControllerBindingSource;
            // 
            // directoryControllerBindingSource
            // 
            this.directoryControllerBindingSource.DataSource = typeof(FileManager.Classes.DirectoryController);
            // 
            // btn_up
            // 
            this.btn_up.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btn_up.Location = new System.Drawing.Point(94, 8);
            this.btn_up.Name = "btn_up";
            this.btn_up.Size = new System.Drawing.Size(36, 26);
            this.btn_up.TabIndex = 7;
            this.btn_up.Text = "Up";
            this.btn_up.UseVisualStyleBackColor = true;
            this.btn_up.Click += new System.EventHandler(this.btn_up_Click);
            // 
            // btn_next
            // 
            this.btn_next.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btn_next.Location = new System.Drawing.Point(136, 8);
            this.btn_next.Name = "btn_next";
            this.btn_next.Size = new System.Drawing.Size(75, 26);
            this.btn_next.TabIndex = 11;
            this.btn_next.Text = "Next";
            this.btn_next.UseVisualStyleBackColor = true;
            this.btn_next.Click += new System.EventHandler(this.btn_next_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1139, 635);
            this.Controls.Add(this.btn_next);
            this.Controls.Add(this.comboBox_path);
            this.Controls.Add(this.comboBox_drives);
            this.Controls.Add(this.btn_go);
            this.Controls.Add(this.btn_up);
            this.Controls.Add(this.btn_Back);
            this.Controls.Add(this.label_fileType);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label_fileName);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.listView_main);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView listView_main;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label_fileName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label_fileType;
        private System.Windows.Forms.Button btn_Back;
        private System.Windows.Forms.Button btn_go;
        private System.Windows.Forms.ImageList imageList_Icons;
        private System.Windows.Forms.ComboBox comboBox_drives;
        private System.Windows.Forms.ComboBox comboBox_path;
        private System.Windows.Forms.BindingSource pathHistoryBindingSource;
        private System.Windows.Forms.BindingSource directoryControllerBindingSource;
        private System.Windows.Forms.Button btn_up;
        private System.Windows.Forms.Button btn_next;
    }
}

