namespace Probotix.Mpu
{
    partial class Main
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.toolStrip_TopToolStrip = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripComboBox_com_port = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton_refreshports = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton_connect = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton_disconnect = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel_MindShine = new System.Windows.Forms.ToolStripLabel();
            this.toolStripButton_showVisualizer = new System.Windows.Forms.ToolStripButton();
            this.panel_devices = new System.Windows.Forms.Panel();
            this.treeView_devices = new System.Windows.Forms.TreeView();
            this.panel_specifications = new System.Windows.Forms.Panel();
            this.panel_search = new System.Windows.Forms.Panel();
            this.panel_device_data = new System.Windows.Forms.Panel();
            this.dataGridView_device_data = new System.Windows.Forms.DataGridView();
            this.addr = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.description = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.value = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.button_start_search = new System.Windows.Forms.Button();
            this.dataGridView_baudRates = new System.Windows.Forms.DataGridView();
            this.baudNum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.baudrate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SearchChecked = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.comboBox_search_options = new System.Windows.Forms.ComboBox();
            this.button_stop_search = new System.Windows.Forms.Button();
            this.statusStrip_statusBar = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel_state = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripProgressBar_progress = new System.Windows.Forms.ToolStripProgressBar();
            this.toolStripStatusLabel_progress_percent = new System.Windows.Forms.ToolStripStatusLabel();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip_TopMenu = new System.Windows.Forms.MenuStrip();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.toolStrip_TopToolStrip.SuspendLayout();
            this.panel_devices.SuspendLayout();
            this.panel_specifications.SuspendLayout();
            this.panel_search.SuspendLayout();
            this.panel_device_data.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_device_data)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_baudRates)).BeginInit();
            this.statusStrip_statusBar.SuspendLayout();
            this.menuStrip_TopMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip_TopToolStrip
            // 
            this.toolStrip_TopToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1,
            this.toolStripComboBox_com_port,
            this.toolStripSeparator1,
            this.toolStripButton_refreshports,
            this.toolStripButton_connect,
            this.toolStripButton_disconnect,
            this.toolStripSeparator2,
            this.toolStripLabel_MindShine,
            this.toolStripButton_showVisualizer});
            this.toolStrip_TopToolStrip.Location = new System.Drawing.Point(0, 24);
            this.toolStrip_TopToolStrip.Name = "toolStrip_TopToolStrip";
            this.toolStrip_TopToolStrip.Size = new System.Drawing.Size(531, 25);
            this.toolStrip_TopToolStrip.TabIndex = 2;
            this.toolStrip_TopToolStrip.Text = "toolStrip1";
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(35, 22);
            this.toolStripLabel1.Text = "Port: ";
            // 
            // toolStripComboBox_com_port
            // 
            this.toolStripComboBox_com_port.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.toolStripComboBox_com_port.Name = "toolStripComboBox_com_port";
            this.toolStripComboBox_com_port.Size = new System.Drawing.Size(80, 25);
            this.toolStripComboBox_com_port.Click += new System.EventHandler(this.toolStripComboBox_com_port_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButton_refreshports
            // 
            this.toolStripButton_refreshports.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton_refreshports.Image = global::Probotix.Mpu.Properties.Resources.arrow_refresh;
            this.toolStripButton_refreshports.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_refreshports.Name = "toolStripButton_refreshports";
            this.toolStripButton_refreshports.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton_refreshports.Text = "toolStripButton1";
            this.toolStripButton_refreshports.ToolTipText = "Refresh Ports";
            this.toolStripButton_refreshports.Click += new System.EventHandler(this.toolStripButton_refreshports_Click);
            // 
            // toolStripButton_connect
            // 
            this.toolStripButton_connect.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton_connect.Image = global::Probotix.Mpu.Properties.Resources.Connect;
            this.toolStripButton_connect.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_connect.Name = "toolStripButton_connect";
            this.toolStripButton_connect.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton_connect.Text = "toolStripButton1";
            this.toolStripButton_connect.ToolTipText = "Connect";
            this.toolStripButton_connect.Click += new System.EventHandler(this.toolStripButton_connect_Click);
            // 
            // toolStripButton_disconnect
            // 
            this.toolStripButton_disconnect.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton_disconnect.Image = global::Probotix.Mpu.Properties.Resources.Disconnect;
            this.toolStripButton_disconnect.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_disconnect.Name = "toolStripButton_disconnect";
            this.toolStripButton_disconnect.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton_disconnect.Text = "toolStripButton2";
            this.toolStripButton_disconnect.ToolTipText = "Disconnect";
            this.toolStripButton_disconnect.Click += new System.EventHandler(this.toolStripButton_disconnect_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripLabel_MindShine
            // 
            this.toolStripLabel_MindShine.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripLabel_MindShine.IsLink = true;
            this.toolStripLabel_MindShine.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this.toolStripLabel_MindShine.Margin = new System.Windows.Forms.Padding(0, 1, 3, 2);
            this.toolStripLabel_MindShine.Name = "toolStripLabel_MindShine";
            this.toolStripLabel_MindShine.Size = new System.Drawing.Size(138, 22);
            this.toolStripLabel_MindShine.Text = "http://www.mindshine.ir";
            // 
            // toolStripButton_showVisualizer
            // 
            this.toolStripButton_showVisualizer.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton_showVisualizer.Image = global::Probotix.Mpu.Properties.Resources._1391703905_cube;
            this.toolStripButton_showVisualizer.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_showVisualizer.Name = "toolStripButton_showVisualizer";
            this.toolStripButton_showVisualizer.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton_showVisualizer.Text = "toolStripButton1";
            this.toolStripButton_showVisualizer.Click += new System.EventHandler(this.toolStripButton_showVisualizer_Click);
            // 
            // panel_devices
            // 
            this.panel_devices.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel_devices.Controls.Add(this.treeView_devices);
            this.panel_devices.Location = new System.Drawing.Point(5, 52);
            this.panel_devices.Name = "panel_devices";
            this.panel_devices.Size = new System.Drawing.Size(175, 377);
            this.panel_devices.TabIndex = 3;
            // 
            // treeView_devices
            // 
            this.treeView_devices.Location = new System.Drawing.Point(3, 3);
            this.treeView_devices.Name = "treeView_devices";
            this.treeView_devices.Size = new System.Drawing.Size(164, 369);
            this.treeView_devices.TabIndex = 0;
            this.treeView_devices.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeView_devices_NodeMouseClick);
            // 
            // panel_specifications
            // 
            this.panel_specifications.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel_specifications.Controls.Add(this.panel_search);
            this.panel_specifications.Location = new System.Drawing.Point(187, 52);
            this.panel_specifications.Name = "panel_specifications";
            this.panel_specifications.Size = new System.Drawing.Size(336, 377);
            this.panel_specifications.TabIndex = 4;
            // 
            // panel_search
            // 
            this.panel_search.AutoSize = true;
            this.panel_search.Controls.Add(this.panel_device_data);
            this.panel_search.Controls.Add(this.button_start_search);
            this.panel_search.Controls.Add(this.dataGridView_baudRates);
            this.panel_search.Controls.Add(this.comboBox_search_options);
            this.panel_search.Controls.Add(this.button_stop_search);
            this.panel_search.Location = new System.Drawing.Point(6, 3);
            this.panel_search.Name = "panel_search";
            this.panel_search.Size = new System.Drawing.Size(321, 362);
            this.panel_search.TabIndex = 0;
            // 
            // panel_device_data
            // 
            this.panel_device_data.AutoSize = true;
            this.panel_device_data.Controls.Add(this.dataGridView_device_data);
            this.panel_device_data.Location = new System.Drawing.Point(0, 0);
            this.panel_device_data.Name = "panel_device_data";
            this.panel_device_data.Size = new System.Drawing.Size(318, 352);
            this.panel_device_data.TabIndex = 5;
            // 
            // dataGridView_device_data
            // 
            this.dataGridView_device_data.AllowUserToAddRows = false;
            this.dataGridView_device_data.AllowUserToDeleteRows = false;
            this.dataGridView_device_data.AllowUserToResizeColumns = false;
            this.dataGridView_device_data.AllowUserToResizeRows = false;
            this.dataGridView_device_data.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView_device_data.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_device_data.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.addr,
            this.description,
            this.value});
            this.dataGridView_device_data.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dataGridView_device_data.Location = new System.Drawing.Point(7, 13);
            this.dataGridView_device_data.Name = "dataGridView_device_data";
            this.dataGridView_device_data.RowHeadersVisible = false;
            this.dataGridView_device_data.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dataGridView_device_data.Size = new System.Drawing.Size(304, 323);
            this.dataGridView_device_data.TabIndex = 1;
            this.dataGridView_device_data.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_device_data_CellEndEdit);
            // 
            // addr
            // 
            this.addr.FillWeight = 64.97462F;
            this.addr.HeaderText = "Addr";
            this.addr.Name = "addr";
            this.addr.ReadOnly = true;
            this.addr.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.addr.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // description
            // 
            this.description.FillWeight = 170.7241F;
            this.description.HeaderText = "Description";
            this.description.MinimumWidth = 160;
            this.description.Name = "description";
            this.description.ReadOnly = true;
            this.description.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // value
            // 
            this.value.FillWeight = 84.30127F;
            this.value.HeaderText = "Value";
            this.value.MinimumWidth = 80;
            this.value.Name = "value";
            this.value.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.value.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // button_start_search
            // 
            this.button_start_search.Location = new System.Drawing.Point(224, 42);
            this.button_start_search.Name = "button_start_search";
            this.button_start_search.Size = new System.Drawing.Size(60, 23);
            this.button_start_search.TabIndex = 2;
            this.button_start_search.Text = "Start";
            this.button_start_search.UseVisualStyleBackColor = true;
            this.button_start_search.Click += new System.EventHandler(this.button_start_search_Click);
            // 
            // dataGridView_baudRates
            // 
            this.dataGridView_baudRates.AllowUserToAddRows = false;
            this.dataGridView_baudRates.AllowUserToDeleteRows = false;
            this.dataGridView_baudRates.AllowUserToResizeColumns = false;
            this.dataGridView_baudRates.AllowUserToResizeRows = false;
            this.dataGridView_baudRates.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView_baudRates.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_baudRates.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.baudNum,
            this.baudrate,
            this.SearchChecked});
            this.dataGridView_baudRates.Location = new System.Drawing.Point(13, 15);
            this.dataGridView_baudRates.Name = "dataGridView_baudRates";
            this.dataGridView_baudRates.RowHeadersVisible = false;
            this.dataGridView_baudRates.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dataGridView_baudRates.Size = new System.Drawing.Size(185, 319);
            this.dataGridView_baudRates.TabIndex = 0;
            // 
            // baudNum
            // 
            this.baudNum.FillWeight = 64.97462F;
            this.baudNum.HeaderText = "";
            this.baudNum.MinimumWidth = 30;
            this.baudNum.Name = "baudNum";
            this.baudNum.ReadOnly = true;
            this.baudNum.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.baudNum.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // baudrate
            // 
            this.baudrate.FillWeight = 139.1048F;
            this.baudrate.HeaderText = "bps";
            this.baudrate.MinimumWidth = 95;
            this.baudrate.Name = "baudrate";
            this.baudrate.ReadOnly = true;
            this.baudrate.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // SearchChecked
            // 
            this.SearchChecked.FalseValue = "false";
            this.SearchChecked.FillWeight = 115.9206F;
            this.SearchChecked.HeaderText = "Search";
            this.SearchChecked.Name = "SearchChecked";
            this.SearchChecked.TrueValue = "true";
            // 
            // comboBox_search_options
            // 
            this.comboBox_search_options.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_search_options.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.comboBox_search_options.FormattingEnabled = true;
            this.comboBox_search_options.Items.AddRange(new object[] {
            "Basic Search",
            "Advance Search",
            "Custom Search",
            "Full Search"});
            this.comboBox_search_options.Location = new System.Drawing.Point(204, 15);
            this.comboBox_search_options.Name = "comboBox_search_options";
            this.comboBox_search_options.Size = new System.Drawing.Size(107, 21);
            this.comboBox_search_options.TabIndex = 0;
            this.comboBox_search_options.SelectedIndexChanged += new System.EventHandler(this.comboBox_search_options_SelectedIndexChanged);
            // 
            // button_stop_search
            // 
            this.button_stop_search.Location = new System.Drawing.Point(224, 71);
            this.button_stop_search.Name = "button_stop_search";
            this.button_stop_search.Size = new System.Drawing.Size(60, 23);
            this.button_stop_search.TabIndex = 3;
            this.button_stop_search.Text = "Stop";
            this.button_stop_search.UseVisualStyleBackColor = true;
            this.button_stop_search.Click += new System.EventHandler(this.button_stop_search_Click);
            // 
            // statusStrip_statusBar
            // 
            this.statusStrip_statusBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel_state,
            this.toolStripProgressBar_progress,
            this.toolStripStatusLabel_progress_percent});
            this.statusStrip_statusBar.Location = new System.Drawing.Point(0, 435);
            this.statusStrip_statusBar.Name = "statusStrip_statusBar";
            this.statusStrip_statusBar.Size = new System.Drawing.Size(531, 32);
            this.statusStrip_statusBar.TabIndex = 6;
            this.statusStrip_statusBar.Text = "statusStrip1";
            // 
            // toolStripStatusLabel_state
            // 
            this.toolStripStatusLabel_state.AutoSize = false;
            this.toolStripStatusLabel_state.Name = "toolStripStatusLabel_state";
            this.toolStripStatusLabel_state.Size = new System.Drawing.Size(225, 27);
            this.toolStripStatusLabel_state.Text = "state:";
            this.toolStripStatusLabel_state.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // toolStripProgressBar_progress
            // 
            this.toolStripProgressBar_progress.Margin = new System.Windows.Forms.Padding(0, 3, 1, 3);
            this.toolStripProgressBar_progress.Name = "toolStripProgressBar_progress";
            this.toolStripProgressBar_progress.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.toolStripProgressBar_progress.Size = new System.Drawing.Size(250, 26);
            // 
            // toolStripStatusLabel_progress_percent
            // 
            this.toolStripStatusLabel_progress_percent.Name = "toolStripStatusLabel_progress_percent";
            this.toolStripStatusLabel_progress_percent.Size = new System.Drawing.Size(35, 27);
            this.toolStripStatusLabel_progress_percent.Text = "100%";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // menuStrip_TopMenu
            // 
            this.menuStrip_TopMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip_TopMenu.Location = new System.Drawing.Point(0, 0);
            this.menuStrip_TopMenu.Name = "menuStrip_TopMenu";
            this.menuStrip_TopMenu.Size = new System.Drawing.Size(531, 24);
            this.menuStrip_TopMenu.TabIndex = 1;
            this.menuStrip_TopMenu.Text = "menuStrip1";
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 4;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(531, 467);
            this.Controls.Add(this.statusStrip_statusBar);
            this.Controls.Add(this.panel_specifications);
            this.Controls.Add(this.panel_devices);
            this.Controls.Add(this.toolStrip_TopToolStrip);
            this.Controls.Add(this.menuStrip_TopMenu);
            this.MainMenuStrip = this.menuStrip_TopMenu;
            this.Name = "Main";
            this.Text = "PRO-IMU Configurator";
            this.toolStrip_TopToolStrip.ResumeLayout(false);
            this.toolStrip_TopToolStrip.PerformLayout();
            this.panel_devices.ResumeLayout(false);
            this.panel_specifications.ResumeLayout(false);
            this.panel_specifications.PerformLayout();
            this.panel_search.ResumeLayout(false);
            this.panel_search.PerformLayout();
            this.panel_device_data.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_device_data)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_baudRates)).EndInit();
            this.statusStrip_statusBar.ResumeLayout(false);
            this.statusStrip_statusBar.PerformLayout();
            this.menuStrip_TopMenu.ResumeLayout(false);
            this.menuStrip_TopMenu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip_TopToolStrip;
        private System.Windows.Forms.ToolStripComboBox toolStripComboBox_com_port;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton toolStripButton_connect;
        private System.Windows.Forms.ToolStripButton toolStripButton_disconnect;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripLabel toolStripLabel_MindShine;
        private System.Windows.Forms.Panel panel_devices;
        private System.Windows.Forms.Panel panel_specifications;
        private System.Windows.Forms.Panel panel_search;
        private System.Windows.Forms.Button button_stop_search;
        private System.Windows.Forms.Button button_start_search;
        private System.Windows.Forms.DataGridView dataGridView_baudRates;
        private System.Windows.Forms.ComboBox comboBox_search_options;
        private System.Windows.Forms.TreeView treeView_devices;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripButton toolStripButton_refreshports;
        private System.Windows.Forms.Panel panel_device_data;
        private System.Windows.Forms.DataGridView dataGridView_device_data;
        private System.Windows.Forms.StatusStrip statusStrip_statusBar;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel_state;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar_progress;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel_progress_percent;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.MenuStrip menuStrip_TopMenu;
        private System.Windows.Forms.DataGridViewTextBoxColumn baudNum;
        private System.Windows.Forms.DataGridViewTextBoxColumn baudrate;
        private System.Windows.Forms.DataGridViewCheckBoxColumn SearchChecked;
        private System.Windows.Forms.DataGridViewTextBoxColumn addr;
        private System.Windows.Forms.DataGridViewTextBoxColumn description;
        private System.Windows.Forms.DataGridViewTextBoxColumn value;
        private System.Windows.Forms.ToolStripButton toolStripButton_showVisualizer;
        private System.Windows.Forms.Timer timer1;
    }
}

