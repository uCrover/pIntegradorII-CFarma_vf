using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CFarma_TemplateN.Models
{
    [Table("Detalle_pedido")]
    public partial class Detalle_pedido
    {
        public Detalle_pedido()
        {
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int iddp { get; set; }

        [ForeignKey("idp")]
        public int idp { get; set; }

        [ForeignKey("idpt")]
        public int idpt { get; set; }

        public int cantidad { get; set; }
    }
}
