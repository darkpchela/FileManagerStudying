namespace FileManager.Forms
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
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
            this.btn_up = new System.Windows.Forms.Button();
            this.btn_next = new System.Windows.Forms.Button();
            this.checkedListBox_buffer = new System.Windows.Forms.CheckedListBox();
            this.contextMenuStrip_buffer_manager = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem_add = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip_buffer_manager.SuspendLayout();
            this.SuspendLayout();
            // 
            // listView_main
            // 
            this.listView_main.ContextMenuStrip = this.contextMenuStrip_buffer_manager;
            this.listView_main.HideSelection = false;
            this.listView_main.LargeImageList = this.imageList_Icons;
            this.listView_main.Location = new System.Drawing.Point(12, 39);
            this.listView_main.Name = "listView_main";
            this.listView_main.Size = new System.Drawing.Size(1100, 485);
            this.listView_main.SmallImageList = this.imageList_Icons;
            this.listView_main.TabIndex = 1;
            this.listView_main.UseCompatibleStateImageBehavior = false;
            this.listView_main.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.listView_main_ItemSelectionChanged);
            this.listView_main.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listView_main_MouseDoubleClick);
            // 
            // imageList_Icons
            // 
            this.imageList_Icons.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList_Icons.ImageStream")));
            this.imageList_Icons.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList_Icons.Images.SetKeyName(0, "png1.ico");
            this.imageList_Icons.Images.SetKeyName(1, "png2.ico");
            this.imageList_Icons.Images.SetKeyName(2, "png3.ico");
            this.imageList_Icons.Images.SetKeyName(3, "png4.ico");
            this.imageList_Icons.Images.SetKeyName(4, "png5.ico");
            this.imageList_Icons.Images.SetKeyName(5, "png6.ico");
            this.imageList_Icons.Images.SetKeyName(6, "png7.ico");
            this.imageList_Icons.Images.SetKeyName(7, "png8.ico");
            this.imageList_Icons.Images.SetKeyName(8, "png9.ico");
            this.imageList_Icons.Images.SetKeyName(9, "png10.ico");
            this.imageList_Icons.Images.SetKeyName(10, "png11.ico");
            this.imageList_Icons.Images.SetKeyName(11, "png12.ico");
            this.imageList_Icons.Images.SetKeyName(12, "png13.ico");
            this.imageList_Icons.Images.SetKeyName(13, "png14.ico");
            this.imageList_Icons.Images.SetKeyName(14, "png15.ico");
            this.imageList_Icons.Images.SetKeyName(15, "png16.ico");
            this.imageList_Icons.Images.SetKeyName(16, "png17.ico");
            this.imageList_Icons.Images.SetKeyName(17, "png18.ico");
            this.imageList_Icons.Images.SetKeyName(18, "png19.ico");
            this.imageList_Icons.Images.SetKeyName(19, "png20.ico");
            this.imageList_Icons.Images.SetKeyName(20, "png21.ico");
            this.imageList_Icons.Images.SetKeyName(21, "png22.ico");
            this.imageList_Icons.Images.SetKeyName(22, "png23.ico");
            this.imageList_Icons.Images.SetKeyName(23, "png24.ico");
            this.imageList_Icons.Images.SetKeyName(24, "png25.ico");
            this.imageList_Icons.Images.SetKeyName(25, "png26.ico");
            this.imageList_Icons.Images.SetKeyName(26, "png27.ico");
            this.imageList_Icons.Images.SetKeyName(27, "png28.ico");
            this.imageList_Icons.Images.SetKeyName(28, "png29.ico");
            this.imageList_Icons.Images.SetKeyName(29, "png30.ico");
            this.imageList_Icons.Images.SetKeyName(30, "png31.ico");
            this.imageList_Icons.Images.SetKeyName(31, "png32.ico");
            this.imageList_Icons.Images.SetKeyName(32, "png33.ico");
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
            this.btn_go.Location = new System.Drawing.Point(1037, 10);
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
            this.comboBox_path.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.comboBox_path.FormattingEnabled = true;
            this.comboBox_path.Location = new System.Drawing.Point(270, 10);
            this.comboBox_path.Name = "comboBox_path";
            this.comboBox_path.Size = new System.Drawing.Size(761, 24);
            this.comboBox_path.TabIndex = 10;
            this.comboBox_path.DropDown += new System.EventHandler(this.comboBox_path_DropDown);
            this.comboBox_path.SelectionChangeCommitted += new System.EventHandler(this.comboBox_path_SelectionChangeCommitted);
            this.comboBox_path.TextChanged += new System.EventHandler(this.comboBox_path_TextChanged);
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
            // checkedListBox_buffer
            // 
            this.checkedListBox_buffer.FormattingEnabled = true;
            this.checkedListBox_buffer.Location = new System.Drawing.Point(653, 529);
            this.checkedListBox_buffer.Name = "checkedListBox_buffer";
            this.checkedListBox_buffer.Size = new System.Drawing.Size(459, 94);
            this.checkedListBox_buffer.TabIndex = 12;
            // 
            // contextMenuStrip_buffer_manager
            // 
            this.contextMenuStrip_buffer_manager.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem_add});
            this.contextMenuStrip_buffer_manager.Name = "contextMenuStrip_buffer_manager";
            this.contextMenuStrip_buffer_manager.Size = new System.Drawing.Size(181, 48);
            // 
            // toolStripMenuItem_add
            // 
            this.toolStripMenuItem_add.Name = "toolStripMenuItem_add";
            this.toolStripMenuItem_add.Size = new System.Drawing.Size(180, 22);
            this.toolStripMenuItem_add.Text = "Add file to buffer";
            this.toolStripMenuItem_add.Click += new System.EventHandler(this.toolStripMenuItem_add_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1124, 635);
            this.Controls.Add(this.checkedListBox_buffer);
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
            this.contextMenuStrip_buffer_manager.ResumeLayout(false);
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
        private System.Windows.Forms.Button btn_up;
        private System.Windows.Forms.Button btn_next;
        private System.Windows.Forms.CheckedListBox checkedListBox_buffer;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip_buffer_manager;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_add;
    }
}

