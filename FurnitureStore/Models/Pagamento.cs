using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace FurnitureStore.Models
{
    public class Pagamento
    {
        public int id { get; set; }

        [Display(Name = "Pagamento")]
        [Required(ErrorMessage = "Informe a Forma de Pagamento")]
        public string formaPagamento { get; set; }

        public virtual ICollection Pedidos { get; set; }
    }
}