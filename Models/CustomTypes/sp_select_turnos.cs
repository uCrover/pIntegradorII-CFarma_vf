using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CFarma_TemplateN.Models.CustomTypes
{
    public partial class sp_select_turnos 
    {
        [Key]
        public int IDTurno { get; set; }

        public string DiaSemana { get; set; }

        public string NombreRepartidor { get; set; }

        
    }
}
