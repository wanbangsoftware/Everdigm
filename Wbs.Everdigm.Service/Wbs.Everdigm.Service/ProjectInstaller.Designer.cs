namespace Wbs.Everdigm.Service
{
    partial class ProjectInstaller
    {
        /// <summary>
        /// 服务的名称
        /// </summary>
        public static string SERVICE_NAME = "EverdigmNetworkService";
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

        #region 组件设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.serviceProcessInstaller = new System.ServiceProcess.ServiceProcessInstaller();
            this.EverdigmNetworkServiceInstaller = new System.ServiceProcess.ServiceInstaller();
            // 
            // serviceProcessInstaller
            // 
            this.serviceProcessInstaller.Account = System.ServiceProcess.ServiceAccount.LocalService;
            this.serviceProcessInstaller.Password = null;
            this.serviceProcessInstaller.Username = null;
            // 
            // EverdigmNetworkServiceInstaller
            // 
            this.EverdigmNetworkServiceInstaller.Description = "Everdigm Terminal Control System Network Service";
            this.EverdigmNetworkServiceInstaller.DisplayName = "Everdigm Network Service";
            this.EverdigmNetworkServiceInstaller.ServiceName = "EverdigmNetworkService";
            this.EverdigmNetworkServiceInstaller.StartType = System.ServiceProcess.ServiceStartMode.Automatic;
            this.EverdigmNetworkServiceInstaller.AfterInstall += new System.Configuration.Install.InstallEventHandler(this.EverdigmNetworkServiceInstaller_AfterInstall);
            this.EverdigmNetworkServiceInstaller.BeforeUninstall += new System.Configuration.Install.InstallEventHandler(this.EverdigmNetworkServiceInstaller_BeforeUninstall);
            // 
            // ProjectInstaller
            // 
            this.Installers.AddRange(new System.Configuration.Install.Installer[] {
            this.serviceProcessInstaller,
            this.EverdigmNetworkServiceInstaller});

        }

        #endregion

        private System.ServiceProcess.ServiceProcessInstaller serviceProcessInstaller;
        private System.ServiceProcess.ServiceInstaller EverdigmNetworkServiceInstaller;
    }
}