namespace Can_Test
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label11 = new System.Windows.Forms.Label();
            this.comboBoxCanBaudRate = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.comboBoxCanDevice = new System.Windows.Forms.ComboBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.ShortKey = new System.Windows.Forms.Button();
            this.shortkeycomboBox = new System.Windows.Forms.ComboBox();
            this.dTCToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewFormatToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.byteToolStripMenuItem = new System.Windows.Forms.ToolStripComboBox();
            this.dataStreamToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripSplitButton();
            this.udsform_toolStrip = new System.Windows.Forms.ToolStrip();
            this.FileButtontoolstrip = new System.Windows.Forms.ToolStripDropDownButton();
            this.openFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.historyFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.BusButtontoolstrip = new System.Windows.Forms.ToolStripButton();
            this.PanelButtontoolstrip = new System.Windows.Forms.ToolStripButton();
            this.richTextBoxDisplay = new System.Windows.Forms.RichTextBox();
            this.contextMenuStriprichTextBoxDisplay = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.StreamSaveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.CopyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ClearToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel3 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel5 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel4 = new System.Windows.Forms.ToolStripStatusLabel();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.udsform_toolStrip.SuspendLayout();
            this.contextMenuStriprichTextBoxDisplay.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(-1, 1);
            this.tabControl1.MinimumSize = new System.Drawing.Size(54, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(409, 590);
            this.tabControl1.TabIndex = 2;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.White;
            this.tabPage1.Controls.Add(this.groupBox4);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(401, 564);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Can set";
            // 
            // groupBox4
            // 
            this.groupBox4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox4.Controls.Add(this.label11);
            this.groupBox4.Controls.Add(this.comboBoxCanBaudRate);
            this.groupBox4.Controls.Add(this.label10);
            this.groupBox4.Controls.Add(this.comboBoxCanDevice);
            this.groupBox4.Location = new System.Drawing.Point(27, 25);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(345, 97);
            this.groupBox4.TabIndex = 37;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "CAN BUS";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(13, 62);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(53, 12);
            this.label11.TabIndex = 38;
            this.label11.Text = "Baudrate";
            // 
            // comboBoxCanBaudRate
            // 
            this.comboBoxCanBaudRate.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxCanBaudRate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxCanBaudRate.FormattingEnabled = true;
            this.comboBoxCanBaudRate.Items.AddRange(new object[] {
            "50",
            "100",
            "125",
            "250",
            "500",
            "1000"});
            this.comboBoxCanBaudRate.Location = new System.Drawing.Point(71, 61);
            this.comboBoxCanBaudRate.Name = "comboBoxCanBaudRate";
            this.comboBoxCanBaudRate.Size = new System.Drawing.Size(254, 20);
            this.comboBoxCanBaudRate.TabIndex = 36;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(14, 29);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(41, 12);
            this.label10.TabIndex = 37;
            this.label10.Text = "Device";
            // 
            // comboBoxCanDevice
            // 
            this.comboBoxCanDevice.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxCanDevice.Cursor = System.Windows.Forms.Cursors.Hand;
            this.comboBoxCanDevice.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxCanDevice.FormattingEnabled = true;
            this.comboBoxCanDevice.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.comboBoxCanDevice.Location = new System.Drawing.Point(71, 26);
            this.comboBoxCanDevice.Name = "comboBoxCanDevice";
            this.comboBoxCanDevice.Size = new System.Drawing.Size(254, 20);
            this.comboBoxCanDevice.TabIndex = 24;
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.Color.White;
            this.tabPage2.Controls.Add(this.groupBox2);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(401, 564);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "UDS_14229";
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.ShortKey);
            this.groupBox2.Controls.Add(this.shortkeycomboBox);
            this.groupBox2.Enabled = false;
            this.groupBox2.Location = new System.Drawing.Point(15, 19);
            this.groupBox2.MinimumSize = new System.Drawing.Size(336, 59);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(378, 65);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "ShortKey";
            // 
            // ShortKey
            // 
            this.ShortKey.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ShortKey.Enabled = false;
            this.ShortKey.Location = new System.Drawing.Point(277, 22);
            this.ShortKey.Name = "ShortKey";
            this.ShortKey.Size = new System.Drawing.Size(83, 23);
            this.ShortKey.TabIndex = 4;
            this.ShortKey.Text = "Send";
            this.ShortKey.UseVisualStyleBackColor = true;
            this.ShortKey.Click += new System.EventHandler(this.ShortKey_Click);
            // 
            // shortkeycomboBox
            // 
            this.shortkeycomboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.shortkeycomboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.shortkeycomboBox.FormattingEnabled = true;
            this.shortkeycomboBox.Location = new System.Drawing.Point(20, 22);
            this.shortkeycomboBox.Name = "shortkeycomboBox";
            this.shortkeycomboBox.Size = new System.Drawing.Size(246, 20);
            this.shortkeycomboBox.TabIndex = 2;
            // 
            // dTCToolStripMenuItem
            // 
            this.dTCToolStripMenuItem.Checked = true;
            this.dTCToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.dTCToolStripMenuItem.Name = "dTCToolStripMenuItem";
            this.dTCToolStripMenuItem.Size = new System.Drawing.Size(178, 22);
            this.dTCToolStripMenuItem.Text = "DTC Analysis";
            // 
            // viewFormatToolStripMenuItem
            // 
            this.viewFormatToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.byteToolStripMenuItem});
            this.viewFormatToolStripMenuItem.Name = "viewFormatToolStripMenuItem";
            this.viewFormatToolStripMenuItem.Size = new System.Drawing.Size(178, 22);
            this.viewFormatToolStripMenuItem.Text = "View Format";
            // 
            // byteToolStripMenuItem
            // 
            this.byteToolStripMenuItem.Items.AddRange(new object[] {
            "8 Byte",
            "16 Byte",
            "24 Byte"});
            this.byteToolStripMenuItem.Name = "byteToolStripMenuItem";
            this.byteToolStripMenuItem.Size = new System.Drawing.Size(152, 25);
            this.byteToolStripMenuItem.Text = "8 Byte";
            // 
            // dataStreamToolStripMenuItem
            // 
            this.dataStreamToolStripMenuItem.Name = "dataStreamToolStripMenuItem";
            this.dataStreamToolStripMenuItem.Size = new System.Drawing.Size(178, 22);
            this.dataStreamToolStripMenuItem.Text = "Data Stream Only";
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButton2.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.dTCToolStripMenuItem,
            this.viewFormatToolStripMenuItem,
            this.dataStreamToolStripMenuItem});
            this.toolStripButton2.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton2.Image")));
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(64, 22);
            this.toolStripButton2.Text = "Setting";
            // 
            // udsform_toolStrip
            // 
            this.udsform_toolStrip.BackColor = System.Drawing.Color.White;
            this.udsform_toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FileButtontoolstrip,
            this.BusButtontoolstrip,
            this.PanelButtontoolstrip});
            this.udsform_toolStrip.Location = new System.Drawing.Point(0, 0);
            this.udsform_toolStrip.Name = "udsform_toolStrip";
            this.udsform_toolStrip.Size = new System.Drawing.Size(854, 25);
            this.udsform_toolStrip.TabIndex = 43;
            this.udsform_toolStrip.Text = "udsform_toolStrip";
            // 
            // FileButtontoolstrip
            // 
            this.FileButtontoolstrip.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.FileButtontoolstrip.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openFileToolStripMenuItem,
            this.saveFileToolStripMenuItem,
            this.historyFileToolStripMenuItem});
            this.FileButtontoolstrip.Image = ((System.Drawing.Image)(resources.GetObject("FileButtontoolstrip.Image")));
            this.FileButtontoolstrip.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.FileButtontoolstrip.Name = "FileButtontoolstrip";
            this.FileButtontoolstrip.Size = new System.Drawing.Size(44, 22);
            this.FileButtontoolstrip.Text = "File ";
            // 
            // openFileToolStripMenuItem
            // 
            this.openFileToolStripMenuItem.Name = "openFileToolStripMenuItem";
            this.openFileToolStripMenuItem.Size = new System.Drawing.Size(140, 22);
            this.openFileToolStripMenuItem.Text = "Open File";
            this.openFileToolStripMenuItem.Click += new System.EventHandler(this.openFileToolStripMenuItem_Click);
            // 
            // saveFileToolStripMenuItem
            // 
            this.saveFileToolStripMenuItem.Name = "saveFileToolStripMenuItem";
            this.saveFileToolStripMenuItem.Size = new System.Drawing.Size(140, 22);
            this.saveFileToolStripMenuItem.Text = "Save File";
            // 
            // historyFileToolStripMenuItem
            // 
            this.historyFileToolStripMenuItem.Name = "historyFileToolStripMenuItem";
            this.historyFileToolStripMenuItem.Size = new System.Drawing.Size(140, 22);
            this.historyFileToolStripMenuItem.Text = "History File";
            // 
            // BusButtontoolstrip
            // 
            this.BusButtontoolstrip.Checked = true;
            this.BusButtontoolstrip.CheckState = System.Windows.Forms.CheckState.Checked;
            this.BusButtontoolstrip.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.BusButtontoolstrip.Image = ((System.Drawing.Image)(resources.GetObject("BusButtontoolstrip.Image")));
            this.BusButtontoolstrip.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.BusButtontoolstrip.Name = "BusButtontoolstrip";
            this.BusButtontoolstrip.Size = new System.Drawing.Size(58, 22);
            this.BusButtontoolstrip.Text = "Off-Line";
            this.BusButtontoolstrip.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // PanelButtontoolstrip
            // 
            this.PanelButtontoolstrip.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.PanelButtontoolstrip.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.PanelButtontoolstrip.Name = "PanelButtontoolstrip";
            this.PanelButtontoolstrip.Size = new System.Drawing.Size(43, 22);
            this.PanelButtontoolstrip.Text = "Panel";
            this.PanelButtontoolstrip.Click += new System.EventHandler(this.Panel_Click);
            // 
            // richTextBoxDisplay
            // 
            this.richTextBoxDisplay.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.richTextBoxDisplay.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBoxDisplay.ContextMenuStrip = this.contextMenuStriprichTextBoxDisplay;
            this.richTextBoxDisplay.HideSelection = false;
            this.richTextBoxDisplay.Location = new System.Drawing.Point(3, 3);
            this.richTextBoxDisplay.Name = "richTextBoxDisplay";
            this.richTextBoxDisplay.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.richTextBoxDisplay.Size = new System.Drawing.Size(396, 584);
            this.richTextBoxDisplay.TabIndex = 40;
            this.richTextBoxDisplay.Text = "";
            // 
            // contextMenuStriprichTextBoxDisplay
            // 
            this.contextMenuStriprichTextBoxDisplay.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.StreamSaveToolStripMenuItem,
            this.CopyToolStripMenuItem,
            this.ClearToolStripMenuItem});
            this.contextMenuStriprichTextBoxDisplay.Name = "contextMenuStripSream";
            this.contextMenuStriprichTextBoxDisplay.Size = new System.Drawing.Size(107, 70);
            // 
            // StreamSaveToolStripMenuItem
            // 
            this.StreamSaveToolStripMenuItem.Name = "StreamSaveToolStripMenuItem";
            this.StreamSaveToolStripMenuItem.Size = new System.Drawing.Size(106, 22);
            this.StreamSaveToolStripMenuItem.Text = "Save";
            // 
            // CopyToolStripMenuItem
            // 
            this.CopyToolStripMenuItem.Name = "CopyToolStripMenuItem";
            this.CopyToolStripMenuItem.Size = new System.Drawing.Size(106, 22);
            this.CopyToolStripMenuItem.Text = "Copy";
            // 
            // ClearToolStripMenuItem
            // 
            this.ClearToolStripMenuItem.Name = "ClearToolStripMenuItem";
            this.ClearToolStripMenuItem.Size = new System.Drawing.Size(106, 22);
            this.ClearToolStripMenuItem.Text = "Clear";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(12, 28);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.AutoScroll = true;
            this.splitContainer1.Panel1.AutoScrollMinSize = new System.Drawing.Size(54, 0);
            this.splitContainer1.Panel1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.splitContainer1.Panel1.Controls.Add(this.tabControl1);
            this.splitContainer1.Panel1MinSize = 407;
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.richTextBoxDisplay);
            this.splitContainer1.Size = new System.Drawing.Size(831, 592);
            this.splitContainer1.SplitterDistance = 411;
            this.splitContainer1.TabIndex = 44;
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "openFileDialog";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.toolStripStatusLabel2,
            this.toolStripStatusLabel3,
            this.toolStripStatusLabel5,
            this.toolStripStatusLabel4});
            this.statusStrip1.Location = new System.Drawing.Point(0, 623);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(854, 22);
            this.statusStrip1.TabIndex = 45;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(0, 17);
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(0, 17);
            // 
            // toolStripStatusLabel3
            // 
            this.toolStripStatusLabel3.Name = "toolStripStatusLabel3";
            this.toolStripStatusLabel3.Size = new System.Drawing.Size(0, 17);
            // 
            // toolStripStatusLabel5
            // 
            this.toolStripStatusLabel5.Name = "toolStripStatusLabel5";
            this.toolStripStatusLabel5.Size = new System.Drawing.Size(0, 17);
            // 
            // toolStripStatusLabel4
            // 
            this.toolStripStatusLabel4.Name = "toolStripStatusLabel4";
            this.toolStripStatusLabel4.Size = new System.Drawing.Size(0, 17);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.ClientSize = new System.Drawing.Size(854, 645);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.udsform_toolStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimumSize = new System.Drawing.Size(870, 616);
            this.Name = "Form1";
            this.Text = "Can Tool";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.udsform_toolStrip.ResumeLayout(false);
            this.udsform_toolStrip.PerformLayout();
            this.contextMenuStriprichTextBoxDisplay.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.ToolStripMenuItem dTCToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewFormatToolStripMenuItem;
        private System.Windows.Forms.ToolStripComboBox byteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dataStreamToolStripMenuItem;
        private System.Windows.Forms.ToolStripSplitButton toolStripButton2;
        private System.Windows.Forms.ToolStrip udsform_toolStrip;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button ShortKey;
        private System.Windows.Forms.ComboBox shortkeycomboBox;
        private System.Windows.Forms.RichTextBox richTextBoxDisplay;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStriprichTextBoxDisplay;
        private System.Windows.Forms.ToolStripMenuItem StreamSaveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem CopyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ClearToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel3;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel4;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel5;
        private System.Windows.Forms.ToolStripButton BusButtontoolstrip;
        private System.Windows.Forms.ToolStripButton PanelButtontoolstrip;
        private System.Windows.Forms.ToolStripDropDownButton FileButtontoolstrip;
        private System.Windows.Forms.ToolStripMenuItem openFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem historyFileToolStripMenuItem;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ComboBox comboBoxCanBaudRate;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox comboBoxCanDevice;
    }
}

