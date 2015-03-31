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
            this.tsdbSettings});
            this.tsMain.Location = new System.Drawing.Point(0, 0);
            this.tsMain.Name = "tsMain";
            this.tsMain.Size = new System.Drawing.Size(728, 25);
            this.tsMain.TabIndex = 0;
            this.tsMain.Text = "toolStrip1";
            // 
            // tsdbSettings
            // 
            this.tsdbSettings.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiShowHistory});
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
            this.tsmiShowHistory.Size = new System.Drawing.Size(152, 22);
            this.tsmiShowHistory.Text = "Show History";
            this.tsmiShowHistory.Click += new System.EventHandler(this.tsmiShowHistory_Click);
            // 
            // ssMain
            // 
            this.ssMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsslServerState});
            this.ssMain.Location = new System.Drawing.Point(0, 406);
            this.ssMain.Name = "ssMain";
            this.ssMain.Size = new System.Drawing.Size(728, 26);
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
            this.rtbHistory.Size = new System.Drawing.Size(728, 381);
            this.rtbHistory.TabIndex = 2;
            this.rtbHistory.Text = "";
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
            this.ClientSize = new System.Drawing.Size(728, 432);
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
    }
}

