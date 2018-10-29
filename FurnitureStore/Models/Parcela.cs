using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace FurnitureStore.Models
{
    public class Parcela
    {
        public int id { get; set; }

        [Display(Name = "Parcela")]
        [Required(ErrorMessage = "Informe o Número de Parcelas")]
        public string numeroParcela { get; set; }

        public virtual ICollection Pedidos { get; set; }
    }
}