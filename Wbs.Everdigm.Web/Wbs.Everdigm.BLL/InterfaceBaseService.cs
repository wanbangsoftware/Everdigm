using System;
using System.Linq;

using System.Linq.Expressions;

namespace Wbs.Everdigm.BLL
{
    /// <summary>
    /// 业务层基类接口，基本就是数据库的增、删、改
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface InterfaceBaseService<T> where T : class
    {
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="entity">数据实体</param>
        /// <returns>添加后的数据实体</returns>
        T Add(T entity);

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        void Update(Expression<Func<T, bool>> predicate, Action<T> action);

        /// <summary>
        /// 查找指定条件的单个实例
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        T Find(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// 查找列表
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        IQueryable<T> FindList(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// 查找数据列表
        /// </summary>
        /// <typeparam name="S">排序</typeparam>
        /// <param name="whereLamdba">查询表达式</param>
        /// <param name="isAsc">是否升序</param>
        /// <param name="orderLamdba">排序表达式</param>
        /// <returns></returns>
        IQueryable<T> FindList<S>(Expression<Func<T, bool>> whereLamdba, string orderName, bool descending);

        /// <summary>
        /// 查找分页数据列表
        /// </summary>
        /// <typeparam name="S">排序</typeparam>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="totalRecord">总记录数</param>
        /// <param name="whereLamdba">查询表达式</param>
        /// <param name="isAsc">是否升序</param>
        /// <param name="orderLamdba">排序表达式</param>
        /// <returns></returns>
        IQueryable<T> FindPageList<S>(int pageIndex, int pageSize, out int totalRecord,
            Expression<Func<T, bool>> whereLamdba, string orderName, bool descending);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="entity">数据实体</param>
        /// <returns>是否成功</returns>
        void Delete(T entity);
    }
}
