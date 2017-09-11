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
using RealmExample.Models;
using Realms;

namespace RealmExample.Repository
{
    public class ProdutoRepository : RepositoryBase<Produto>
    {
        public ProdutoRepository(Realm realm)
            :base(realm)
        {

        }
    }
}