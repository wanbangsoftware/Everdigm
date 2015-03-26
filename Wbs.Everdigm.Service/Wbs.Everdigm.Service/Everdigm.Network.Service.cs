using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Wbs.Everdigm.Service
{
    /// <summary>
    /// Everdigm的网络服务
    /// </summary>
    public partial class EverdigmNetworkService : ServiceBase
    {
        public EverdigmNetworkService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            StartService();
        }

        /// <summary>
        /// 启动服务
        /// </summary>
        public void StartService()
        {
            Thread.Sleep(20000);
            StaticService.StartService();
        }

        protected override void OnStop()
        {
            StaticService.StopService();
        }
    }
}
