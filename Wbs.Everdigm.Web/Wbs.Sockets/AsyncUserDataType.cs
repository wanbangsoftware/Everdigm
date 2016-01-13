namespace Wbs.Sockets
{
    /// <summary>
    /// 需要服务器处理的数据类型。
    /// </summary>
    public enum AsyncUserDataType
    {
        /// <summary>
        /// 客户端正常连接服务器。
        /// </summary>
        ClientConnected,
        /// <summary>
        /// 正常接收的数据。
        /// </summary>
        ReceivedData,
        /// <summary>
        /// 需要发送到指定客户端的数据。
        /// </summary>
        SendData,
        /// <summary>
        /// 客户端断开连接。
        /// </summary>
        ClientDisconnected,
        /// <summary>
        /// 未知数据类型，服务器不处理此类数据。
        /// </summary>
        None
    }
}
