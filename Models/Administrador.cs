using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CFarma_TemplateN.Models
{
    [Table("Administrador")]

    public partial class Administrador
    {
        public Administrador()
        {

        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int codad { get; set; }

        [Column("idtip")]
        [ForeignKey("idtip")]
        public int idtip { get; set; } = 1;

        [Required(ErrorMessage = "Debe escribir el nombre")]
        [Display(Name = "Nombre")]
        [Column("nomad")]
        public string nomad { get; set; }

        [EmailAddress]
        [Required(ErrorMessage = "Debe escribir un correo electronico valido")]
        [Display(Name = "Correo electrónico")]
        [Column("correo")]
        public string correo { get; set; }

        [Required(ErrorMessage = "Debe escribir una contraseña")]
        [Display(Name = "Contraseña")]
        [Column("pswd")]
        public string pswd { get; set; }
 
    }
}
