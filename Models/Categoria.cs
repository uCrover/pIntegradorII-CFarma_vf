using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

//#nullable disable

namespace CFarma_TemplateN.Models
{
    [Table("Categoria")]
    public partial class Categoria
    {
        public Categoria()
        {
            
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int idcat { get; set; }
      
        
        [Column("nomcat")]
        [Display (Name="Nombre Categoria")]
        public string Nombre { get; set; }
        

        [Column("tipo")]
        [Display(Name = "Tipo")]
        public string Tipo { get; set; }



    }
}
