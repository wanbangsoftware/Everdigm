namespace Wbs.Everdigm.Desktop
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
            this.tsdbSettings = new System.Windows.Forms.ToolStripDropDownButton();
            this.tsmiShowHistory = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiShowIridiumPackage = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiStopFetchingAddress = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.tstbIridiumServer = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.tstbIridiumPort = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel3 = new System.Windows.Forms.ToolStripLabel();
            this.tscbIMEI = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripSplitButton1 = new System.Windows.Forms.ToolStripSplitButton();
            this.tsmiFlushMtQueue = new System.Windows.Forms.ToolStripMenuItem();
            this.tsbtSend = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.tstbData = new System.Windows.Forms.ToolStripTextBox();
            this.tsbtAnalyse = new System.Windows.Forms.ToolStripButton();
            this.ssMain = new System.Windows.Forms.StatusStrip();
            this.tsslServerState = new System.Windows.Forms.ToolStripStatusLabel();
            this.rtbHistory = new System.Windows.Forms.RichTextBox();
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.cmsNotifyMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiShowMainForm = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiStartService = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiStopService = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiExit = new System.Windows.Forms.ToolStripMenuItem();
            this.browser = new System.Windows.Forms.WebBrowser();
            this.tsMain.SuspendLayout();
            this.ssMain.SuspendLayout();
            this.cmsNotifyMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // tsMain
            // 
            this.tsMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsdbSettings,
            this.toolStripSeparator1,
            this.toolStripLabel1,
            this.tstbIridiumServer,
            this.toolStripSeparator2,
            this.toolStripLabel2,
            this.tstbIridiumPort,
            this.toolStripSeparator3,
            this.toolStripLabel3,
            this.tscbIMEI,
            this.toolStripSplitButton1,
            this.tsbtSend,
            this.toolStripSeparator4,
            this.tstbData,
            this.tsbtAnalyse});
            this.tsMain.Location = new System.Drawing.Point(0, 0);
            this.tsMain.Name = "tsMain";
            this.tsMain.Size = new System.Drawing.Size(998, 25);
            this.tsMain.TabIndex = 0;
            this.tsMain.Text = "toolStrip1";
            // 
            // tsdbSettings
            // 
            this.tsdbSettings.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiShowHistory,
            this.tsmiShowIridiumPackage,
            this.toolStripMenuItem2,
            this.tsmiStopFetchingAddress});
            this.tsdbSettings.Image = ((System.Drawing.Image)(resources.GetObject("tsdbSettings.Image")));
            this.tsdbSettings.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsdbSettings.Name = "tsdbSettings";
            this.tsdbSettings.Size = new System.Drawing.Size(80, 22);
            this.tsdbSettings.Text = "Setting:";
            // 
            // tsmiShowHistory
            // 
            this.tsmiShowHistory.Checked = true;
            this.tsmiShowHistory.CheckState = System.Windows.Forms.CheckState.Checked;
            this.tsmiShowHistory.Name = "tsmiShowHistory";
            this.tsmiShowHistory.Size = new System.Drawing.Size(316, 22);
            this.tsmiShowHistory.Text = "Show History";
            this.tsmiShowHistory.Click += new System.EventHandler(this.tsmiShowHistory_Click);
            // 
            // tsmiShowIridiumPackage
            // 
            this.tsmiShowIridiumPackage.Name = "tsmiShowIridiumPackage";
            this.tsmiShowIridiumPackage.Size = new System.Drawing.Size(316, 22);
            this.tsmiShowIridiumPackage.Text = "Show Iridium packages";
            this.tsmiShowIridiumPackage.Click += new System.EventHandler(this.tsmiShowIridiumPackage_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(313, 6);
            // 
            // tsmiStopFetchingAddress
            // 
            this.tsmiStopFetchingAddress.Name = "tsmiStopFetchingAddress";
            this.tsmiStopFetchingAddress.Size = new System.Drawing.Size(316, 22);
            this.tsmiStopFetchingAddress.Text = "Stop Fetching Address from Google map";
            this.tsmiStopFetchingAddress.Click += new System.EventHandler(this.tsmiStopFetchingAddress_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(92, 22);
            this.toolStripLabel1.Text = "Iridium server:";
            // 
            // tstbIridiumServer
            // 
            this.tstbIridiumServer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tstbIridiumServer.Name = "tstbIridiumServer";
            this.tstbIridiumServer.Size = new System.Drawing.Size(100, 25);
            this.tstbIridiumServer.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tstbIridiumServer_KeyPress);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(35, 22);
            this.toolStripLabel2.Text = "Port:";
            // 
            // tstbIridiumPort
            // 
            this.tstbIridiumPort.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tstbIridiumPort.MaxLength = 5;
            this.tstbIridiumPort.Name = "tstbIridiumPort";
            this.tstbIridiumPort.Size = new System.Drawing.Size(50, 25);
            this.tstbIridiumPort.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tstbIridiumPort_KeyPress);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripLabel3
            // 
            this.toolStripLabel3.Name = "toolStripLabel3";
            this.toolStripLabel3.Size = new System.Drawing.Size(38, 22);
            this.toolStripLabel3.Text = "IMEI:";
            // 
            // tscbIMEI
            // 
            this.tscbIMEI.DropDownWidth = 130;
            this.tscbIMEI.Items.AddRange(new object[] {
            "300234061921380",
            "300234061944960",
            "300234062060500"});
            this.tscbIMEI.Name = "tscbIMEI";
            this.tscbIMEI.Size = new System.Drawing.Size(130, 25);
            // 
            // toolStripSplitButton1
            // 
            this.toolStripSplitButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiFlushMtQueue});
            this.toolStripSplitButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripSplitButton1.Image")));
            this.toolStripSplitButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripSplitButton1.Name = "toolStripSplitButton1";
            this.toolStripSplitButton1.Size = new System.Drawing.Size(67, 22);
            this.toolStripSplitButton1.Text = "Flag:";
            // 
            // tsmiFlushMtQueue
            // 
            this.tsmiFlushMtQueue.Name = "tsmiFlushMtQueue";
            this.tsmiFlushMtQueue.Size = new System.Drawing.Size(170, 22);
            this.tsmiFlushMtQueue.Text = "Flush MT Queue";
            this.tsmiFlushMtQueue.Click += new System.EventHandler(this.tsmiFlushMtQueue_Click);
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
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
            // 
            // tstbData
            // 
            this.tstbData.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tstbData.Name = "tstbData";
            this.tstbData.Size = new System.Drawing.Size(100, 25);
            // 
            // tsbtAnalyse
            // 
            this.tsbtAnalyse.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbtAnalyse.Image = ((System.Drawing.Image)(resources.GetObject("tsbtAnalyse.Image")));
            this.tsbtAnalyse.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtAnalyse.Name = "tsbtAnalyse";
            this.tsbtAnalyse.Size = new System.Drawing.Size(56, 22);
            this.tsbtAnalyse.Text = "Analyse";
            this.tsbtAnalyse.Click += new System.EventHandler(this.tsbtAnalyse_Click);
            // 
            // ssMain
            // 
            this.ssMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsslServerState});
            this.ssMain.Location = new System.Drawing.Point(0, 406);
            this.ssMain.Name = "ssMain";
            this.ssMain.Size = new System.Drawing.Size(998, 26);
            this.ssMain.TabIndex = 1;
            this.ssMain.Text = "statusStrip1";
            // 
            // tsslServerState
            // 
            this.tsslServerState.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.tsslServerState.Name = "tsslServerState";
            this.tsslServerState.Size = new System.Drawing.Size(103, 21);
            this.tsslServerState.Text = "Server: Running";
            // 
            // rtbHistory
            // 
            this.rtbHistory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbHistory.Location = new System.Drawing.Point(0, 25);
            this.rtbHistory.Name = "rtbHistory";
            this.rtbHistory.Size = new System.Drawing.Size(998, 381);
            this.rtbHistory.TabIndex = 2;
            this.rtbHistory.Text = "";
            this.rtbHistory.WordWrap = false;
            // 
            // notifyIcon
            // 
            this.notifyIcon.ContextMenuStrip = this.cmsNotifyMenu;
            this.notifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon.Icon")));
            this.notifyIcon.Text = "Everdigm Terminal Network Service";
            this.notifyIcon.Visible = true;
            this.notifyIcon.DoubleClick += new System.EventHandler(this.notifyIcon_DoubleClick);
            // 
            // cmsNotifyMenu
            // 
            this.cmsNotifyMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiShowMainForm,
            this.toolStripMenuItem1,
            this.tsmiStartService,
            this.tsmiStopService,
            this.toolStripMenuItem3,
            this.tsmiExit});
            this.cmsNotifyMenu.Name = "cmsNotifyMenu";
            this.cmsNotifyMenu.Size = new System.Drawing.Size(175, 104);
            // 
            // tsmiShowMainForm
            // 
            this.tsmiShowMainForm.Enabled = false;
            this.tsmiShowMainForm.Name = "tsmiShowMainForm";
            this.tsmiShowMainForm.Size = new System.Drawing.Size(174, 22);
            this.tsmiShowMainForm.Text = "Show Main Form";
            this.tsmiShowMainForm.Click += new System.EventHandler(this.tsmiShowMainForm_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(171, 6);
            // 
            // tsmiStartService
            // 
            this.tsmiStartService.Name = "tsmiStartService";
            this.tsmiStartService.Size = new System.Drawing.Size(174, 22);
            this.tsmiStartService.Text = "Start Service";
            // 
            // tsmiStopService
            // 
            this.tsmiStopService.Name = "tsmiStopService";
            this.tsmiStopService.Size = new System.Drawing.Size(174, 22);
            this.tsmiStopService.Text = "Stop Service";
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(171, 6);
            // 
            // tsmiExit
            // 
            this.tsmiExit.Name = "tsmiExit";
            this.tsmiExit.Size = new System.Drawing.Size(174, 22);
            this.tsmiExit.Text = "Exit";
            this.tsmiExit.Click += new System.EventHandler(this.tsmiExit_Click);
            // 
            // browser
            // 
            this.browser.Location = new System.Drawing.Point(474, 157);
            this.browser.MinimumSize = new System.Drawing.Size(20, 20);
            this.browser.Name = "browser";
            this.browser.Size = new System.Drawing.Size(250, 250);
            this.browser.TabIndex = 3;
            this.browser.Visible = false;
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(998, 432);
            this.Controls.Add(this.browser);
            this.Controls.Add(this.rtbHistory);
            this.Controls.Add(this.ssMain);
            this.Controls.Add(this.tsMain);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Everdigm Terminal Control System Service";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormMain_FormClosing);
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.Resize += new System.EventHandler(this.FormMain_Resize);
            this.tsMain.ResumeLayout(false);
            this.tsMain.PerformLayout();
            this.ssMain.ResumeLayout(false);
            this.ssMain.PerformLayout();
            this.cmsNotifyMenu.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip tsMain;
        private System.Windows.Forms.StatusStrip ssMain;
        private System.Windows.Forms.RichTextBox rtbHistory;
        private System.Windows.Forms.ToolStripDropDownButton tsdbSettings;
        private System.Windows.Forms.ToolStripStatusLabel tsslServerState;
        private System.Windows.Forms.ToolStripMenuItem tsmiShowHistory;
        private System.Windows.Forms.NotifyIcon notifyIcon;
        private System.Windows.Forms.ContextMenuStrip cmsNotifyMenu;
        private System.Windows.Forms.ToolStripMenuItem tsmiShowMainForm;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem tsmiStartService;
        private System.Windows.Forms.ToolStripMenuItem tsmiStopService;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem tsmiExit;
        private System.Windows.Forms.WebBrowser browser;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem tsmiStopFetchingAddress;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripTextBox tstbIridiumServer;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
        private System.Windows.Forms.ToolStripTextBox tstbIridiumPort;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripTextBox tstbData;
        private System.Windows.Forms.ToolStripButton tsbtAnalyse;
        private System.Windows.Forms.ToolStripLabel toolStripLabel3;
        private System.Windows.Forms.ToolStripButton tsbtSend;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripComboBox tscbIMEI;
        private System.Windows.Forms.ToolStripSplitButton toolStripSplitButton1;
        private System.Windows.Forms.ToolStripMenuItem tsmiFlushMtQueue;
        private System.Windows.Forms.ToolStripMenuItem tsmiShowIridiumPackage;
    }
}

