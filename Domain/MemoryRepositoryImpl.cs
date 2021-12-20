using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using DotNetty_Common;


namespace Domain
{
    public abstract class MemoryRepositoryImpl<T> : IRepository<T> where T : BaseDomain
    {
        private readonly List<T> _data;

        protected MemoryRepositoryImpl()
        {
            Type tType = typeof(T);
            if (!DBContext.DataBase.ContainsKey(tType))
            {
                DBContext.DataBase.Add(tType, new List<T>());
            }
            _data = DBContext.DataBase[tType] as List<T>;
        }
        public void Add(T model)
        {
            if (model.ID == Guid.Empty) model.ID = Guid.NewGuid();
            if (_data.Any(m => m.ID == model.ID)) throw new DotNettyServerException("唯一标识重复");
            _data.Add(model);
        }
        public void Delete(Guid id)
        {
            T modelFromDB = FirstOrDefault(id);
            if(modelFromDB == null) throw new DotNettyServerException("唯一标识不存在");
            _data.Remove(modelFromDB);
        }
        public List<T> Find(Expression<Func<T, bool>> expression)
        {
            return _data.Where(expression.Compile()).ToList();
        }
        public T FirstOrDefault(Guid id)
        {
            return FirstOrDefault(m => m.ID == id);
        }
        public T FirstOrDefault(Expression<Func<T, bool>> expression)
        {
            return _data.FirstOrDefault(expression.Compile());
        }
    }
}
