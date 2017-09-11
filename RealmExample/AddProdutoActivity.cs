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
using RealmExample.Data;
using RealmExample.Models;
using Newtonsoft.Json;

namespace RealmExample
{
    [Activity(Label = "AddProdutoActivity")]
    public class AddProdutoActivity : Activity
    {
        private readonly UoW _uow = new UoW();
        Produto ProdutoSelecionado;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.AddProduto);

            EditText txtDescricao = FindViewById<EditText>(Resource.Id.txtDescricaoProduto);

            Button btnSalvar = FindViewById<Button>(Resource.Id.btnSalvar);
            btnSalvar.Click += delegate
            {
                if (!string.IsNullOrEmpty(txtDescricao.Text))
                {
                    _uow.BeginTransaction();

                    try
                    {
                        Produto produto = new Produto
                        {
                            Descricao = txtDescricao.Text
                        };

                        if (ProdutoSelecionado == null)
                            _uow.ProdutoRepository.Add(produto);
                        else
                        {
                            ProdutoSelecionado.Descricao = txtDescricao.Text;
                            _uow.ProdutoRepository.Update(ProdutoSelecionado);
                        }

                        _uow.CommitTransaction();
                        Finish();
                    }
                    catch (Exception ex)
                    {
                        _uow.RollBackTransaction();
                    }
                                                           
                }
            };

            if(Intent.GetStringExtra("PRODUTO_EDIT") != null)
            {
                ProdutoSelecionado = JsonConvert.DeserializeObject<Produto>(Intent.GetStringExtra("PRODUTO_EDIT"));

                txtDescricao.Text = ProdutoSelecionado.Descricao;

                Button btnRemover = FindViewById<Button>(Resource.Id.btnRemover);
                btnRemover.Visibility = ViewStates.Visible;
                btnRemover.Click += delegate
                {
                    _uow.BeginTransaction();
                    _uow.ProdutoRepository.RemoveByPredicate(x=>x.Id == ProdutoSelecionado.Id);
                    _uow.CommitTransaction();
                    Finish();
                };

            }           
        }
    }
}