using Android.App;
using Android.Widget;
using Android.OS;
using Realms;
using RealmExample.Interface;
using RealmExample.Data;
using RealmExample.Models;
using System.Linq;
using System;
using Android.Content;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace RealmExample
{
    [Activity(Label = "RealmExample", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        private readonly UoW _uow = new UoW();
        ListView listViewProdutos;
        List<Produto> ListaProdutos;
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView (Resource.Layout.Main);

            listViewProdutos = FindViewById<ListView>(Resource.Id.listViewProdutos);
            listViewProdutos.ItemClick += ListViewProdutos_ItemClick;

            Button btn = FindViewById<Button>(Resource.Id.btnNovo);
            btn.Click += delegate
            {
                Intent itt = new Intent(this, typeof(AddProdutoActivity));
                StartActivity(itt);
            };

           
        }

        private void ListViewProdutos_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            Produto produtoSelecionado = ListaProdutos[e.Position];

            Intent itt = new Intent(this, typeof(AddProdutoActivity));
            itt.PutExtra("PRODUTO_EDIT", JsonConvert.SerializeObject(produtoSelecionado));
            StartActivity(itt);
        }
        
        protected override void OnResume()
        {
            base.OnResume();

            ListaProdutos = _uow.ProdutoRepository.GetAll().ToList();

            listViewProdutos.Adapter = new ArrayAdapter<String>
                (
                this,
                Android.Resource.Layout.SimpleListItem1,
                ListaProdutos.Select(s => s.Descricao).ToList()
                );
        }
    }
}

