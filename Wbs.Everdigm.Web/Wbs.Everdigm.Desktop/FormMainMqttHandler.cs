using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Configuration;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;

namespace Wbs.Everdigm.Desktop
{
    public partial class FormMain
    {
        /// <summary>
        /// Mqtt服务端
        /// </summary>
        private MqttBroker broker;
        /// <summary>
        /// 启动Mqtt Broker
        /// </summary>
        private void StartMqttBroker()
        {
            // 启动mqtt服务
            if (null == broker)
            {
                broker = new MqttBroker();
                broker.Start();
            }
        }
        /// <summary>
        /// 停止Mqtt Broker
        /// </summary>
        private void StopMqttBroker()
        {
            if (null != broker)
            {
                try
                {
                    broker.Stop();
                }
                catch { }
            }
        }
        
    }
}
