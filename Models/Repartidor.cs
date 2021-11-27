using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

//#nullable disable

namespace CFarma_TemplateN.Models
{
    [Table("Repartidor")]
    public partial class Repartidor
    {
        public Repartidor()
        {
            
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int codrep { get; set; }
        [ForeignKey("idtip")]
        public int Idtip { get; set; } = 2;

        [Required(ErrorMessage = "Debe escribir el nombre")]
        [Column("nomrep")]
        [Display (Name="Nombre")]
        public string Nom { get; set; }

        [EmailAddress]
        [Required(ErrorMessage = "Debe escribir un correo electronico valido")]
        [Column("correo")]
        [Display(Name = "Correo electrónico")]
        public string Correo { get; set; }

        [Required(ErrorMessage = "Debe escribir una contraseña")]
        [Display(Name = "Contraseña")]
        [Column("pswd")]
        public string Pswd { get; set; }

       
    }
}
