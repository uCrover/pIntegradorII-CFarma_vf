using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

//#nullable disable

namespace CFarma_TemplateN.Models
{
    [Table("Producto")]
    public partial class Producto
    {
        public Producto()
        {
            
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int idpt { get; set; }

        [Required(ErrorMessage = "Debe seleccionar la subcategoría")]
        [ForeignKey("codsubc")]
        public int CodSubc { get; set; } 

        [Required(ErrorMessage = "Debe escribir el nombre del producto")]
        [Column("nom")]
        [Display (Name="Nombre")]
        public string Nom { get; set; }

        [Required(ErrorMessage = "Debe ingresar el precio")]
        [Column("precio")]
        [Display(Name = "Precio")]
        public Decimal Precio { get; set; }

        [Required(ErrorMessage = "Debe ingresar el stock")]
        [Column("stock")]
        [Display(Name = "Stock")]
        public int Stock { get; set; }

        [Column("imagen")]
        [Display(Name = "imagen")]
        public string imagen { get; set; }

        [Required(ErrorMessage = "Debe ingresar descripción")]
        [Column("descripcion1")]
        [Display(Name = "Descripción")]
        public String Descripcion1 { get; set; }

        /*[Required(ErrorMessage = "Debe ingresar una imagen")]
        [Column("imgbin")]
        public byte[] imgbin { get; set; }*/

    }
}
