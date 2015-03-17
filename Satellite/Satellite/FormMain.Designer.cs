namespace Satellite
{
    partial class FormMain
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
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.tsMain = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.tscbPort = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.tscbBaudrate = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbtOpen = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
            this.tsmiDisplayHistory = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiDisplayHex = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiSendAsHexData = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiAnalyseData = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiShowGPSData = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbtICTEST = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbtSystemTest = new System.Windows.Forms.ToolStripButton();
            this.spPort = new System.IO.Ports.SerialPort(this.components);
            this.ssMain = new System.Windows.Forms.StatusStrip();
            this.tsslTimer = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsslNumber = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsSend = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel3 = new System.Windows.Forms.ToolStripLabel();
            this.tstbOrigin = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel4 = new System.Windows.Forms.ToolStripLabel();
            this.tstbTarget = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripLabel5 = new System.Windows.Forms.ToolStripLabel();
            this.tstbData = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbtSend = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbtTest = new System.Windows.Forms.ToolStripButton();
            this.scContainer = new System.Windows.Forms.SplitContainer();
            this.rtbHistory = new System.Windows.Forms.RichTextBox();
            this.rtbGPS = new System.Windows.Forms.RichTextBox();
            this.tsslCapacity = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsMain.SuspendLayout();
            this.ssMain.SuspendLayout();
            this.tsSend.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.scContainer)).BeginInit();
            this.scContainer.Panel1.SuspendLayout();
            this.scContainer.Panel2.SuspendLayout();
            this.scContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // tsMain
            // 
            this.tsMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel2,
            this.tscbPort,
            this.toolStripSeparator1,
            this.toolStripLabel1,
            this.tscbBaudrate,
            this.toolStripSeparator2,
            this.tsbtOpen,
            this.toolStripSeparator3,
            this.toolStripDropDownButton1,
            this.toolStripSeparator4,
            this.tsbtICTEST,
            this.toolStripSeparator8,
            this.tsbtSystemTest});
            this.tsMain.Location = new System.Drawing.Point(0, 0);
            this.tsMain.Name = "tsMain";
            this.tsMain.Size = new System.Drawing.Size(884, 25);
            this.tsMain.TabIndex = 0;
            this.tsMain.Text = "toolStrip1";
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(39, 22);
            this.toolStripLabel2.Text = "Port: ";
            // 
            // tscbPort
            // 
            this.tscbPort.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.tscbPort.Name = "tscbPort";
            this.tscbPort.Size = new System.Drawing.Size(75, 25);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(71, 22);
            this.toolStripLabel1.Text = "BaudRate: ";
            // 
            // tscbBaudrate
            // 
            this.tscbBaudrate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.tscbBaudrate.DropDownWidth = 75;
            this.tscbBaudrate.Items.AddRange(new object[] {
            "110",
            "320",
            "1200",
            "2400",
            "4800",
            "9600",
            "19200",
            "38400",
            "57600",
            "115200",
            "230400",
            "460800",
            "921600"});
            this.tscbBaudrate.Name = "tscbBaudrate";
            this.tscbBaudrate.Size = new System.Drawing.Size(75, 25);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbtOpen
            // 
            this.tsbtOpen.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbtOpen.Image = ((System.Drawing.Image)(resources.GetObject("tsbtOpen.Image")));
            this.tsbtOpen.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtOpen.Name = "tsbtOpen";
            this.tsbtOpen.Size = new System.Drawing.Size(44, 22);
            this.tsbtOpen.Text = "Open";
            this.tsbtOpen.Click += new System.EventHandler(this.tsbtOpen_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripDropDownButton1
            // 
            this.toolStripDropDownButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripDropDownButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiDisplayHistory,
            this.tsmiDisplayHex,
            this.tsmiSendAsHexData,
            this.toolStripMenuItem1,
            this.tsmiAnalyseData,
            this.tsmiShowGPSData});
            this.toolStripDropDownButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownButton1.Image")));
            this.toolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton1.Name = "toolStripDropDownButton1";
            this.toolStripDropDownButton1.Size = new System.Drawing.Size(68, 22);
            this.toolStripDropDownButton1.Text = "Setting: ";
            // 
            // tsmiDisplayHistory
            // 
            this.tsmiDisplayHistory.Checked = true;
            this.tsmiDisplayHistory.CheckState = System.Windows.Forms.CheckState.Checked;
            this.tsmiDisplayHistory.Name = "tsmiDisplayHistory";
            this.tsmiDisplayHistory.Size = new System.Drawing.Size(178, 22);
            this.tsmiDisplayHistory.Text = "Display History";
            this.tsmiDisplayHistory.Click += new System.EventHandler(this.tsmiDisplayHistory_Click);
            // 
            // tsmiDisplayHex
            // 
            this.tsmiDisplayHex.Checked = true;
            this.tsmiDisplayHex.CheckState = System.Windows.Forms.CheckState.Checked;
            this.tsmiDisplayHex.Name = "tsmiDisplayHex";
            this.tsmiDisplayHex.Size = new System.Drawing.Size(178, 22);
            this.tsmiDisplayHex.Text = "Display Hex data";
            this.tsmiDisplayHex.Click += new System.EventHandler(this.tsmiDisplayHex_Click);
            // 
            // tsmiSendAsHexData
            // 
            this.tsmiSendAsHexData.Name = "tsmiSendAsHexData";
            this.tsmiSendAsHexData.Size = new System.Drawing.Size(178, 22);
            this.tsmiSendAsHexData.Text = "Send as Hex data";
            this.tsmiSendAsHexData.Click += new System.EventHandler(this.tsmiSendAsHexData_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(175, 6);
            // 
            // tsmiAnalyseData
            // 
            this.tsmiAnalyseData.Checked = true;
            this.tsmiAnalyseData.CheckState = System.Windows.Forms.CheckState.Checked;
            this.tsmiAnalyseData.Name = "tsmiAnalyseData";
            this.tsmiAnalyseData.Size = new System.Drawing.Size(178, 22);
            this.tsmiAnalyseData.Text = "Analyse data";
            this.tsmiAnalyseData.Click += new System.EventHandler(this.tsmiAnalyseData_Click);
            // 
            // tsmiShowGPSData
            // 
            this.tsmiShowGPSData.Name = "tsmiShowGPSData";
            this.tsmiShowGPSData.Size = new System.Drawing.Size(178, 22);
            this.tsmiShowGPSData.Text = "Show GPS data";
            this.tsmiShowGPSData.Click += new System.EventHandler(this.tsmiShowGPSData_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbtICTEST
            // 
            this.tsbtICTEST.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbtICTEST.Image = ((System.Drawing.Image)(resources.GetObject("tsbtICTEST.Image")));
            this.tsbtICTEST.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtICTEST.Name = "tsbtICTEST";
            this.tsbtICTEST.Size = new System.Drawing.Size(52, 22);
            this.tsbtICTEST.Text = "IC Test";
            this.tsbtICTEST.Click += new System.EventHandler(this.tsbtICTEST_Click);
            // 
            // toolStripSeparator8
            // 
            this.toolStripSeparator8.Name = "toolStripSeparator8";
            this.toolStripSeparator8.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbtSystemTest
            // 
            this.tsbtSystemTest.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbtSystemTest.Image = ((System.Drawing.Image)(resources.GetObject("tsbtSystemTest.Image")));
            this.tsbtSystemTest.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtSystemTest.Name = "tsbtSystemTest";
            this.tsbtSystemTest.Size = new System.Drawing.Size(81, 22);
            this.tsbtSystemTest.Text = "System Test";
            this.tsbtSystemTest.Click += new System.EventHandler(this.tsbtSystemTest_Click);
            // 
            // spPort
            // 
            this.spPort.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.spPort_DataReceived);
            // 
            // ssMain
            // 
            this.ssMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsslTimer,
            this.tsslNumber,
            this.tsslCapacity});
            this.ssMain.Location = new System.Drawing.Point(0, 435);
            this.ssMain.Name = "ssMain";
            this.ssMain.Size = new System.Drawing.Size(884, 26);
            this.ssMain.TabIndex = 2;
            this.ssMain.Text = "statusStrip1";
            // 
            // tsslTimer
            // 
            this.tsslTimer.Name = "tsslTimer";
            this.tsslTimer.Size = new System.Drawing.Size(0, 21);
            // 
            // tsslNumber
            // 
            this.tsslNumber.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left;
            this.tsslNumber.Name = "tsslNumber";
            this.tsslNumber.Size = new System.Drawing.Size(132, 21);
            this.tsslNumber.Text = "Original No.: 000000";
            // 
            // tsSend
            // 
            this.tsSend.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel3,
            this.tstbOrigin,
            this.toolStripSeparator5,
            this.toolStripLabel4,
            this.tstbTarget,
            this.toolStripLabel5,
            this.tstbData,
            this.toolStripSeparator6,
            this.tsbtSend,
            this.toolStripSeparator7,
            this.tsbtTest});
            this.tsSend.Location = new System.Drawing.Point(0, 25);
            this.tsSend.Name = "tsSend";
            this.tsSend.Size = new System.Drawing.Size(884, 25);
            this.tsSend.TabIndex = 3;
            this.tsSend.Text = "toolStrip1";
            // 
            // toolStripLabel3
            // 
            this.toolStripLabel3.Name = "toolStripLabel3";
            this.toolStripLabel3.Size = new System.Drawing.Size(51, 22);
            this.toolStripLabel3.Text = "Origin: ";
            // 
            // tstbOrigin
            // 
            this.tstbOrigin.Name = "tstbOrigin";
            this.tstbOrigin.Size = new System.Drawing.Size(100, 25);
            this.tstbOrigin.Text = "306614";
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripLabel4
            // 
            this.toolStripLabel4.Name = "toolStripLabel4";
            this.toolStripLabel4.Size = new System.Drawing.Size(53, 22);
            this.toolStripLabel4.Text = "Target: ";
            // 
            // tstbTarget
            // 
            this.tstbTarget.Name = "tstbTarget";
            this.tstbTarget.Size = new System.Drawing.Size(100, 25);
            this.tstbTarget.Text = "306614";
            // 
            // toolStripLabel5
            // 
            this.toolStripLabel5.Name = "toolStripLabel5";
            this.toolStripLabel5.Size = new System.Drawing.Size(42, 22);
            this.toolStripLabel5.Text = "Data: ";
            // 
            // tstbData
            // 
            this.tstbData.Name = "tstbData";
            this.tstbData.Size = new System.Drawing.Size(300, 25);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbtSend
            // 
            this.tsbtSend.Image = ((System.Drawing.Image)(resources.GetObject("tsbtSend.Image")));
            this.tsbtSend.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtSend.Name = "tsbtSend";
            this.tsbtSend.Size = new System.Drawing.Size(57, 22);
            this.tsbtSend.Text = "Send";
            this.tsbtSend.Click += new System.EventHandler(this.tsbtSend_Click);
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbtTest
            // 
            this.tsbtTest.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbtTest.Image = ((System.Drawing.Image)(resources.GetObject("tsbtTest.Image")));
            this.tsbtTest.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtTest.Name = "tsbtTest";
            this.tsbtTest.Size = new System.Drawing.Size(36, 22);
            this.tsbtTest.Text = "Test";
            this.tsbtTest.Click += new System.EventHandler(this.tsbtTest_Click);
            // 
            // scContainer
            // 
            this.scContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scContainer.Location = new System.Drawing.Point(0, 50);
            this.scContainer.Name = "scContainer";
            this.scContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // scContainer.Panel1
            // 
            this.scContainer.Panel1.Controls.Add(this.rtbHistory);
            // 
            // scContainer.Panel2
            // 
            this.scContainer.Panel2.Controls.Add(this.rtbGPS);
            this.scContainer.Size = new System.Drawing.Size(884, 385);
            this.scContainer.SplitterDistance = 355;
            this.scContainer.TabIndex = 4;
            // 
            // rtbHistory
            // 
            this.rtbHistory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbHistory.Location = new System.Drawing.Point(0, 0);
            this.rtbHistory.Name = "rtbHistory";
            this.rtbHistory.ReadOnly = true;
            this.rtbHistory.Size = new System.Drawing.Size(884, 355);
            this.rtbHistory.TabIndex = 2;
            this.rtbHistory.Tag = "0";
            this.rtbHistory.Text = "";
            // 
            // rtbGPS
            // 
            this.rtbGPS.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbGPS.Location = new System.Drawing.Point(0, 0);
            this.rtbGPS.Name = "rtbGPS";
            this.rtbGPS.ReadOnly = true;
            this.rtbGPS.Size = new System.Drawing.Size(884, 26);
            this.rtbGPS.TabIndex = 0;
            this.rtbGPS.Tag = "0";
            this.rtbGPS.Text = "";
            // 
            // tsslCapacity
            // 
            this.tsslCapacity.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left;
            this.tsslCapacity.Name = "tsslCapacity";
            this.tsslCapacity.Size = new System.Drawing.Size(64, 21);
            this.tsslCapacity.Text = "Capacity:";
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(884, 461);
            this.Controls.Add(this.scContainer);
            this.Controls.Add(this.tsSend);
            this.Controls.Add(this.ssMain);
            this.Controls.Add(this.tsMain);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Satellite Communication";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormMain_FormClosing);
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.Resize += new System.EventHandler(this.FormMain_Resize);
            this.tsMain.ResumeLayout(false);
            this.tsMain.PerformLayout();
            this.ssMain.ResumeLayout(false);
            this.ssMain.PerformLayout();
            this.tsSend.ResumeLayout(false);
            this.tsSend.PerformLayout();
            this.scContainer.Panel1.ResumeLayout(false);
            this.scContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.scContainer)).EndInit();
            this.scContainer.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip tsMain;
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
        private System.Windows.Forms.ToolStripComboBox tscbPort;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripComboBox tscbBaudrate;
        private System.IO.Ports.SerialPort spPort;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton tsbtOpen;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.StatusStrip ssMain;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton1;
        private System.Windows.Forms.ToolStripMenuItem tsmiDisplayHistory;
        private System.Windows.Forms.ToolStripMenuItem tsmiDisplayHex;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem tsmiSendAsHexData;
        private System.Windows.Forms.ToolStrip tsSend;
        private System.Windows.Forms.ToolStripLabel toolStripLabel3;
        private System.Windows.Forms.ToolStripTextBox tstbOrigin;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripLabel toolStripLabel4;
        private System.Windows.Forms.ToolStripTextBox tstbTarget;
        private System.Windows.Forms.ToolStripLabel toolStripLabel5;
        private System.Windows.Forms.ToolStripTextBox tstbData;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripButton tsbtSend;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem tsmiAnalyseData;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private System.Windows.Forms.ToolStripButton tsbtTest;
        private System.Windows.Forms.SplitContainer scContainer;
        private System.Windows.Forms.RichTextBox rtbHistory;
        private System.Windows.Forms.RichTextBox rtbGPS;
        private System.Windows.Forms.ToolStripStatusLabel tsslTimer;
        private System.Windows.Forms.ToolStripMenuItem tsmiShowGPSData;
        private System.Windows.Forms.ToolStripStatusLabel tsslNumber;
        private System.Windows.Forms.ToolStripButton tsbtICTEST;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator8;
        private System.Windows.Forms.ToolStripButton tsbtSystemTest;
        private System.Windows.Forms.ToolStripStatusLabel tsslCapacity;
    }
}

