using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CFarma_TemplateN.Models
{
    [Table("EstadoPedido")]
    public partial class EstadoPedido
    {
        public EstadoPedido()
        {

        }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int idEstadoPedido { get; set; }
        public string nom { get; set; }
        public string descripcion { get; set; }
    }
}
