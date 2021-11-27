using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CFarma_TemplateN.Models.CustomTypes
{
    public partial class sp_select_pedido_estados
    {
        [Key]
        public int idp { get; set; }

        public string nomCli { get; set; }

        public int estadoPedido { get; set; }
        public decimal totalPago { get; set; }

        public string fecha { get; set; }
        public string hora { get; set; }

        public string forma_pago { get; set; }

        public string lugar_envio { get; set; }
    }
}
