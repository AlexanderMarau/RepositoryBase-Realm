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
using Realms;
using RealmExample.Repository;

namespace RealmExample.Data
{
    public class UoW
    {
        protected readonly Realm realm;
        Transaction transaction;

        public UoW()
        {
            realm = DatabaseManager.GetInstance();
        }

        private ProdutoRepository _produtoRepository;
        public ProdutoRepository ProdutoRepository
        {
            get
            {

                if (_produtoRepository == null)
                {
                    _produtoRepository = new ProdutoRepository(realm);
                }
                return _produtoRepository;
            }
        }


        public void BeginTransaction()
        {
            transaction = realm.BeginWrite();
        }

        public void CommitTransaction()
        {
            if (realm.IsInTransaction && transaction != null)
                transaction.Commit();
        }

        public void RollBackTransaction()
        {
            if (realm.IsInTransaction && transaction != null)
                transaction.Rollback();
        }

        #region Dispose
        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    realm.Dispose();
                }
            }
            this.disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}