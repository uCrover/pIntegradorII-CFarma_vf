using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

//#nullable disable

namespace CFarma_TemplateN.Models
{
    [Table("Cliente")]
    public partial class Cliente
    {
        public Cliente()
        {

        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int codc { get; set; }
        [ForeignKey("idtip")]
        public int Idtip { get; set; } = 3;

        [Required(ErrorMessage = "Debe escribir el nombre")]
        [Column("nom")]
        [Display(Name = "Nombre")]
        public string Nom { get; set; }


        [Required(ErrorMessage = "Debe escribir los apellidos")]
        [Column("apellido")]
        [Display(Name = "Apellidos")]
        public string Apellido { get; set; }


        [Required(ErrorMessage = "Debe escribir su numero de dni")]
        //[DataType(DataType., ErrorMessage = "Numero de dni incorrecto")]
        [Column("nrodoc")]
        [Display(Name = "N° Documento de Identidad")]
        public int Nrodoc { get; set; }

        [Column("sexo")]
        [Display(Name = "Sexo")]
        public string Sexo { get; set; }

        [Required(ErrorMessage = "Debe escribir un correo electrónico válido")]
        [DataType(DataType.EmailAddress,ErrorMessage ="Correo electrónico inválido")]
        [Column("correo")]
        [Display(Name = "Correo electrónico")]
        public string Correo { get; set; }

        [Required(ErrorMessage = "Debe escribir un número de teléfono valido")]
        [DataType(DataType.PhoneNumber, ErrorMessage = "Numero de telefono incorrecto")]
        [Column("movil")]
        [Display(Name = "Teléfono móvil")]
        public int Movil { get; set; }

        [Required(ErrorMessage = "Debe escribir una contraseña")]
        [Display(Name = "Contraseña")]
        [Column("pswd")]
        public string Pswd { get; set; }

       
    }
}
