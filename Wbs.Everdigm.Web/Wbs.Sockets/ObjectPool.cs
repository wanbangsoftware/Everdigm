using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Web;
using System.Diagnostics.Contracts;

namespace Wbs.Sockets
{
    /// <summary>
    /// 待处理数据缓冲池，线程安全
    /// </summary>
    public class ObjectPool<T>
    {
        private ConcurrentBag<T> _bag;
        private Func<T> _createFunc;
        private Action<T> _resetFunc;

        /// <summary>
        /// 创建一个数据缓冲池实例
        /// </summary>
        /// <param name="createFunc">初始化对象的方法</param>
        /// <param name="resetFunc">重置对象的方法</param>
        /// <param name="capacity">缓冲池容量大小，默认为100</param>
        public ObjectPool(Func<T> createFunc, Action<T> resetFunc, int capacity = 100)
        {
            Contract.Assume(createFunc != null);
            Contract.Assume(capacity > 0);

            _bag = new ConcurrentBag<T>();

            _createFunc = createFunc;
            _resetFunc = resetFunc;
            Capacity = capacity;
        }
        /// <summary>
        /// 获取缓冲池的容量
        /// </summary>
        public int Capacity { get; private set; }
        /// <summary>
        /// 获取缓冲池当前的大小
        /// </summary>
        public int Count { get { return _bag.Count; } }
        //public T Get(Func<T> find) {
        //    var obj=_bag.Select(find);
        //    //return _bag.
        //}
        /// <summary>
        /// 获取缓冲池中的对象
        /// </summary>
        /// <returns></returns>
        public T Get()
        {
            var obj = default(T);
            if (!_bag.TryTake(out obj))
                return _createFunc();
            return obj;
        }
        /// <summary>
        /// 将对象放入缓冲池，如果缓冲池已满则返回false
        /// </summary>
        /// <param name="item"></param>
        /// <returns>缓冲池大于设定容量时返回false，否则返回true</returns>
        public bool Push(T item)
        {
            Contract.Assume(item != null);

            if (Count > Capacity) {
                return false; 
            }

            if (null != _resetFunc)
                _resetFunc(item);

            _bag.Add(item);
            return true;
        }
    }
}