using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Satellite
{
    /// <summary>
    /// 数据处理类
    /// </summary>
    public class DataHandler : IDisposable
    {
        /// <summary>
        /// 数据处理完毕事件
        /// </summary>
        public EventHandler<HandledData> OnDataHandled;
        //public EventHandler<> OnDataHandled;
        /// <summary>
        /// 有数据需要发送
        /// </summary>
        public EventHandler<DataPackage> OnDataSend;
        /// <summary>
        /// 待处理数据队列
        /// </summary>
        private ConcurrentQueue<DataPackage> _queue;
        /// <summary>
        /// 数据包实体缓冲区
        /// </summary>
        private ObjectPool<DataPackage> _bufferPool;
        /// <summary>
        /// 标记是否退出
        /// </summary>
        private bool HasStoped = false;
        /// <summary>
        /// 生成一个新的数据处理实例
        /// </summary>
        public DataHandler()
        { }
        /// <summary>
        /// 初始化相关资源
        /// </summary>
        private void Initialize()
        {
            _queue = new ConcurrentQueue<DataPackage>();
            _bufferPool = new ObjectPool<DataPackage>(() => new DataPackage(), action =>
            {
                action.Data = null;
                action.Type = DataType.Null;
            });

            InitializeThreadPool();
        }
        /// <summary>
        /// 初始化线程池
        /// </summary>
        private void InitializeThreadPool()
        {
            var count = Environment.ProcessorCount;
            // 只建立一个线程
            for (int i = 0; i < 1; i++)
                ThreadPool.QueueUserWorkItem(new WaitCallback(ThreadHandler));
        }
        /// <summary>
        /// 开始处理数据
        /// </summary>
        public void StartService()
        {
            Initialize();
        }
        /// <summary>
        /// 停止处理数据
        /// </summary>
        public void StopService()
        {
            HasStoped = true;
            Dispose();
        }
        /// <summary>
        /// 获取一个空的数据包实体
        /// </summary>
        /// <returns></returns>
        public DataPackage GetBlankDataPackage()
        { return _bufferPool.Get(); }
        /// <summary>
        /// 回收数据包实体
        /// </summary>
        /// <param name="obj"></param>
        public void RecycleDataPackage(DataPackage obj)
        {
            if (!_bufferPool.Push(obj))
            {
                obj.Dispose();
                obj = null;
            }
        }
        /// <summary>
        /// 将接收到的数据放入待处理队列中
        /// </summary>
        /// <param name="obj"></param>
        public void AddMessage(DataPackage obj)
        {
            _queue.Enqueue(obj);
        }
        /// <summary>
        /// 线程处理过程
        /// </summary>
        private void ThreadHandler(Object state)
        {
            DataPackage data;
            while (true)
            {
                Thread.Sleep(10);
                if (HasStoped) break;

                if (!_queue.TryDequeue(out data))
                {
                    data = null;
                }

                if (null == data) continue;

                // 处理接收到的串口数据
                switch (data.Type)
                {
                    case DataType.Received:
                        HandleReceivedData(data);
                        break;
                    case DataType.Send:
                        HandleSendData(data);
                        break;
                }
            }
        }
        /// <summary>
        /// 处理接收到的数据
        /// </summary>
        /// <param name="data"></param>
        private void HandleReceivedData(DataPackage data)
        {
            var obj = new SatellitePackage(data.Data);
            //obj.Unpackage();
            switch (obj.Command)
            {
                case "$TXSQ":
                    HandleTXSQ(obj);
                    break;
                case "$TXXX":
                    HandleTXXX(obj);
                    break;
                case "$FKXX":
                    HandleFKXX(obj);
                    break;
                case "$ICJC":
                    HandleICJC(obj);
                    break;
                case "$XTZJ":
                    HandleXTZJ(obj);
                    break;
                case "$ICXX":
                    HandleICXX(obj);
                    break;
                case "$ZJXX":
                    HandleZJXX(obj);
                    break;
            }
            // 回收数据结构
            RecycleDataPackage(data);
        }
        /// <summary>
        /// 发起数据处理完毕事件
        /// </summary>
        /// <param name="list"></param>
        private void HandleEvent(List<string> list, SatellitePackage data)
        {
            if (null != OnDataHandled)
            {
                OnDataHandled(this, new HandledData() { Message = list, Data = data });
            }
        }
        /// <summary>
        /// 处理IC检查数据包
        /// </summary>
        /// <param name="obj"></param>
        private void HandleICJC(SatellitePackage obj)
        {
            List<string> list = new List<string>();
            ICJC icjc = new ICJC();
            icjc.Content = obj.Content;
            icjc.Unpackage();
            list.Add(icjc.ToString());
            HandleEvent(list, icjc);
        }
        /// <summary>
        /// 处理IC信息数据解包
        /// </summary>
        /// <param name="obj"></param>
        private void HandleICXX(SatellitePackage obj)
        {
            List<string> list = new List<string>();
            ICXX icxx = new ICXX();
            icxx.Content = obj.Content;
            icxx.Unpackage();
            list.Add(icxx.ToString());
            HandleEvent(list, icxx);
        }
        /// <summary>
        /// 处理系统自检数据解包
        /// </summary>
        /// <param name="obj"></param>
        private void HandleXTZJ(SatellitePackage obj)
        {
            List<string> list = new List<string>();
            XTZJ xtzj = new XTZJ();
            xtzj.Content = obj.Content;
            xtzj.Unpackage();
            list.Add(xtzj.ToString());
            HandleEvent(list, xtzj);
        }
        /// <summary>
        /// 处理系统信息数据解包
        /// </summary>
        /// <param name="obj"></param>
        private void HandleZJXX(SatellitePackage obj)
        {
            List<string> list = new List<string>();
            ZJXX zjxx = new ZJXX();
            zjxx.Content = obj.Content;
            zjxx.Unpackage();
            list.Add(zjxx.ToString());
            HandleEvent(list, zjxx);
        }
        /// <summary>
        /// 处理通信申请数据解包
        /// </summary>
        /// <param name="obj"></param>
        private void HandleTXSQ(SatellitePackage obj)
        {
            List<string> list = new List<string>();
            TXSQ txsq = new TXSQ();
            txsq.Content = obj.Content;
            txsq.Unpackage();
            list.Add(txsq.ToString());
            HandleEvent(list, txsq);
        }
        /// <summary>
        /// 处理通信信息数据解包
        /// </summary>
        /// <param name="obj"></param>
        private void HandleTXXX(SatellitePackage obj)
        {
            TXXX txxx = new TXXX();
            txxx.Content = obj.Content;
            txxx.Unpackage();
            var list = new List<string>();
            list.Add(txxx.ToString());
            HandleEvent(list, txxx);
        }
        /// <summary>
        /// 处理反馈信息数据解包
        /// </summary>
        /// <param name="obj"></param>
        private void HandleFKXX(SatellitePackage obj)
        {
            FKXX fkxx = new FKXX();
            fkxx.Content = obj.Content;
            fkxx.Unpackage();
            var list = new List<string>();
            list.Add(fkxx.ToString());
            HandleEvent(list, fkxx);
        }
        /// <summary>
        /// 处理要发送的数据
        /// </summary>
        /// <param name="data"></param>
        private void HandleSendData(DataPackage data)
        {
            if (null != OnDataSend)
            {
                OnDataSend(this, data);
            }
        }
        ~DataHandler()
        { Dispose(); }
        /// <summary>
        /// 清理资源
        /// </summary>
        public void Dispose()
        {
            if (null != _queue && _queue.Count > 0)
            {
                DataPackage data;
                while (_queue.TryDequeue(out data))
                {
                    data.Dispose();
                }
                data = null;
            }
            _queue = null;

            _bufferPool.Clear();
        }
    }
}
