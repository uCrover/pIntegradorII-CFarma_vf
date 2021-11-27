using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

//#nullable disable

namespace CFarma_TemplateN.Models
{
    [Table("SubCategoria")]
    public partial class SubCategoria
    {
        public SubCategoria()
        {
            
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int idsubc { get; set; }

        [ForeignKey("idcat")]
        public int idcat { get; set; }
        

        [Column("nomsc")]
        [Display(Name = "nombreSubcategoria")]
        public string NombreSubCategoria { get; set; }

        [Column("fl_estado")]
        [Display(Name = "flagEstado")]
        public int fl_estado { get; set; }



    }
}
