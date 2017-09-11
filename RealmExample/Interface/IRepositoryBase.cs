using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System.Linq.Expressions;

namespace RealmExample.Interface
{
    public interface IRepositoryBase<TEntity> : IDisposable where TEntity : class, IContractID
    {
        void Add(TEntity obj);
        TEntity GetById(string id);
        IEnumerable<TEntity> GetAll();
        void Update(TEntity obj);
        void RemoveById(string id);
        void RemoveByPredicate(Expression<Func<TEntity, bool>> predicate);
        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);
    }
}