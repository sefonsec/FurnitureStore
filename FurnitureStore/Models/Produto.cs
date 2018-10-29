using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FurnitureStore.Models
{
    public class Produto
    {
        public int id { get; set; }

        [Display(Name = "Descrição")]
        [Required(ErrorMessage = "Informe a Descrição do Produto")]
        public string descricao { get; set; }

        [Display(Name = "Qtde")]
        [Required(ErrorMessage = "Informe a Quantidade")]
        public string qtde { get; set; }

        [Display(Name = "Valor Unitário")]
        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:C}")]
        [Required(ErrorMessage = "Informe o Valor Unitário")]
        public decimal valorUnitario { get; set; }

        [Display(Name = "Tipo de Unidade")]
        [Required(ErrorMessage = "Informe o Tipo de Unidade")]
        public int idTipoUnidade { get; set; }

        public virtual TipoUnidade tipoUnidade { get; set; }

        public string descricaoTipoUnidade { get; set; }

        public static List<TipoUnidadeDescricao> listTipoUnidade = new List<TipoUnidadeDescricao>()
        {
            new TipoUnidadeDescricao() { tipo = TipoUnidade.Peca, descricao = "Peça" },
            new TipoUnidadeDescricao() { tipo = TipoUnidade.Kilo, descricao = "Kilo" },
            new TipoUnidadeDescricao() { tipo = TipoUnidade.Metro, descricao = "Metro" },
            new TipoUnidadeDescricao() { tipo = TipoUnidade.Litro, descricao = "Litro" },
        };
    }

    public enum TipoUnidade : int
    {
        Peca = 1,
        Kilo,
        Metro,
        Litro
    }

    public class TipoUnidadeDescricao
    {
        public TipoUnidade tipo { get; set; }
        public string descricao { get; set; }
    }
}