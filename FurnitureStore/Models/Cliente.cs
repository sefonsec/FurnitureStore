using System;
using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace FurnitureStore.Models
{
    public class Cliente
    {
        public int Id { get; set; }

        [Display(Name = "Data de cadastro")]
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Informe a Data de Cadastro")]
        public DateTime dataCadastro { get; set; }

        [Display(Name = "Nome")]
        [Required(ErrorMessage = "Informe o Nome do Cliente")]
        public string nome { get; set; }

        [Display(Name = "RG")]
        public string rg { get; set; }

        [Display(Name = "CPF")]
        public string cpf { get; set; }

        [Display(Name = "Data de nascimento")]
        [DataType(DataType.Date)]
        public DateTime dataNasc { get; set; }

        [Display(Name = "Endereço")]
        [Required(ErrorMessage = "Informe o Endereço")]
        public string endereco { get; set; }

        [Display(Name = "Número")]
        [Required(ErrorMessage = "Informe o Número")]
        public int numero { get; set; }

        [Display(Name = "Complemento")]
        public string complemento { get; set; }

        [Display(Name = "Bairro")]
        [Required(ErrorMessage = "Informe o Bairro")]
        public string bairro { get; set; }

        [Display(Name = "Cidade")]
        [Required(ErrorMessage = "Informe a Cidade")]
        public string cidade { get; set; }

        [Display(Name = "Estado")]
        [Required(ErrorMessage = "Informe o Estado")]
        public string uf { get; set; }

        [Display(Name = "CEP")]
        [Required(ErrorMessage = "Informe o CEP")]
        public string cep { get; set; }

        [Display(Name = "Nome do Banco")]
        public string nomeBanco { get; set; }

        [Display(Name = "Agência")]
        public string agencia { get; set; }

        [Display(Name = "Conta Corrente")]
        public string contaCorrente { get; set; }

        [Display(Name = "Cliente desde")]
        [DataType(DataType.Date)]
        public DateTime dataClienteDesde { get; set; }

        public virtual ICollection Pedidos { get; set; }
    }
}