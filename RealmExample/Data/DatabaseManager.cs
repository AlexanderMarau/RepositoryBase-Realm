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

namespace RealmExample.Data
{
    public static class DatabaseManager
    {
        public static Realm GetInstance()
        {
            var config = new RealmConfiguration("RealmDatabaseExample.realm")
            {
                #if DEBUG
                ShouldDeleteIfMigrationNeeded = true
                #endif
            };
            var realmDataBase = Realm.GetInstance(config);

            return realmDataBase;
        }
    }
}