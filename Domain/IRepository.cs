using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Domain
{
    public interface IRepository<T> where T : BaseDomain
    {
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        void Add(T model);
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        void Delete(Guid id);
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        List<T> Find(Expression<Func<T, bool>> expression);
        /// <summary>
        /// 第一个
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        T FirstOrDefault(Guid id);
        /// <summary>
        /// 第一个
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        T FirstOrDefault(Expression<Func<T, bool>> expression);
    }
}
