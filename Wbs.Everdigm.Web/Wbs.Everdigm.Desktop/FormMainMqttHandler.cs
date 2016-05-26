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
        private static string ClientId = ConfigurationManager.AppSettings["MQTT_SERVER_ID"];
        /// <summary>
        /// Mqtt服务端
        /// </summary>
        private MqttBroker broker;
        /// <summary>
        /// Mqtt客户端（订阅成server，相当于服务端）
        /// </summary>
        private MqttClient mqtt;
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
                broker.Stop();
            }
        }
        /// <summary>
        /// 初始化 mqtt 服务端订阅
        /// </summary>
        private void StartMqttClient()
        {
            if (null == mqtt)
            {
                mqtt = new MqttClient("127.0.0.1");
                // mqtt底层连接断开
                mqtt.ConnectionClosed += Mqtt_ConnectionClosed;
                // 收到mqtt连接消息
                mqtt.MqttMsgConnected += Mqtt_MqttMsgConnected;
                // 收到mqtt断开连接消息
                mqtt.MqttMsgDisconnected += Mqtt_MqttMsgDisconnected;
                // 收到mqtt发布成功的消息
                mqtt.MqttMsgPublished += Mqtt_MqttMsgPublished;
                mqtt.MqttMsgPublishReceived += Mqtt_MqttMsgPublishReceived;
            }
            SubscribeMqtt();
        }

        private void Mqtt_MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
        {
            ShowHistory(string.Format("Publish received, topic: {0}, qos: {1}, message: {2}", e.Topic, e.QosLevel, ASCIIEncoding.ASCII.GetString(e.Message)), true);
        }

        private void StopMqttClient()
        {
            if (null != mqtt)
            {
                mqtt.Unsubscribe(new string[] { ClientId });
                mqtt.Close();
            }
        }
        private void Mqtt_MqttMsgPublished(object sender, MqttMsgPublishedEventArgs e)
        {
            ShowHistory("Mqtt client published message: " + e.MessageId + " " + e.IsPublished, true);
        }

        private void Mqtt_MqttMsgDisconnected(object sender, EventArgs e)
        {
            ShowHistory("Mqtt client is closing now...", true);
        }

        private void Mqtt_ConnectionClosed(object sender, EventArgs e)
        {
            ShowHistory("Mqtt client has closed.", true);
        }

        private void Mqtt_MqttMsgConnected(object sender, MqttMsgConnectEventArgs e)
        {
            ShowHistory("Mqtt client has connected: " + e.Message.ToString(), true);
        }

        /// <summary>
        /// 连接、订阅mqtt
        /// </summary>
        private void SubscribeMqtt()
        {
            try
            {
                mqtt.Connect(ClientId);
                mqtt.Subscribe(new string[] { ClientId }, new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE });
            }
            catch (Exception e)
            {
                ShowHistory("Can not connect with client " + ClientId + Environment.NewLine + e.StackTrace, true);
            }
        }
    }
}
