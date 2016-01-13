using System;
using System.Linq;

using System.Linq.Expressions;
using Wbs.Everdigm.Database;

namespace Wbs.Everdigm.BLL
{
    /// <summary>
    /// 业务层实现基类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class BaseService<T> : InterfaceBaseService<T> where T : class
    {
        protected BaseRepository<T> CurrentRepository { get; set; }

        public BaseService(BaseRepository<T> currentRepository) { CurrentRepository = currentRepository; }

        /// <summary>
        /// 添加纪录
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>返回刚刚插入的记录</returns>
        public T Add(T entity) { return CurrentRepository.Add(entity); }

        /// <summary>
        /// 更新记录
        /// </summary>
        /// <param name="predicate">条件表达式</param>
        /// <param name="action">更新内容</param>
        /// <returns>返回刚刚更新过后的记录</returns>
        public void Update(Expression<Func<T, bool>> predicate, Action<T> action)
        {
            CurrentRepository.Update(predicate, action);
        }

        /// <summary>
        /// 删除记录
        /// </summary>
        /// <param name="entity"></param>
        public void Delete(T entity) { CurrentRepository.Delete(entity); }
        /// <summary>
        /// 删除指定条件的所有记录
        /// </summary>
        /// <param name="predicate"></param>
        public void Delete(Expression<Func<T, bool>> predicate)
        {
            CurrentRepository.Delete(predicate);
        }
        /// <summary>
        /// 查找指定条件的单个实例
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public T Find(Expression<Func<T, bool>> predicate)
        {
            return CurrentRepository.Find(predicate);
        }
        /// <summary>
        /// 查找指定条件的列表
        /// </summary>
        /// <param name="predicate">查询条件的Lambda表达式</param>
        /// <returns></returns>
        public IQueryable<T> FindList(Expression<Func<T, bool>> predicate)
        {
            return CurrentRepository.FindList<T>(predicate);
        }
        /// <summary>
        /// 查找指定条件的列表并排序
        /// </summary>
        /// <typeparam name="S"></typeparam>
        /// <param name="whereLamdba">查询条件</param>
        /// <param name="orderNames">排序字段，可以按照","分割多个字段</param>
        /// <param name="descending">是否倒序，默认为false</param>
        /// <returns></returns>
        public IQueryable<T> FindList<S>(Expression<Func<T, bool>> whereLamdba,
            string orderNames, bool descending = false)
        {
            return CurrentRepository.FindList<S>(whereLamdba, orderNames, descending);
        }
        /// <summary>
        /// 查找指定条件的列表并排序然后返回指定页的数据
        /// </summary>
        /// <typeparam name="S"></typeparam>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页大小</param>
        /// <param name="totalRecord">返回查询条件的总条数</param>
        /// <param name="whereLamdba">查询条件</param>
        /// <param name="orderNames">排序字段，可以按照","分割多个字段</param>
        /// <param name="descending">是否倒序，默认为false</param>
        /// <returns></returns>
        public IQueryable<T> FindPageList<S>(int pageIndex, int pageSize, out int totalRecord,
            Expression<Func<T, bool>> whereLamdba, string orderNames, bool descending = false)
        {
            return CurrentRepository.FindPageList<S>(pageIndex, pageSize, out totalRecord, whereLamdba, orderNames, descending);
        }
        /// <summary>
        /// 强制生成新的实例
        /// </summary>
        /// <returns></returns>
        public abstract T GetObject();
        /// <summary>
        /// 将实例对象生成字符串
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public abstract string ToString(T entity);
    }
}
