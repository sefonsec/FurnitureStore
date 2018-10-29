using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FurnitureStore.Models.DTO
{
    public class PedidoDTO
    {
        public int id { get; set; }
        public DateTime dataPedido { get; set; }
        public string nomeVendedor { get; set; }
        public string nomeCliente { get; set; }
        public string rg { get; set; }
        public string cpf { get; set; }
        public DateTime dataNasc { get; set; }
        public string endereco { get; set; }
        public int numero { get; set; }
        public string bairro { get; set; }
        public string cidade { get; set; }
        public string uf { get; set; }
        public string cep { get; set; }
        public string banco { get; set; }
        public string agencia { get; set; }
        public string contaCorrente { get; set; }
        public DateTime clienteDesde { get; set; }
      
    }

    public class ItensPedidoDTO
    {
        public int id { get; set; }
        public string qtdeProduto { get; set; }
        public string descricao { get; set; }
        public string valorUnitario { get; set; }
        public string valorTotal { get; set; }
    }

}