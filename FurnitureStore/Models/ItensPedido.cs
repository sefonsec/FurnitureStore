using System.ComponentModel.DataAnnotations;

namespace FurnitureStore.Models
{
    public class ItensPedido
    {
        public int id { get; set; }

        public int pedidoID { get; set; }
        public virtual Pedido Pedido { get; set; }

        [Display(Name = "Produto")]
        [Required(ErrorMessage = "Informe o Produto")]
        public int produtoID { get; set; }
        public virtual Produto Produto { get; set; }

        [Display(Name = "Qtde")]
        [Required(ErrorMessage = "Informe a Qtde")]
        public string qtdeProduto { get; set; }

        [Display(Name = "Valor Total")]
        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public decimal valorTotalProduto { get; set; }
    }
}