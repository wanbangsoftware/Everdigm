using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using uPLibrary.Networking.M2MqttClient;
using uPLibrary.Networking.M2MqttClient.Messages;

namespace Wbs.Everdigm.Desktop
{
    /// <summary>
    /// M2Mqtt client 处理类
    /// </summary>
    public partial class FormMain
    {
        private static string ClientId = ConfigurationManager.AppSettings["MQTT_SERVER_ID"];
        /// <summary>
        /// Mqtt客户端（订阅成server，相当于服务端）
        /// </summary>
        private MqttClient mqtt;
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
                mqtt.MqttMsgSubscribed += Mqtt_MqttMsgSubscribed;
                mqtt.MqttMsgUnsubscribed += Mqtt_MqttMsgUnsubscribed;
                mqtt.MqttMsgPublished += Mqtt_MqttMsgPublished;
                mqtt.MqttMsgPublishReceived += Mqtt_MqttMsgPublishReceived;
            }
            SubscribeMqtt();
        }

        private void Mqtt_MqttMsgUnsubscribed(object sender, MqttMsgUnsubscribedEventArgs e)
        {
            ShowHistory(string.Format("Unsubscribed, msgId: {0}", e.MessageId), true);
        }

        private void Mqtt_MqttMsgSubscribed(object sender, MqttMsgSubscribedEventArgs e)
        {
            ShowHistory(string.Format("Subscribed, msgId: {0}, QoS: {1}", e.MessageId, e.GrantedQoSLevels), true);
        }

        private void Mqtt_MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
        {
            ShowHistory(string.Format("Publish received, topic: {0}, qos: {1}, message: {2}", e.Topic, e.QosLevel, Encoding.ASCII.GetString(e.Message)), true);
        }

        private void StopMqttClient()
        {
            if (null != mqtt && mqtt.IsConnected)
            {
                mqtt.Unsubscribe(new string[] { ClientId });
                mqtt.Disconnect();
            }
        }

        private void Mqtt_MqttMsgPublished(object sender, MqttMsgPublishedEventArgs e)
        {
            ShowHistory("Mqtt client published message: " + e.MessageId + " " + e.IsPublished, true);
        }

        private void Mqtt_MqttMsgDisconnected(object sender, EventArgs e)
        {
            ShowHistory("Mqtt client is closing now(disconnect command)...", true);
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
        /// Mqtt推送
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Mqtt_Publish(object sender, TrackerChatEvent e)
        {
            if (null != mqtt && mqtt.IsConnected)
            {
                try
                {
                    ShowHistory("Try to publish message to " + e.Target + ": " + e.Content, true);
                    mqtt.Publish(e.Target, Encoding.ASCII.GetBytes(e.Content));
                }
                catch (Exception ignore)
                {
                    ShowHistory("Publish data to " + e.Target + "(" + e.Content + ") failed: " + ignore.Message + Environment.NewLine + ignore.StackTrace, true);
                }
            }
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
