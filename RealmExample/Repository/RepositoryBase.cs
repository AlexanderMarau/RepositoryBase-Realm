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
using RealmExample.Interface;
using Realms;

namespace RealmExample.Repository
{
    public class RepositoryBase<TEntity> : IRepositoryBase<TEntity> where TEntity : RealmObject, IContractID
    {
        private readonly Realm realm;

        public RepositoryBase(Realm realm)
        {
            this.realm = realm;
        }

        public void Add(TEntity obj)
        {
            if (string.IsNullOrEmpty(obj.Id))
                obj.Id = Guid.NewGuid().ToString();

            realm.Add(obj);
        }

        public void Dispose()
        {
            realm.Dispose();
            GC.SuppressFinalize(this);
        }

        public IEnumerable<TEntity> Find(System.Linq.Expressions.Expression<Func<TEntity, bool>> predicate)
        {
            return realm.All<TEntity>().Where(predicate);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return realm.All<TEntity>();
        }

        public TEntity GetById(string id)
        {
            return realm.All<TEntity>().FirstOrDefault(x =>x.Id == id);
        }

        public void RemoveById(string id)
        {
            var objRemove = realm.All<TEntity>().FirstOrDefault(x => x.Id == id);

            realm.Remove(objRemove);

        }

        public void RemoveByPredicate(System.Linq.Expressions.Expression<Func<TEntity, bool>> predicate)
        {
            var objsRemove = realm.All<TEntity>().Where(predicate);

            realm.RemoveRange(objsRemove);
        }

        public void Update(TEntity obj)
        {
           realm.Add(obj, true);
        }
    }
}