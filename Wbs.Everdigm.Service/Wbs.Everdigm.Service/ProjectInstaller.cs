using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Linq;
using System.Threading.Tasks;
using System.ServiceProcess;
using System.Diagnostics;

namespace Wbs.Everdigm.Service
{
    [RunInstaller(true)]
    public partial class ProjectInstaller : System.Configuration.Install.Installer
    {
        public ProjectInstaller()
        {
            InitializeComponent();
        }

        private void EverdigmNetworkServiceInstaller_AfterInstall(object sender, InstallEventArgs e)
        {
            ServiceController con = new ServiceController(SERVICE_NAME);
            con.Start();
        }

        private void EverdigmNetworkServiceInstaller_BeforeUninstall(object sender, InstallEventArgs e)
        {
            ServiceController con = new ServiceController(SERVICE_NAME);
            try
            {
                if (con.Status != ServiceControllerStatus.Stopped)
                {
                    con.Stop();
                }
            }
            catch
            {
            }
        }

    }
}
