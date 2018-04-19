using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cocina_Tech.BO
{
    public class CategoriaBO
    {
        public int IDCT { get; set; }
        public string Nombre { get; set; }
        public DateTime FechaCreacion { get; set; }
        public bool Estado { get; set; }


    }
}