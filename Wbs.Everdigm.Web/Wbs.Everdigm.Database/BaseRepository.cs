using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Wbs.Everdigm.Database
{
    /// <summary>
    /// 数据库操作基类
    /// <remarks>创建：2014.11.15</remarks>
    /// <para>增加多线程时解决冲突的方法（自定义SubmitChange方法）2015-10-21</para>
    /// </summary>
    public class BaseRepository<T> : InterfaceBaseRepository<T> where T : class
    {
        protected EverdigmDataContext context = new EverdigmDataContext();

        /// <summary>
        /// 自定义submitchanges方法避免冲突
        /// </summary>
        protected virtual void SubmitChanges()
        {
            ChangeSet cSet = context.GetChangeSet();
            if (cSet.Inserts.Count > 0
                 || cSet.Updates.Count > 0
                 || cSet.Deletes.Count > 0)
            {
                try
                {
                    context.SubmitChanges(System.Data.Linq.ConflictMode.ContinueOnConflict);
                }
                catch (System.Data.Linq.ChangeConflictException)
                {
                    foreach (System.Data.Linq.ObjectChangeConflict occ in context.ChangeConflicts)
                    {
                        occ.Resolve(System.Data.Linq.RefreshMode.OverwriteCurrentValues);
                        occ.Resolve(System.Data.Linq.RefreshMode.KeepCurrentValues);
                        occ.Resolve(System.Data.Linq.RefreshMode.KeepChanges);
                    }
                    context.SubmitChanges();
                }
            }
        }
        /// <summary>
        /// 添加数据库记录
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public T Add(T entity)
        {
            context.GetTable<T>().InsertOnSubmit(entity);
            SubmitChanges();
            return entity;
        }

        /// <summary>
        /// 查询记录数
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public int Count(Expression<Func<T, bool>> predicate)
        {
            return context.GetTable<T>().Count(predicate);
        }
        /// <summary>
        /// 更新指定查询条件的记录
        /// </summary>
        /// <param name="predicate">查询条件</param>
        /// <param name="action">更新内容</param>
        public void Update(Expression<Func<T, bool>> predicate, Action<T> action)
        {
            var list = context.GetTable<T>().Where<T>(predicate);
            //var t = context.GetTable<T>().SingleOrDefault<T>(predicate);
            if (list.Count() > 0)
            {
                foreach (var t in list)
                    action(t);
                SubmitChanges();
            }
            //return t;
        }

        /// <summary>
        /// 删除记录
        /// </summary>
        /// <param name="entity"></param>
        public void Delete(T entity)
        {
            context.GetTable<T>().DeleteOnSubmit(entity);
            SubmitChanges();
        }

        /// <summary>
        /// 查找指定条件的记录是否存在
        /// </summary>
        /// <param name="anyLambda"></param>
        /// <returns></returns>
        public bool Exist(Expression<Func<T, bool>> anyLambda)
        {
            return context.GetTable<T>().Any(anyLambda);
        }

        /// <summary>
        /// 查找指定条件的记录
        /// </summary>
        /// <param name="whereLambda"></param>
        /// <returns></returns>
        public T Find(Expression<Func<T, bool>> whereLambda)
        {
            T _entity = context.GetTable<T>().FirstOrDefault<T>(whereLambda);
            return _entity;
        }
        /// <summary>
        /// 查找指定条件的记录列表
        /// </summary>
        /// <typeparam name="S"></typeparam>
        /// <param name="whereLamdba"></param>
        /// <returns></returns>
        public IQueryable<T> FindList<S>(Expression<Func<T, bool>> whereLamdba)
        {
            return FindList<S>(whereLamdba, null);
        }

        /// <summary>
        /// 查找指定条件的记录列表并指定排序方式
        /// </summary>
        /// <typeparam name="S"></typeparam>
        /// <param name="whereLamdba">条件表达式</param>
        /// <param name="orderNames">排序字段，可以已","分割多个字段</param>
        /// <param name="isAsc">顺序或倒序</param>
        /// <returns></returns>
        public IQueryable<T> FindList<S>(Expression<Func<T, bool>> whereLamdba, string orderNames, bool descending = false)
        {
            var _list = (null == whereLamdba ?
                context.GetTable<T>() :
                context.GetTable<T>().Where<T>(whereLamdba));

            if (null != orderNames)
                _list = Sort(_list, orderNames, descending);
            return _list;
        }
        /// <summary>
        /// 查找指定条件的记录并按页返回
        /// </summary>
        /// <typeparam name="S"></typeparam>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalRecord"></param>
        /// <param name="whereLamdba"></param>
        /// <param name="orderNames">排序字段，可以用","分割多个字段</param>
        /// <param name="isAsc"></param>
        /// <returns></returns>
        public IQueryable<T> FindPageList<S>(int pageIndex, int pageSize, out int totalRecord,
            Expression<Func<T, bool>> whereLamdba, string orderNames, bool descending = false)
        {
            var _list = (null == whereLamdba ? 
                context.GetTable<T>() : 
                context.GetTable<T>().Where<T>(whereLamdba));

            totalRecord = _list.Count();

            if (null != orderNames)
                _list = Sort(_list, orderNames, descending);
            _list = _list.Skip<T>((pageIndex - 1) * pageSize).Take<T>(pageSize);
            return _list;
        }

        private IQueryable<T> Sort(IQueryable<T> source, string propertyName, bool descending = false)
        {
            if (source == null)
                throw new ArgumentNullException("source", "cannot be null");
            if (string.IsNullOrEmpty(propertyName))
                return source;

            // 排序返回
            IOrderedQueryable<T> ret = null;

            var names = propertyName.Split(new char[] { ',' });
            var cnt = 0;
            foreach (var name in names)
            {
                if (!string.IsNullOrEmpty(name))
                {
                    ret = (0 == cnt ? BaseRepositorySortFunc.OrderBy<T>(source, name, descending) :
                        BaseRepositorySortFunc.ThenBy<T>(ret, name, descending));
                    cnt++;
                }
            }
            return null == ret ? source : ret;
        }

        /// <summary>
        /// 排序
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="source">原IQueryable</param>
        /// <param name="propertyName">排序属性名</param>
        /// <param name="isAsc">是否正序</param>
        /// <returns>排序后的IQueryable<T></returns>
        private IQueryable<T> OrderBy(IQueryable<T> source, string propertyName, bool isAsc)
        {
            if (source == null) 
                throw new ArgumentNullException("source", "不能为空");
            if (string.IsNullOrEmpty(propertyName)) 
                return source;
            var _parameter = Expression.Parameter(source.ElementType);
            var _property = Expression.Property(_parameter, propertyName);
            if (_property == null) 
                throw new ArgumentNullException("propertyName", "属性\"" + propertyName + "\"不存在");
            var _lambda = Expression.Lambda(_property, _parameter);
            var _methodName = isAsc ? "OrderBy" : "OrderByDescending";
            var _resultExpression = Expression.Call(typeof(Queryable), _methodName, 
                new Type[] { source.ElementType, _property.Type }, source.Expression, Expression.Quote(_lambda));
            return source.Provider.CreateQuery<T>(_resultExpression);
        }
    }
}
