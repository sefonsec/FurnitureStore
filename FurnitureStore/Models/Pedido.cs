using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace FurnitureStore.Models
{
    public class Pedido
    {
        public int id { get; set; }

        [Display(Name = "Vendedor")]
        [Required(ErrorMessage = "Informe o Vendedor")]
        public int vendedorID { get; set; }
        public virtual Vendedor Vendedor { get; set; }

        [Display(Name = "Cliente")]
        [Required(ErrorMessage = "Informe o Cliente")]
        public int clienteID { get; set; }
        public virtual Cliente Cliente { get; set; }

        [Display(Name = "Forma de Pagamento")]
        [Required(ErrorMessage = "Informe a Forma de Pagamento")]
        public int pagamentoID { get; set; }
        public virtual Pagamento Pagamento { get; set; }

        [Display(Name = "Número de Parcelas")]
        public int parcelaID { get; set; }
        public virtual Parcela Parcela { get; set; }

        [Display(Name = "Data do Pedido")]
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Informe a Data do Pedido")]
        public DateTime dataPedido { get; set; }

        [Display(Name = "Entrada de")]
        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public Nullable<decimal> entrada { get; set; } // Quanto o cliente está deixando para a entrada

        [Display(Name = "Para todo o dia")]
        public Nullable<int> paraTodoDia { get; set; } // Ex.: Para todo dia 15

        [Display(Name = "Prazo de Entrega")]
        [DataType(DataType.Date)]
        public Nullable<DateTime> prazoEntrega { get; set; }

        [Display(Name = "Tipo Pedido")]
        [Required(ErrorMessage = "Informe o Tipo de Pedido")]
        public int idTipoPedido { get; set; }
        public virtual TipoPedido tipoPedido { get; set; }

        public string descricaoTipoPedido { get; set; }

        public static List<TipoPedidoDescricao> listTipoPedido = new List<TipoPedidoDescricao>()
        {
            new TipoPedidoDescricao() { tipo = TipoPedido.Pedido, descricao = "Pedido" },
            new TipoPedidoDescricao() { tipo = TipoPedido.Orcamento, descricao = "Orçamento" },
        };

    }

    public enum TipoPedido : int
    {
        Pedido = 1,
        Orcamento
    }

    public class TipoPedidoDescricao
    {
        public TipoPedido tipo { get; set; }
        public string descricao { get; set; }
    }
}