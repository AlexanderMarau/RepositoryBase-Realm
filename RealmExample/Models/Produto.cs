using RealmExample.Interface;
using Realms;
using System;

namespace RealmExample.Models
{
    public class Produto : RealmObject, IContractID
    {
        public Produto()
        {
            Id = Guid.NewGuid().ToString();
        }

        [PrimaryKey]
        public string Id { get; set; }
        public string Descricao { get; set; }
    }
}