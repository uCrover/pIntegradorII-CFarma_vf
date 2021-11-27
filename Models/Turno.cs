using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CFarma_TemplateN.Models
{
    [Table("Turno")]
    public class Turno
    {

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int idturno { get; set; }

        [Required(ErrorMessage = "Debe seleccionar el repartidor")]
        [Display(Name = "Repartidor")]
        [ForeignKey("codrep")]
        public int codrep { get; set; }

        [Required(ErrorMessage = "Debe seleccionar el día")]
        [Display(Name = "Dia")]
        [ForeignKey("idhorario")]
        public int idhorario { get; set; }





    }
}
