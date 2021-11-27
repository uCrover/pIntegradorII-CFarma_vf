using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

//#nullable disable

namespace CFarma_TemplateN.Models
{
    public partial class Carrito 
    {

        public int idpt { get; set; }

        public string Nom { get; set; }
        public string id_cliente { get; set; }

        public int cantidad { get; set; }

        public Decimal Precio { get; set; }

        public string identificador { get; set; }

        public string imagen { get; set; }

        public int stock { get; set; }

        public Decimal Total()
        {
            return cantidad * Precio;
        }

    }
}
