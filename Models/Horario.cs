using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CFarma_TemplateN.Models
{
    [Table("Horario")]
    public class Horario
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int idhorario { get; set; }

        [Required(ErrorMessage = "Debe seleccionar el día")]
        [Display(Name = "Dia")]
        [Column("nombre")]
        public string nombre { get; set; }

    }
}
