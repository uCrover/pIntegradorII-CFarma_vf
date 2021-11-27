using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CFarma_TemplateN.Models
{
    [Table("Pedido")]
    public partial class Pedido
    {
        public Pedido()
        {

        }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int idp { get; set; }

        [ForeignKey("codc")]
        public int codc { get; set; }

        [ForeignKey("idestado")]
        public int idestado { get; set; }
        public decimal total { get; set; }
        public DateTime fecha { get; set; }

        [Display(Name = "Forma de Pago")]
        public string forma_pago { get; set; }

        [Required(ErrorMessage = "Por favor ingrese un lugar de envio")]
        [Display(Name = "Lugar de envio")]
        public string lugar_envio { get; set; }

        [Required(ErrorMessage = "Por favor ingrese nombre de encargado")]
        [Display(Name = "Nombre encargado")]
        public string nom_cli_recibo { get; set; }

        [Required(ErrorMessage = "Por favor ingrese DNI de encargado")]
        //[StringLength(8, ErrorMessage = "Por favor ingrese DNI de encargado")]
        [Display(Name = "DNI encargado")]
        public int dni_cli_recibo { get; set; }

        //(8 , ErrorMessage = "Por favor ingrese DNI de encargado")

    }
}
