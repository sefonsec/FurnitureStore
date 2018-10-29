using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FurnitureStore.Models
{
    public class Vendedor
    {
        public int id { get; set; }

        [Display(Name = "Vendedor")]
        [Required(ErrorMessage = "Informe o Nome completo do vendedor")]        
        public string nome { get; set; }

        [Display(Name = "Apelido")]
        public string apelido { get; set; }

        public virtual ICollection Pedidos { get; set; }
    }
}