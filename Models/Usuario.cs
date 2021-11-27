using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CFarma_TemplateN.Models
{
    public class Usuario
    {
        [Required(ErrorMessage = "Debe escribir un correo electronico valido")]
        [Column("correo")]
        [Display(Name = "Correo electrónico")]
        public string Correo { get; set; }

        [Required(ErrorMessage = "Debe escribir una contraseña")]
        [Display(Name = "Contraseña")]
        [Column("pswd")]
        public string Pswd { get; set; }
        public int codUser { get; set; }
        public string perfil { get; set; }
        public string nombre { get; set; }
        public string apellido { get; set; } 

        public int idtip { get; set; }
    }
}
